using System;

namespace wms_android.Interfaces
{
    public interface ICiontekPosService
    {
        int InitPrinter();
        int SetAlign(int align);
        int PrintText(string text);
        int PrintQRCode(string content, int size);
        int PrintBitmap(byte[] bitmapData);
        int FeedPaper(int lines);
        int StartPrint();
        int GetPrinterStatus();
        int InitScanner(int mode);
        int StartScan();
        int StopScan();
        int GetScannerStatus();
        void SetFont(int font);
    }
} 