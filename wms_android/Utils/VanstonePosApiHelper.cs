using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Android.Graphics;
using Com.Vanstone.Trans.Api;
using Android.Content;
using wms_android.Interfaces;
using wms_android.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Android.OS;

namespace wms_android.Utils
{
    /// <summary>
    /// Implementation of printer functionality for A90 device using VanstonePosSdk
    /// </summary>
    public class VanstonePosApiHelper : Java.Lang.Object, IPosApiHelper, IVanstonePosApi, IServiceConnection
    {
        private static VanstonePosApiHelper _instance;
        private bool _isPrinterInitialized = false;
        private Context _context;
        private const string TAG = "VanstonePosApiHelper";
        private readonly object _initLock = new object(); // Lock for thread-safe initialization
        private int _initializationAttempts = 0;
        private const int MAX_INIT_ATTEMPTS = 5;

        public static VanstonePosApiHelper Instance => _instance ??= new VanstonePosApiHelper();

        private VanstonePosApiHelper()
        {
            _context = Android.App.Application.Context;
            System.Diagnostics.Debug.WriteLine($"{TAG}: Instance created");
        }

        public int PrintInit(int gray, int density, int mode, int otherOptions)
        {
            try
            {
                // Use lock to prevent concurrent initialization
                lock (_initLock)
                {
                    if (_isPrinterInitialized)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Printer already initialized");
                        return 0;
                    }

                    _initializationAttempts++;
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing printer (attempt {_initializationAttempts})...");

                    if (_initializationAttempts > MAX_INIT_ATTEMPTS)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Maximum initialization attempts reached");
                        return -1;
                    }

                    // Get the context
                    _context = Android.App.Application.Context;
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Context acquired: {_context != null}");

                    // Set context for the printer API (must be done before any other printer operations)
                    PrinterApi.SetContext(_context);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Context set for PrinterApi");

                    // Open the printer
                    int result = PrinterApi.PrnOpen_Api("", _context);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrnOpen_Api result: {result}");

                    if (result != 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Failed to open printer: {result}");
                        return result;
                    }

                    // Clear the printer buffer
                    PrinterApi.PrnClrBuff_Api();
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer buffer cleared");

                    // Set the gray level - higher values (1-10) make printing darker
                    // For PrnSetGray_Api, range is typically 1-10
                    int grayLevel = Math.Max(1, Math.Min(gray, 10)); // Ensure gray is between 1 and 10
                    PrinterApi.PrnSetGray_Api(grayLevel);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Gray level set to {grayLevel}");
                    
                    // Set default font based on device type
                    SetDefaultFont();
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Default font configured for device type");

                    _isPrinterInitialized = true;
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer initialization successful");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error in PrintInit: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        private void EnsurePrinterServiceInitialized()
        {
            // Set the context for the printer API
            _context = Android.App.Application.Context;
            if (_context == null)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error - Context is null");
                return;
            }
            
            PrinterApi.SetContext(_context);
            System.Diagnostics.Debug.WriteLine($"{TAG}: Context set for printer service");
        }

        private void EnsurePrinterHandlerInitialized()
        {
            try
            {
                if (!_isPrinterInitialized)
                {
                    // Initialize the printer using the public API only
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer not initialized, initializing...");
                    PrintInit(4, 100, 0, 0); // Use default values
                }
                
                // Set context for the printer API (safety measure)
                _context = Android.App.Application.Context;
                PrinterApi.SetContext(_context);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error in EnsurePrinterHandlerInitialized: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Sets the default font settings appropriate for the device.
        /// For A90, uses larger font settings consistent with the direct test approach.
        /// </summary>
        /// <returns>0 if successful, -1 if there was an error</returns>
        public int SetDefaultFont()
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                bool isA90 = Android.OS.Build.Model.ToUpper().Contains("A90");
                
                if (isA90)
                {
                    // For A90 devices, use larger font like in the direct test
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Setting default font for A90 device");
                    
                    // Darker print (gray level 10 - maximum)
                    PrinterApi.PrnSetGray_Api(10);
                    
                    // Larger font (24x24) similar to the direct test
                    PrinterApi.PrnFontSet_Api(24, 24, 0);
                }
                else
                {
                    // For other devices, use standard font
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Setting default font for standard device");
                    PrinterApi.PrnFontSet_Api(8, 8, 0); // Default size
                }
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Default font set");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: SetDefaultFont Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintSetAlign(int align)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                // Align: 0=left, 1=center, 2=right
                System.Diagnostics.Debug.WriteLine($"{TAG}: Setting alignment to {align}");
                
                // Use BtPrinterApi.PrnAlignSet_Api for proper alignment
                BtPrinterApi.PrnAlignSet_Api(align);
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: SetAlign completed");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetAlign Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintSetFont(sbyte width, sbyte height, sbyte style)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Setting font - width: {width}, height: {height}, style: {style:X2}");
                
                // Convert to integers for PrnFontSet_Api
                int widthInt = width;
                int heightInt = height;
                int styleInt = style;
                
                // Use available font settings method
                PrinterApi.PrnFontSet_Api(widthInt, heightInt, styleInt);
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: SetFont completed");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintStr(string text)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing text: {(text.Length > 30 ? text.Substring(0, 30) + "..." : text)}");
                int result = PrinterApi.PrnStr_Api(text);
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr result={result}");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintQrCode_Cut(string content, int width, int height, string format)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Attempting to print QR code: '{content}', width={width}, height={height}");
                
                // Calculate alignment based on width (0=left, 1=center, 2=right)
                int alignment = 1; // Default to center alignment (1)
                
                // Set QR code size - height should be between 100-500 for best results
                int qrHeight = Math.Min(Math.Max(height, 150), 500); // Clamp height between 150-500
                
                bool success = false;
                
                // Try multiple approaches to print QR code, with each one wrapped in its own try/catch
                try
                {
                    // Method 1: Use BtPrinterApi - this is the safest option
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Trying to print QR code using BtPrinterApi.PrnQrcode_Api");
                    BtPrinterApi.PrnQrcode_Api(content);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: QR code printed successfully with BtPrinterApi.PrnQrcode_Api");
                    success = true;
                }
                catch (Exception ex1)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: BtPrinterApi QR code method failed: {ex1.Message}");
                    
                    // Method 2: Try string-based fallback
                    try
                    {
                        // If QR code printing fails, print the content as text in a box
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Falling back to text-based QR representation");
                        PrintSetAlign(1); // Center
                        PrintStr("\n--- QR CODE ---\n");
                        PrintStr($"{content}\n");
                        PrintStr("---------------\n");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Printed text-based QR code alternative");
                        success = true;
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Text-based QR fallback failed: {ex2.Message}");
                        return -1;
                    }
                }
                
                return success ? 0 : -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode_Cut Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        /// <summary>
        /// Print QR code (IVanstonePosApi interface method)
        /// </summary>
        public int PrintQrCode(string content, int width, int height)
        {
            // Delegate to the existing implementation with default format
            return PrintQrCode_Cut(content, width, height, "QR_CODE");
        }

        /// <summary>
        /// Print bitmap from byte array (IVanstonePosApi interface method)
        /// </summary>
        public int PrintBitmap(byte[] bitmapData, int width, int height)
        {
            try
            {
                // Convert byte array to Bitmap
                var bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
                if (bitmap == null)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Failed to create bitmap from byte array");
                    return -1;
                }

                // Use the existing PrintBitmap method
                var result = PrintBitmap(bitmap);
                
                // Clean up bitmap
                bitmap.Dispose();
                
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap(byte[]) Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintFeedPaper(int lines)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Feeding paper: {lines} lines");
                
                // Use PrnStep_Api which seems to be for paper feeding
                PrinterApi.PrnStep_Api(lines);
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: FeedPaper completed");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintFeedPaper Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintStart()
        {
            try
            {
                EnsurePrinterHandlerInitialized();
                
                // Check printer status before starting print job
                int status = PrinterApi.PrnStatus_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printer status before PrintStart: {status}");
                
                if (status != 0 && status != 136) // 0x00 or 0x88 = ready
                {
                    // Handle specific error conditions
                    if (status == 2) // 0x02 = out of paper
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Cannot print - out of paper");
                        return 2;
                    }
                    else if (status == 3) // 0x03 = overheated
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Cannot print - printer overheated");
                        return 3;
                    }
                    else if (status == 170) // 0xAA = busy
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Cannot print - printer busy");
                        return 170;
                    }
                }
                
                // Start print job
                int result = PrinterApi.PrnStart_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStart result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStart Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }
        
        private string GetPrinterErrorMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 0: // 0x00
                    return "Printer operation successful.";
                case 136: // 0x88
                    return "Printer ready.";
                case 2: // 0x02
                    return "Out of paper - please load paper.";
                case 3: // 0x03
                    return "Printer overheated - please allow it to cool down.";
                case 170: // 0xAA
                    return "Printer is busy - please wait and try again.";
                default:
                    return $"Unknown printer status code: {errorCode} (0x{errorCode:X2})";
            }
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer service connected: {name?.ClassName}");
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer service disconnected: {name?.ClassName}");
        }

        public int PrintBitmap(Bitmap bitmap)
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing bitmap: {bitmap.Width}x{bitmap.Height}");
                
                // Use PrnLogo_Api for bitmap printing
                PrinterApi.PrnLogo_Api(bitmap);
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap completed");
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        /// <summary>
        /// Gets the current printer status (IVanstonePosApi interface method)
        /// </summary>
        public int GetPrinterStatus()
        {
            try
            {
                // Ensure the printer handler is initialized
                EnsurePrinterHandlerInitialized();
                
                // Use PrinterApi.PrnStatus_Api() to get status
                int status = PrinterApi.PrnStatus_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printer status: {status}");
                
                return status;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetPrinterStatus Error: {ex.Message}");
                return -1; // Return error status
            }
        }

        /// <summary>
        /// Checks if printer paper is ready
        /// </summary>
        public bool IsPaperReady()
        {
            try
            {
                // Directly check printer status
                int status = PrinterApi.PrnStatus_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Paper ready check - status: {status}");
                
                // 0x00 or 0x88 (136) = success/ready
                // 0x02 (2) = out of paper
                // 0x03 (3) = printer overheated
                // 0xAA (170) = printer busy
                
                return status == 0 || status == 136; // 0x00 or 0x88 = success
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error checking paper status: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Gets a human-readable message about the printer's current status
        /// </summary>
        public string GetStatusMessage()
        {
            try
            {
                int status = PrinterApi.PrnStatus_Api();
                switch (status)
                {
                    case 0: // 0x00
                        return "Printer ready.";
                    case 136: // 0x88
                        return "Printer ready.";
                    case 2: // 0x02
                        return "Please load paper in the printer.";
                    case 3: // 0x03
                        return "Printer overheated - please allow it to cool down.";
                    case 170: // 0xAA
                        return "Printer is busy - please wait and try again.";
                    default:
                        return GetPrinterErrorMessage(status);
                }
            }
            catch (Exception ex)
            {
                return $"Error checking printer status: {ex.Message}";
            }
        }

        public bool IsPrinterReady()
        {
            try
            {
                int status = PrinterApi.PrnStatus_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printer ready check - status: {status}");
                
                // 0x00 or 0x88 (136) = success/ready
                return status == 0 || status == 136;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error checking printer readiness: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Event raised when a barcode is scanned (IVanstonePosApi interface)
        /// </summary>
        public event EventHandler<BarcodeScannedEventArgs> OnBarcodeScanned;

        /// <summary>
        /// Initialize the scanner (IVanstonePosApi interface method)
        /// </summary>
        public int ScannerInit(int mode)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Scanner initialization requested (mode: {mode})");
                // Scanner initialization for A90 would typically be handled by the scanner service
                // This is a placeholder implementation
                return 0; // Return success
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: ScannerInit Error: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Start the scanning process (IVanstonePosApi interface method)
        /// </summary>
        public int StartScan()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Start scan requested");
                // Scanner control for A90 would typically be handled by the scanner service
                // This is a placeholder implementation
                return 0; // Return success
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: StartScan Error: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Stop the scanning process (IVanstonePosApi interface method)
        /// </summary>
        public int StopScan()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stop scan requested");
                // Scanner control for A90 would typically be handled by the scanner service
                // This is a placeholder implementation
                return 0; // Return success
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: StopScan Error: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Get the current scanner status (IVanstonePosApi interface method)
        /// </summary>
        public int GetScannerStatus()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Scanner status requested");
                // Scanner status for A90 would typically be handled by the scanner service
                // This is a placeholder implementation
                return 0; // Return ready status
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetScannerStatus Error: {ex.Message}");
                return -1;
            }
        }
    }
} 