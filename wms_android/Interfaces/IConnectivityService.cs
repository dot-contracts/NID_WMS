using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    public interface IConnectivityService
    {
        bool IsConnected { get; }
        bool IsWiFiConnected { get; }
        Task<bool> CheckConnectivityAsync();
        event EventHandler ConnectivityChanged;
    }
}