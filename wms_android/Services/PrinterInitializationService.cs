using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using Com.Vanstone.Trans.Api;
using wms_android.Interfaces;
using wms_android.Utils;

namespace wms_android.Services
{
    public static class PrinterInitializationService
    {
        private static object _posApiHelper;
        private static bool _isInitializing = false;
        private static readonly object _lockObject = new object();
        private const string TAG = "PrinterInit";
        private const int MAX_RETRY_COUNT = 5;
        private static bool _contextSet = false;
        public static bool IsInitialized { get; private set; } = false;
        private static readonly bool _isA90Device = PrinterFactory.IsA90Device();
        private static readonly bool _isCS30Device = PrinterFactory.IsCS30Device();

        /// <summary>
        /// Initialize the printer service - main entry point
        /// </summary>
        public static void Initialize()
        {
            lock (_lockObject)
            {
                if (IsInitialized)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer already initialized, skipping initialization");
                    return;
                }

                if (_isInitializing)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Initialization already in progress, waiting...");
                    return;
                }

                _isInitializing = true;
                System.Diagnostics.Debug.WriteLine($"{TAG}: Starting printer initialization. Device type: {(PrinterFactory.IsA90Device() ? "A90" : PrinterFactory.IsCS30Device() ? "CS30" : "Unknown")}");

                try
                {
                    // Set Android context (important for SDK functionality)
                    if (!_contextSet)
                    {
                        try
                        {
                            var context = Android.App.Application.Context;
                            if (context == null)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: ERROR - Android context is null");
                                // Continue anyway - printer might still initialize
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Error getting Android context: {ex.Message}");
                            // Continue anyway - printer might still initialize
                        }
                    }

                    // Get the appropriate printer implementation
                    _posApiHelper = PrinterFactory.GetPrinterImplementation();
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Got printer implementation: {_posApiHelper?.GetType().Name}");

                    if (_posApiHelper is VanstonePosApiHelper)
                    {
                        // For A90 device, perform additional initialization steps
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Detected Vanstone A90 device, using specialized initialization");
                        InitializeVanstonePrinter();
                    }
                    else if (_posApiHelper is CS30PosApiHelper)
                    {
                        // For CS30 device, use CS30-specific initialization
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Detected CS30 device, using specialized initialization");
                        InitializeCS30Printer();
                    }
                    else
                    {
                        // For other devices, use standard initialization
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Using standard printer initialization");
                        int retryCount = 0;
                        int result;
                        
                        do {
                            if (_posApiHelper is IPosApiHelper vanstonePrinter)
                            {
                                result = vanstonePrinter.PrintInit(2, 2, 0, 0);
                            }
                            else if (_posApiHelper is ICs30PosApi cs30Printer)
                            {
                                result = cs30Printer.PrintInit(2, 2, 0, 0);
                            }
                            else
                            {
                                result = -1; // Unknown printer type
                            }
                            
                            if (result != 0)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Standard printer initialization failed with result: {result}, attempt {retryCount+1} of {MAX_RETRY_COUNT}");
                                Task.Delay(1000).Wait(); // Wait before retrying
                            }
                        } while (result != 0 && ++retryCount < MAX_RETRY_COUNT);
                        
                        IsInitialized = (result == 0);
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Standard printer initialization result: {result}, IsInitialized: {IsInitialized}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: ERROR initializing printer: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                    IsInitialized = false;
                }
                finally
                {
                    _isInitializing = false;
                }
            }
        }

        private static void InitializeVanstonePrinter()
        {
            try
            {
                Context context = Android.App.Application.Context;
                System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing Vanstone A90 printer...");
                
                // CRITICAL: Initialize the system API first
                try
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Calling SystemInit_Api to initialize hardware");
                    SystemApi.SystemInit_Api(0, new byte[0], context);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: System initialization completed");
                    
                    // Give the system a moment to initialize
                    Task.Delay(1000).Wait();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: System initialization error: {ex.Message}");
                    // Continue anyway - printer might still initialize
                }
                
                // Set the context first - this is critical
                PrinterApi.SetContext(context);
                System.Diagnostics.Debug.WriteLine($"{TAG}: Context set");
                _contextSet = true;
                
                // Wait a moment to ensure context registration
                Task.Delay(200).Wait();
                
                // Simple open approach - retry a few times
                int retryCount = 0;
                bool success = false;

                while (!success && retryCount < MAX_RETRY_COUNT)
                {
                    try
                    {
                        retryCount++;
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Opening printer (attempt {retryCount}/{MAX_RETRY_COUNT})...");
                        
                        // Most basic approach from sample - open, clear buffer, set gray level
                        int result = PrinterApi.PrnOpen_Api("", context);
                        System.Diagnostics.Debug.WriteLine($"{TAG}: PrnOpen_Api result: {result}");
                        
                        if (result == 0)
                        {
                            // Clear buffer
                            try 
                            {
                                PrinterApi.PrnClrBuff_Api();
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Printer buffer cleared");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Error clearing buffer: {ex.Message}");
                            }
                            
                            // Set gray level
                            try
                            {
                                int grayResult = PrinterApi.PrnSetGray_Api(5); // Use 5 as in sample
                                System.Diagnostics.Debug.WriteLine($"{TAG}: PrnSetGray_Api result: {grayResult}");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Error setting gray level: {ex.Message}");
                            }
                            
                            success = true;
                            IsInitialized = true;
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer initialized successfully");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer open failed with code {result}");
                            
                            if (retryCount < MAX_RETRY_COUNT)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Will retry initialization");
                                Task.Delay(1000).Wait();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Error during printer initialization (attempt {retryCount}): {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                        
                        if (retryCount < MAX_RETRY_COUNT)
                        {
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Waiting before retry...");
                            Task.Delay(1000).Wait();
                        }
                    }
                }
                
                // Result after all attempts
                System.Diagnostics.Debug.WriteLine($"{TAG}: Vanstone printer initialization completed. Success: {success}, IsInitialized: {IsInitialized}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: ERROR initializing Vanstone printer: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                IsInitialized = false;
            }
        }
        
        private static void InitializeCS30Printer()
        {
            try
            {
                Context context = Android.App.Application.Context;
                System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing CS30 printer...");
                
                // Set up for CS30 printer - this will need to be updated with actual CS30 SDK calls
                // when the CS30 printer implementation is completed
                
                // Simple open approach - retry a few times
                int retryCount = 0;
                bool success = false;

                while (!success && retryCount < MAX_RETRY_COUNT)
                {
                    try
                    {
                        retryCount++;
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing CS30 printer (attempt {retryCount}/{MAX_RETRY_COUNT})...");
                        
                        // Call the generic PrintInit method which has placeholders for CS30-specific code
                        int result = -1;
                        if (_posApiHelper is ICs30PosApi cs30Printer)
                        {
                            result = cs30Printer.PrintInit(4, 100, 0, 0);
                        }
                        else if (_posApiHelper is IPosApiHelper vanstonePrinter)
                        {
                            result = vanstonePrinter.PrintInit(4, 100, 0, 0);
                        }
                        
                        if (result == 0)
                        {
                            success = true;
                            IsInitialized = true;
                            System.Diagnostics.Debug.WriteLine($"{TAG}: CS30 printer initialized successfully");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"{TAG}: CS30 printer initialization failed with code {result}");
                            
                            if (retryCount < MAX_RETRY_COUNT)
                            {
                                System.Diagnostics.Debug.WriteLine($"{TAG}: Will retry initialization");
                                Task.Delay(1000).Wait();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Error during CS30 printer initialization (attempt {retryCount}): {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                        
                        if (retryCount < MAX_RETRY_COUNT)
                        {
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Waiting before retry...");
                            Task.Delay(1000).Wait();
                        }
                    }
                }
                
                // Result after all attempts
                System.Diagnostics.Debug.WriteLine($"{TAG}: CS30 printer initialization completed. Success: {success}, IsInitialized: {IsInitialized}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: ERROR initializing CS30 printer: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                IsInitialized = false;
            }
        }
        
        /// <summary>
        /// Get the appropriate printer helper instance, initializing if needed
        /// </summary>
        public static object GetPrinterHelper()
        {
            // Use a lock to prevent race conditions
            lock (_lockObject)
            {
                string deviceModel = Android.OS.Build.Model;
                System.Diagnostics.Debug.WriteLine($"{TAG}: Getting printer implementation for device: {deviceModel}");
                
                // Initialize if needed
                if (!IsInitialized)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer not initialized, initializing now");
                    Initialize();
                }
                
                // Return the appropriate implementation
                return PrinterFactory.GetPrinterImplementation();
            }
        }
        
        /// <summary>
        /// Reset the printer service and reinitialize
        /// </summary>
        public static bool ResetPrinter()
        {
            System.Diagnostics.Debug.WriteLine($"{TAG}: Resetting printer...");
            IsInitialized = false;
            _isInitializing = false;
            _contextSet = false;
            Initialize();
            return IsInitialized;
        }
        
        /// <summary>
        /// Get the current printer status information
        /// </summary>
        public static string GetPrinterStatus()
        {
            try
            {
                if (_posApiHelper == null)
                {
                    return "Printer helper not initialized";
                }
                
                if (!IsInitialized)
                {
                    return "Printer not initialized";
                }
                
                // Simple status display - could be enhanced with printer status code
                int status = -1;
                try
                {
                    status = PrinterApi.PrnStatus_Api();
                    
                    // Interpret the status code for known values
                    string statusMessage = "";
                    if (status == 0 || status == 136) // Both 0 and 136 (0x88 in hex) indicate ready status
                    {
                        statusMessage = "Ready";
                    }
                    else if (status == 2) // 0x02 in hex - out of paper
                    {
                        statusMessage = "Out of paper - please load paper";
                    }
                    else if (status == 3) // 0x03 in hex - overheated
                    {
                        statusMessage = "Printer overheated - please allow it to cool down";
                    }
                    else if (status == 170) // 0xaa in hex - printer busy
                    {
                        statusMessage = "Printer is busy - please wait and try again";
                    }
                    else
                    {
                        statusMessage = $"Not ready (Code: {status})";
                    }
                    
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer status check: {status}");
                    return $"Printer initialized: {IsInitialized}, Type: {_posApiHelper.GetType().Name}, Status: {statusMessage}";
                }
                catch (Exception ex)
                {
                    return $"Printer initialized: {IsInitialized}, Type: {_posApiHelper.GetType().Name}, Error checking status: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error getting printer status: {ex.Message}";
            }
        }
        
        /// <summary>
        /// Do a simple test print to verify printer functionality
        /// </summary>
        public static bool TestPrinter()
        {
            if (!IsInitialized)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Cannot test - printer not initialized");
                return false;
            }
            
            try
            {
                Context context = Android.App.Application.Context;
                
                // Based on sample project pattern
                PrinterApi.PrnClrBuff_Api();
                PrinterApi.PrnFontSet_Api(24, 24, 0);
                PrinterApi.PrnSetGray_Api(5);
                
                // Print test pattern
                PrinterApi.PrintSetAlign_Api(1); // Center alignment (0=left, 1=center, 2=right)
                PrinterApi.PrnStr_Api("===== PRINTER TEST =====");
                PrinterApi.PrnStr_Api("WMS Android App");
                PrinterApi.PrnStr_Api(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                PrinterApi.PrnStr_Api("======================");
                PrinterApi.PrnStr_Api("\n\n");
                
                // Start the print job
                int result = PrinterApi.PrnStart_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Test print result: {result}");
                
                return result == 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error during test print: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Direct test method that closely follows the sample project implementation
        /// and uses SystemInit_Api for proper hardware initialization
        /// </summary>
        public static bool TestDirectPrinting()
        {
            try
            {
                // Direct implementation like sample project
                var context = Android.App.Application.Context;
                
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Starting direct test");
                
                // CRITICAL: Initialize the system API first - this is required for hardware access
                try
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Calling SystemInit_Api to initialize hardware");
                    SystemApi.SystemInit_Api(0, new byte[0], context);
                    System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - System initialization completed");
                    
                    // Give the system a moment to initialize
                    Task.Delay(1000).Wait();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - System initialization error: {ex.Message}");
                    return false;
                }
                
                // First, set context - absolutely required step
                PrinterApi.SetContext(context);
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Context set");
                
                // Open printer directly with context
                int result = PrinterApi.PrnOpen_Api("", context);
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - PrnOpen_Api direct result: {result}");
                
                if (result != 0)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - PrnOpen_Api failed with code: {result}");
                    return false;
                }
                
                // Clear buffer
                PrinterApi.PrnClrBuff_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Buffer cleared");
                
                // Set gray level
                PrinterApi.PrnSetGray_Api(5);
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Gray level set");
                
                // Set font
                PrinterApi.PrnFontSet_Api(24, 24, 0);
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Font set");
                
                // Print text
                PrinterApi.PrintSetAlign_Api(1);
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Alignment set");
                
                PrinterApi.PrnStr_Api("=== SYSTEM INIT TEST ===");
                PrinterApi.PrnStr_Api("With SystemInit_Api call");
                PrinterApi.PrnStr_Api(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                PrinterApi.PrnStr_Api("\n\n");
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Text added");
                
                // Start print
                result = PrinterApi.PrnStart_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting - Direct print result: {result}");
                
                return result == 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: TestDirectPrinting stack trace: {ex.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Prints a sample receipt based on the provided template format
        /// </summary>
        /// <returns>True if printing was successful, false otherwise</returns>
        public static bool PrintReceiptSample()
        {
            if (!IsInitialized)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Cannot print receipt - printer not initialized");
                return false;
            }
            
            try
            {
                Context context = Android.App.Application.Context;
                
                // Clear buffer and set basic formatting
                PrinterApi.PrnClrBuff_Api();
                PrinterApi.PrnSetGray_Api(5);
                
                // ======== HEADER - COMPANY INFO ========
                // Title - Company name in large font
                PrinterApi.PrnFontSet_Api(32, 32, 1); // Larger, bold font for company name
                PrinterApi.PrintSetAlign_Api(1); // Center alignment
                PrinterApi.PrnStr_Api("WMS EXPRESS");
                
                // Normal sized text for address details
                PrinterApi.PrnFontSet_Api(24, 24, 0); // Normal font
                PrinterApi.PrintSetAlign_Api(1); // Center alignment
                PrinterApi.PrnStr_Api("123 Logistics Way");
                PrinterApi.PrnStr_Api("Warehouse District");
                PrinterApi.PrnStr_Api("Contact: +1-555-123-4567");
                PrinterApi.PrnStr_Api("-------------------------------");
                
                // ======== RECEIPT DETAILS ========
                // Date and receipt number
                PrinterApi.PrintSetAlign_Api(0); // Left alignment
                PrinterApi.PrnStr_Api($"Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
                PrinterApi.PrnStr_Api($"Receipt #: WMS-{DateTime.Now.ToString("yyyyMMdd")}-00123");
                PrinterApi.PrnStr_Api($"Operator: JOHN DOE");
                PrinterApi.PrnStr_Api("-------------------------------");
                
                // ======== SHIPMENT DETAILS ========
                PrinterApi.PrnFontSet_Api(28, 28, 1); // Slightly larger bold font for section title
                PrinterApi.PrintSetAlign_Api(1); // Center alignment
                PrinterApi.PrnStr_Api("SHIPMENT DETAILS");
                
                PrinterApi.PrnFontSet_Api(24, 24, 0); // Back to normal font
                PrinterApi.PrintSetAlign_Api(0); // Left alignment
                PrinterApi.PrnStr_Api("Tracking #: TRK12345678901234");
                PrinterApi.PrnStr_Api("Destination: 456 Delivery St.");
                PrinterApi.PrnStr_Api("Recipient: Jane Smith");
                PrinterApi.PrnStr_Api("Package Type: Standard");
                PrinterApi.PrnStr_Api("Weight: 2.5 kg");
                PrinterApi.PrnStr_Api("-------------------------------");
                
                // ======== PRICING SECTION ========
                PrinterApi.PrintSetAlign_Api(0); // Left alignment for the data
                PrinterApi.PrnStr_Api("Service Fee:           $15.00");
                PrinterApi.PrnStr_Api("Packaging:              $2.50");
                PrinterApi.PrnStr_Api("Insurance:              $3.00");
                PrinterApi.PrnStr_Api("-------------------------------");
                
                // Total in bold
                PrinterApi.PrnFontSet_Api(28, 28, 1); // Slightly larger bold font for total
                PrinterApi.PrnStr_Api("TOTAL:                $20.50");
                
                // ======== FOOTER ========
                PrinterApi.PrnFontSet_Api(24, 24, 0); // Normal font
                PrinterApi.PrintSetAlign_Api(1); // Center alignment
                PrinterApi.PrnStr_Api("-------------------------------");
                PrinterApi.PrnStr_Api("Thank you for choosing WMS Express!");
                PrinterApi.PrnStr_Api("Visit us at: www.wmsexpress.com");
                
                // Add QR code for tracking
                PrinterApi.PrnStr_Api("Scan to track your package:");
                try {
                    // Try to print QR code if the device supports it
                    BtPrinterApi.PrnQrcode_Api("TRK12345678901234");
                } catch (Exception ex) {
                    // If QR code printing fails, print a fallback message
                    System.Diagnostics.Debug.WriteLine($"{TAG}: QR code printing failed: {ex.Message}");
                    PrinterApi.PrnStr_Api("[QR Code: TRK12345678901234]");
                }
                
                // Add some space at the end
                PrinterApi.PrnStr_Api("\n\n");
                
                // Start the print job
                int result = PrinterApi.PrnStart_Api();
                System.Diagnostics.Debug.WriteLine($"{TAG}: Sample receipt print result: {result}");
                
                return result == 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Error printing sample receipt: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return false;
            }
        }
    }
    
    // Service connection class to bind to the printer service
    public class PrinterServiceConnection : Java.Lang.Object, Android.Content.IServiceConnection
    {
        private const string TAG = "PrinterInit";
        
        public void OnServiceConnected(Android.Content.ComponentName name, Android.OS.IBinder service)
        {
            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer service connected: {name?.ClassName}");
        }

        public void OnServiceDisconnected(Android.Content.ComponentName name)
        {
            System.Diagnostics.Debug.WriteLine($"{TAG}: Printer service disconnected: {name?.ClassName}");
        }
    }
} 