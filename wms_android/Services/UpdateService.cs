using System.Net.Http.Json;
using System.Text.Json;
using wms_android.Interfaces;
using wms_android.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;

#if ANDROID
using AndroidX.Core.Content;
using Android.Content;
using JavaFile = Java.IO.File;
using AndroidFileProvider = AndroidX.Core.Content.FileProvider;
#endif

namespace wms_android.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivityService _connectivityService;
        private readonly ILogger<UpdateService> _logger;
        private readonly Timer _updateCheckTimer;
        private const string GITHUB_API_BASE = "https://api.github.com";
        private const string GITHUB_REPO = "dot-contracts/WMS_Android";
        private const string TARGET_BRANCH = "aisino";
        private const int CHECK_INTERVAL_MINUTES = 30;

        public event EventHandler<UpdateInfo> UpdateAvailable;
        public event EventHandler<double> DownloadProgress;
        public event EventHandler UpdateInstalled;

        public UpdateService(HttpClient httpClient, IConnectivityService connectivityService, ILogger<UpdateService> logger)
        {
            _httpClient = httpClient;
            _connectivityService = connectivityService;
            _logger = logger;
            
            // Configure HttpClient for GitHub API
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WMS-Android-App/1.0");
            
            _updateCheckTimer = new Timer(OnUpdateCheckTimer, null, Timeout.Infinite, Timeout.Infinite);
        }

        public async Task<UpdateInfo> CheckForUpdatesAsync()
        {
            try
            {
                if (!_connectivityService.IsConnected)
                {
                    _logger.LogInformation("No internet connection for update check");
                    return new UpdateInfo { IsUpdateAvailable = false };
                }

                _logger.LogInformation($"Checking for updates on {TARGET_BRANCH} branch");

                // Get the latest commit from the target branch
                var response = await _httpClient.GetAsync($"{GITHUB_API_BASE}/repos/{GITHUB_REPO}/commits/{TARGET_BRANCH}");
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Failed to fetch commit info: {response.StatusCode}");
                    return new UpdateInfo { IsUpdateAvailable = false };
                }

                var commitData = await response.Content.ReadFromJsonAsync<GitHubCommit>();
                if (commitData == null)
                {
                    _logger.LogWarning("Failed to deserialize commit data");
                    return new UpdateInfo { IsUpdateAvailable = false };
                }

                var currentCommitSha = Preferences.Get("CurrentCommitSha", string.Empty);
                var isUpdateAvailable = !string.IsNullOrEmpty(currentCommitSha) && 
                                      currentCommitSha != commitData.Sha;

                var updateInfo = new UpdateInfo
                {
                    CommitSha = commitData.Sha,
                    CommitDate = commitData.Commit.Author.Date,
                    CommitMessage = commitData.Commit.Message,
                    IsUpdateAvailable = isUpdateAvailable,
                    Version = commitData.Sha[..8] // Use first 8 chars of SHA as version
                };

                if (isUpdateAvailable)
                {
                    // Try to get download URL from releases or build artifacts
                    updateInfo.DownloadUrl = await GetDownloadUrlAsync();
                }

                _logger.LogInformation($"Update check completed. Update available: {isUpdateAvailable}");
                return updateInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking for updates");
                return new UpdateInfo { IsUpdateAvailable = false };
            }
        }

        private async Task<string> GetDownloadUrlAsync()
        {
            try
            {
                // Check for latest release with APK asset
                var response = await _httpClient.GetAsync($"{GITHUB_API_BASE}/repos/{GITHUB_REPO}/releases/latest");
                
                if (response.IsSuccessStatusCode)
                {
                    var release = await response.Content.ReadFromJsonAsync<GitHubRelease>();
                    var apkAsset = release?.Assets?.FirstOrDefault(a => a.Name.EndsWith(".apk", StringComparison.OrdinalIgnoreCase));
                    
                    if (apkAsset != null)
                    {
                        return apkAsset.Browser_download_url;
                    }
                }
                
                // Fallback: construct URL pattern for GitHub Actions artifacts (if available)
                // You might need to adjust this based on your CI/CD setup
                return $"https://github.com/{GITHUB_REPO}/releases/latest/download/app-release.apk";
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get download URL");
                return string.Empty;
            }
        }

        public async Task<bool> DownloadUpdateAsync(UpdateInfo updateInfo, IProgress<double> progress = null)
        {
            try
            {
                if (string.IsNullOrEmpty(updateInfo.DownloadUrl))
                {
                    _logger.LogWarning("No download URL available for update");
                    return false;
                }

                _logger.LogInformation($"Starting download of update from {updateInfo.DownloadUrl}");

                var downloadPath = System.IO.Path.Combine(FileSystem.CacheDirectory, "app-update.apk");
                
                using var response = await _httpClient.GetAsync(updateInfo.DownloadUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? 0;
                
                using var contentStream = await response.Content.ReadAsStreamAsync();
                using var fileStream = new System.IO.FileStream(downloadPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);

                var buffer = new byte[8192];
                var totalBytesRead = 0L;
                int bytesRead;

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;

                    if (totalBytes > 0)
                    {
                        var progressPercentage = (double)totalBytesRead / totalBytes * 100;
                        progress?.Report(progressPercentage);
                        DownloadProgress?.Invoke(this, progressPercentage);
                    }
                }

                // Store the download path for installation
                Preferences.Set("UpdateApkPath", downloadPath);
                Preferences.Set("PendingUpdateSha", updateInfo.CommitSha);

                _logger.LogInformation($"Update downloaded successfully to {downloadPath}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading update");
                return false;
            }
        }

        public async Task<bool> InstallUpdateAsync()
        {
            try
            {
                var apkPath = Preferences.Get("UpdateApkPath", string.Empty);
                if (string.IsNullOrEmpty(apkPath) || !System.IO.File.Exists(apkPath))
                {
                    _logger.LogWarning("No update APK found for installation");
                    return false;
                }

                _logger.LogInformation($"Installing update from {apkPath}");

                // For Android, we need to use the Android Package Installer
                // This requires the app to request INSTALL_PACKAGES permission
#if ANDROID
                var file = new JavaFile(apkPath);
                var fileUri = AndroidFileProvider.GetUriForFile(Platform.CurrentActivity.ApplicationContext, $"{AppInfo.PackageName}.fileprovider", file);
                
                var intent = new Intent(Intent.ActionInstallPackage);
                intent.SetDataAndType(fileUri, "application/vnd.android.package-archive");
                intent.SetFlags(ActivityFlags.GrantReadUriPermission | ActivityFlags.NewTask);
                intent.PutExtra(Intent.ExtraNotUnknownSource, true);
                intent.PutExtra(Intent.ExtraReturnResult, true);
                
                Platform.CurrentActivity?.StartActivity(intent);
#else
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(apkPath)
                });
#endif

                // Update the current commit SHA after successful installation trigger
                var pendingUpdateSha = Preferences.Get("PendingUpdateSha", string.Empty);
                if (!string.IsNullOrEmpty(pendingUpdateSha))
                {
                    Preferences.Set("CurrentCommitSha", pendingUpdateSha);
                    Preferences.Remove("PendingUpdateSha");
                    Preferences.Remove("UpdateApkPath");
                }

                UpdateInstalled?.Invoke(this, EventArgs.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error installing update");
                return false;
            }
        }

        public async Task StartPeriodicUpdateCheckAsync()
        {
            _logger.LogInformation($"Starting periodic update checks every {CHECK_INTERVAL_MINUTES} minutes");
            
            // Initial check after 30 seconds
            _updateCheckTimer.Change(TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(CHECK_INTERVAL_MINUTES));
            
            await Task.CompletedTask;
        }

        public async Task StopPeriodicUpdateCheckAsync()
        {
            _logger.LogInformation("Stopping periodic update checks");
            _updateCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
            await Task.CompletedTask;
        }

        private async void OnUpdateCheckTimer(object state)
        {
            try
            {
                // Only check if connected to WiFi
                if (!_connectivityService.IsWiFiConnected)
                {
                    _logger.LogDebug("Not connected to WiFi, skipping update check");
                    return;
                }

                var updateInfo = await CheckForUpdatesAsync();
                if (updateInfo.IsUpdateAvailable)
                {
                    _logger.LogInformation($"Update available: {updateInfo.Version}");
                    UpdateAvailable?.Invoke(this, updateInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in periodic update check");
            }
        }
    }
}