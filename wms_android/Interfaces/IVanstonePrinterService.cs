using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    public interface IVanstonePrinterService
    {
        Task<bool> InitializePrinterAsync();
        Task<bool> PrintTextAsync(string text);
        Task<bool> PrintQRCodeAsync(string qrCode);
        Task<bool> CutPaperAsync();
        Task<string> GetStatusAsync();
    }
} 