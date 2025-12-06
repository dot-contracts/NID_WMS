using System;

namespace wms_android.Interfaces
{
    /// <summary>
    /// Interface for Vanstone (A90) device operations
    /// </summary>
    public interface IVanstonePosApi : IDisposable
    {
        /// <summary>
        /// Initialize the printer
        /// </summary>
        int PrintInit(int gray, int density, int mode, int otherOptions);

        /// <summary>
        /// Set the alignment for text printing
        /// </summary>
        int PrintSetAlign(int align);

        /// <summary>
        /// Set the font for text printing
        /// </summary>
        int PrintSetFont(sbyte width, sbyte height, sbyte style);

        /// <summary>
        /// Print text
        /// </summary>
        int PrintStr(string text);

        /// <summary>
        /// Print a QR code
        /// </summary>
        int PrintQrCode(string content, int width, int height);

        /// <summary>
        /// Print a bitmap image
        /// </summary>
        int PrintBitmap(byte[] bitmapData, int width, int height);

        /// <summary>
        /// Feed paper by a number of lines
        /// </summary>
        int PrintFeedPaper(int lines);

        /// <summary>
        /// Start the printing process
        /// </summary>
        int PrintStart();

        /// <summary>
        /// Gets the current printer status
        /// </summary>
        int GetPrinterStatus();

        /// <summary>
        /// Initialize the scanner
        /// </summary>
        int ScannerInit(int mode);

        /// <summary>
        /// Start the scanning process
        /// </summary>
        int StartScan();

        /// <summary>
        /// Stop the scanning process
        /// </summary>
        int StopScan();

        /// <summary>
        /// Get the current scanner status
        /// </summary>
        int GetScannerStatus();

        /// <summary>
        /// Event raised when a barcode is scanned
        /// </summary>
        event EventHandler<BarcodeScannedEventArgs> OnBarcodeScanned;
    }
} 