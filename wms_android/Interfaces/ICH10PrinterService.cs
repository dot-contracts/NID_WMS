using Android.Graphics;

namespace wms_android.Interfaces
{
    public interface ICH10PrinterService : IPrinterService
    {
        Task<bool> PrintTextAsync(string text);
        Task<bool> PrintBitmapAsync(Bitmap bitmap);
        Task<bool> PrintQRCodeAsync(string data, int moduleSize = 4, int errorLevel = 3);
        Task<bool> PrintBarcodeAsync(string data, int type = 3, int height = 80, int width = 2);
        Task<bool> SetTextBoldAsync(bool bold);
        Task<bool> SetTextSizeAsync(float textSize);
        Task<bool> SetAlignmentAsync(int alignment);
        Task<bool> NextLineAsync(int lines = 1);
        Task<bool> PrintTableTextAsync(string[] text, int[] weight, int[] alignment);
        Task<bool> BeginWorkAsync();
        Task<bool> EndWorkAsync();
        Task<string> GetServiceVersionAsync();
        Task<bool> GetScannerStatusAsync();
    }
}