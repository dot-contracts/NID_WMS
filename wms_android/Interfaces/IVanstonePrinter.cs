using System.Threading.Tasks;

namespace wms_android.Interfaces
{
    public interface IVanstonePrinter
    {
        bool Initialize();
        bool Print(string text);
        bool PrintQRCode(string qrCode);
        bool PrintBarcode(string barcode);
        bool CutPaper();
        bool FeedPaper(int lines);
        string GetStatus();
        void Close();
    }
} 