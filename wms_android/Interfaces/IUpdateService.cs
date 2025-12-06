using System;
using System.Threading.Tasks;
using wms_android.Models;

namespace wms_android.Interfaces
{
    public interface IUpdateService
    {
        Task<UpdateInfo> CheckForUpdatesAsync();
        Task<bool> DownloadUpdateAsync(UpdateInfo updateInfo, IProgress<double> progress = null);
        Task<bool> InstallUpdateAsync();
        Task StartPeriodicUpdateCheckAsync();
        Task StopPeriodicUpdateCheckAsync();
        event EventHandler<UpdateInfo> UpdateAvailable;
        event EventHandler<double> DownloadProgress;
        event EventHandler UpdateInstalled;
    }
}