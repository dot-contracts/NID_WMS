using Microsoft.Maui.Devices;
using wms_android.Interfaces;
using wms_android.shared.Models;

namespace wms_android.Services
{
    public class DeviceDetectionService : IDeviceDetectionService
    {
        public PosDeviceType DetectDevice()
        {
            var deviceModel = GetDeviceModel().ToUpperInvariant();
            
            if (deviceModel.Contains("A90") || deviceModel.Contains("VANSTONE"))
            {
                return PosDeviceType.A90;
            }
            else if (deviceModel.Contains("CS30"))
            {
                return PosDeviceType.CS30;
            }
            
            // Default to CS30 if unknown (can be adjusted as needed)
            return PosDeviceType.CS30;
        }
        
        public string GetDeviceModel()
        {
            return Android.OS.Build.Model;
        }
        
        public string GetDeviceManufacturer()
        {
            return Android.OS.Build.Manufacturer;
        }
        
        public string GetDeviceName()
        {
            return $"{GetDeviceManufacturer()} {GetDeviceModel()}";
        }
        
        public string GetDeviceVersion()
        {
            return Android.OS.Build.VERSION.Release;
        }
        
        public string GetDevicePlatform()
        {
            return Microsoft.Maui.Devices.DeviceInfo.Platform.ToString();
        }
    }
} 