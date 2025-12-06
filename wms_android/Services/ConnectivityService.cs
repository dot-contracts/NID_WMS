using Microsoft.Maui.Networking;
using wms_android.Interfaces;

namespace wms_android.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool IsConnected => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

        public bool IsWiFiConnected => 
            Connectivity.Current.NetworkAccess == NetworkAccess.Internet &&
            Connectivity.Current.ConnectionProfiles.Contains(ConnectionProfile.WiFi);

        public async Task<bool> CheckConnectivityAsync()
        {
            return await Task.FromResult(IsConnected);
        }

        public event EventHandler ConnectivityChanged;

        public ConnectivityService()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}