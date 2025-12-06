using wms_android.shared.Models;

namespace wms_android.Interfaces
{
    public interface IDeviceDetectionService
    {
        PosDeviceType DetectDevice();
        string GetDeviceModel();
        string GetDeviceManufacturer();
        string GetDeviceName();
        string GetDeviceVersion();
        string GetDevicePlatform();
    }
} 