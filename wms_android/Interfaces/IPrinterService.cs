using System.Threading.Tasks;
using wms_android.shared.Models;

namespace wms_android.Interfaces
{
    /// <summary>
    /// Interface for device-specific receipt printing services.
    /// </summary>
    public interface IPrinterService
    {
        /// <summary>
        /// Initializes the printer hardware/SDK components.
        /// This might include system initialization if required by the SDK.
        /// </summary>
        /// <returns>True if initialization was successful, otherwise false.</returns>
        Task<bool> InitializePrinterAsync();

        /// <summary>
        /// Prints formatted text content for a receipt.
        /// </summary>
        /// <param name="textContent">The formatted text to print.</param>
        /// <returns>True if printing command was sent successfully, otherwise false.</returns>
        Task<bool> PrintTextAsync(string textContent);

        /// <summary>
        /// Prints a QR code with the specified data and dimensions.
        /// </summary>
        /// <param name="data">The data to encode in the QR code.</param>
        /// <param name="width">The width of the QR code in pixels.</param>
        /// <param name="height">The height of the QR code in pixels.</param>
        /// <returns>True if QR code printing command was sent successfully, otherwise false.</returns>
        Task<bool> PrintQRCodeAsync(string data, int width = 200, int height = 200);

        /// <summary>
        /// Starts the actual print job after all content has been prepared.
        /// This executes the printing of all prepared content.
        /// </summary>
        /// <returns>True if the print job was started successfully, otherwise false.</returns>
        Task<bool> StartPrintJobAsync();

        // TODO: Add methods for specific formatting or graphics if needed
        // Task<bool> PrintBarcodeAsync(string data, BarcodeType type);
        // Task<bool> PrintImageAsync(byte[] imageData);
        // Task<bool> FeedPaperAsync(int lines);
        // Task<bool> CutPaperAsync();

        /// <summary>
        /// Gets the current status of the printer (e.g., online, offline, out of paper).
        /// </summary>
        /// <returns>A string or enum representing the printer status.</returns>
        Task<string> GetPrinterStatusAsync(); // Status could be an enum later

        /// <summary>
        /// Closes the printer connection and releases resources.
        /// </summary>
        void ClosePrinter();
    }
} 