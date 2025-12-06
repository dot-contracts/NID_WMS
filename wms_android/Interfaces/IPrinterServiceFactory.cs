using wms_android.shared.Models;

namespace wms_android.Interfaces
{
    public interface IPrinterServiceFactory
    {
        IPrinterService GetPrinterService(PosDeviceType deviceType);
        IPrinterService GetDefaultPrinterService();
    }
} 