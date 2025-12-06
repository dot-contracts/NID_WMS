using wms_android.Interfaces;
using wms_android.Models;
using wms_android.Controls;
using Microsoft.Extensions.Logging;

namespace wms_android.Services
{
    public class UpdateManager
    {
        private readonly IUpdateService _updateService;
        private readonly IConnectivityService _connectivityService;
        private readonly ILogger<UpdateManager> _logger;
        private UpdateNotificationDialog _currentDialog;

        public UpdateManager(IUpdateService updateService, IConnectivityService connectivityService, ILogger<UpdateManager> logger)
        {
            _updateService = updateService;
            _connectivityService = connectivityService;
            _logger = logger;

            // Subscribe to update events
            _updateService.UpdateAvailable += OnUpdateAvailable;
            _updateService.DownloadProgress += OnDownloadProgress;
            _updateService.UpdateInstalled += OnUpdateInstalled;
        }

        public async Task StartMonitoringAsync()
        {
            _logger.LogInformation("Starting update monitoring");
            await _updateService.StartPeriodicUpdateCheckAsync();
        }

        public async Task StopMonitoringAsync()
        {
            _logger.LogInformation("Stopping update monitoring");
            await _updateService.StopPeriodicUpdateCheckAsync();
        }

        private async void OnUpdateAvailable(object sender, UpdateInfo updateInfo)
        {
            try
            {
                // Only show dialog if connected to WiFi
                if (!_connectivityService.IsWiFiConnected)
                {
                    _logger.LogInformation("Update available but not on WiFi, skipping notification");
                    return;
                }

                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await ShowUpdateDialogAsync(updateInfo);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing update dialog");
            }
        }

        private async Task ShowUpdateDialogAsync(UpdateInfo updateInfo)
        {
            try
            {
                var mainPage = Application.Current?.MainPage;
                if (mainPage == null) return;

                _currentDialog = new UpdateNotificationDialog();

                // Get the current displayed page
                ContentPage targetPage = null;
                if (mainPage is Shell shell && shell.CurrentPage is ContentPage contentPage)
                {
                    targetPage = contentPage;
                }
                else if (mainPage is ContentPage directPage)
                {
                    targetPage = directPage;
                }

                if (targetPage?.Content is Grid grid)
                {
                    // Add dialog to existing grid
                    grid.Children.Add(_currentDialog);
                }
                else if (targetPage != null)
                {
                    // Wrap existing content in a grid to add dialog
                    var existingContent = targetPage.Content;
                    var wrapperGrid = new Grid();
                    wrapperGrid.Children.Add(existingContent);
                    wrapperGrid.Children.Add(_currentDialog);
                    targetPage.Content = wrapperGrid;
                }

                // Show the dialog with callbacks
                await _currentDialog.ShowUpdateDialogAsync(
                    updateInfo,
                    onAccept: () => HandleUpdateAcceptAsync(updateInfo),
                    onDeny: () => HandleUpdateDenyAsync()
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing update dialog");
            }
        }

        private async Task HandleUpdateAcceptAsync(UpdateInfo updateInfo)
        {
            try
            {
                _logger.LogInformation("User accepted update, starting download");

                var progress = new Progress<double>(percentage =>
                {
                    _currentDialog?._viewModel?.UpdateDownloadProgress(percentage);
                });

                var downloadSuccess = await _updateService.DownloadUpdateAsync(updateInfo, progress);
                
                if (downloadSuccess)
                {
                    _logger.LogInformation("Update downloaded successfully, starting installation");
                    await _updateService.InstallUpdateAsync();
                }
                else
                {
                    _logger.LogError("Failed to download update");
                    await HideCurrentDialogAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling update accept");
                await HideCurrentDialogAsync();
            }
        }

        private async Task HandleUpdateDenyAsync()
        {
            _logger.LogInformation("User denied update");
            await HideCurrentDialogAsync();
        }

        private async Task HideCurrentDialogAsync()
        {
            if (_currentDialog != null)
            {
                await _currentDialog.HideDialogAsync();
                
                // Remove from parent
                if (_currentDialog.Parent is Grid parentGrid)
                {
                    parentGrid.Children.Remove(_currentDialog);
                }
                
                _currentDialog = null;
            }
        }

        private void OnDownloadProgress(object sender, double progress)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _currentDialog?._viewModel?.UpdateDownloadProgress(progress);
            });
        }

        private async void OnUpdateInstalled(object sender, EventArgs e)
        {
            _logger.LogInformation("Update installation triggered");
            await HideCurrentDialogAsync();
        }
    }
}