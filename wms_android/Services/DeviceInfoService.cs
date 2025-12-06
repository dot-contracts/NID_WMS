using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Maui.Devices;

namespace wms_android.Services
{
    public class DeviceInfoService
    {
        public static async Task<DeviceInfoDto> GetDeviceInfoAsync()
        {
            try
            {
                var deviceInfo = new DeviceInfoDto
                {
                    SerialNumber = await GetDeviceSerialNumberAsync(),
                    DeviceName = Microsoft.Maui.Devices.DeviceInfo.Name,
                    DeviceModel = Microsoft.Maui.Devices.DeviceInfo.Model,
                    DeviceManufacturer = Microsoft.Maui.Devices.DeviceInfo.Manufacturer,
                    AndroidVersion = Microsoft.Maui.Devices.DeviceInfo.VersionString,
                    AppVersion = AppInfo.VersionString,
                    Location = await GetLocationAsync()
                };

                Debug.WriteLine($"Device Info: {deviceInfo.SerialNumber}, {deviceInfo.DeviceName}, {deviceInfo.DeviceModel}");
                return deviceInfo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting device info: {ex.Message}");
                return new DeviceInfoDto
                {
                    SerialNumber = "unknown",
                    DeviceName = "Unknown Device",
                    DeviceModel = "Unknown",
                    DeviceManufacturer = "Unknown",
                    AndroidVersion = "Unknown",
                    AppVersion = AppInfo.VersionString
                };
            }
        }

        private static async Task<string> GetDeviceSerialNumberAsync()
        {
            try
            {
#if ANDROID
                var context = Platform.CurrentActivity;
                if (context != null)
                {
                    var telephonyManager = context.GetSystemService(Android.Content.Context.TelephonyService) as Android.Telephony.TelephonyManager;
                    if (telephonyManager != null)
                    {
                        var imei = telephonyManager.Imei;
                        if (!string.IsNullOrEmpty(imei))
                        {
                            return imei;
                        }
                    }

                    // Fallback to Android ID
                    var androidId = Android.Provider.Settings.Secure.GetString(
                        context.ContentResolver, 
                        Android.Provider.Settings.Secure.AndroidId);
                    
                    if (!string.IsNullOrEmpty(androidId))
                    {
                        return androidId;
                    }
                }
#endif
                // Fallback to a generated ID stored in preferences
                var storedId = Preferences.Get("DeviceId", "");
                if (string.IsNullOrEmpty(storedId))
                {
                    storedId = Guid.NewGuid().ToString();
                    Preferences.Set("DeviceId", storedId);
                }
                return storedId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting device serial: {ex.Message}");
                return "unknown";
            }
        }

        private static async Task<string> GetLocationAsync()
        {
            try
            {
                // This is a simplified location implementation
                // In a real app, you'd want to use proper location services
                return "Location not available";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting location: {ex.Message}");
                return "Location unavailable";
            }
        }
    }

    public class DeviceInfoDto
    {
        public string SerialNumber { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceManufacturer { get; set; }
        public string AndroidVersion { get; set; }
        public string AppVersion { get; set; }
        public string Location { get; set; }
    }
}