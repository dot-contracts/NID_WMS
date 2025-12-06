using System;
using System.Threading.Tasks;

namespace wms_android.Interfaces // Adjust namespace if needed
{
    /// <summary>
    /// Interface for device-specific barcode/QR code scanning services.
    /// </summary>
    public interface IScannerService : IDisposable
    {
        /// <summary>
        /// Initializes the scanner hardware/SDK components.
        /// </summary>
        /// <returns>True if initialization was successful, otherwise false.</returns>
        bool InitializeScanner();

        /// <summary>
        /// Asynchronously performs a scan operation.
        /// </summary>
        /// <param name="timeout">Maximum time to wait for a scan.</param>
        /// <returns>The scanned data as a string, or null if timed out or failed.</returns>
        Task<string?> ScanAsync(TimeSpan timeout);

        /// <summary>
        /// Cancels the current scan operation.
        /// </summary>
        void CancelScan();

        /// <summary>
        /// Closes the scanner connection and releases resources.
        /// </summary>
        void CloseScanner();

        // Optional: Add other methods if needed, e.g., SetScanMode, GetStatus, etc.
    }
} 