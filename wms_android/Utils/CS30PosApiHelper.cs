using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Android.Graphics;
using Android.Content;
using wms_android.Interfaces;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Android.OS;
using Com.Ctk.Sdk;
using Android.Util;

namespace wms_android.Utils
{
    /// <summary>
    /// Implementation of printer functionality for CS30 device using AndroidPosSdk
    /// </summary>
    public class CS30PosApiHelper : Java.Lang.Object, ICs30PosApi, IServiceConnection
    {
        private static CS30PosApiHelper _instance;
        private bool _isPrinterInitialized = false;
        private Context _context;
        private const string TAG = "CS30PosApiHelper";
        private readonly object _initLock = new object(); // Lock for thread-safe initialization
        private int _initializationAttempts = 0;
        private const int MAX_INIT_ATTEMPTS = 3; // Reduced max attempts slightly
        private Com.Ctk.Sdk.PosApiHelper _sdkHelperInstance;

        public static CS30PosApiHelper Instance => _instance ??= new CS30PosApiHelper();

        // Event for barcode scanning
        public event EventHandler<BarcodeScannedEventArgs> OnBarcodeScanned;

        /// <summary>
        /// Resets the initialization state to allow fresh attempts
        /// </summary>
        public void ResetInitializationState()
        {
            lock (_initLock)
            {
                _initializationAttempts = 0;
                _isPrinterInitialized = false;
                Log.Info(TAG, "Initialization state reset - allowing fresh attempts");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Initialization state reset - allowing fresh attempts");
            }
        }

        private CS30PosApiHelper()
        {
            try
        {
            _context = Android.App.Application.Context;
                if (_context == null)
                {
                    Log.Warn(TAG, "WARNING - Android Application Context is null");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: WARNING - Android Application Context is null");
                }
                
                // Try to get the SDK instance
                try
                {
            _sdkHelperInstance = Com.Ctk.Sdk.PosApiHelper.Instance;
            if (_sdkHelperInstance == null)
            {
                        Log.Error(TAG, "CRITICAL - Failed to get Com.Ctk.Sdk.PosApiHelper instance!");
                        Log.Error(TAG, "This likely means the CS30 SDK is not properly installed or the device is not a CS30");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: CRITICAL - Failed to get Com.Ctk.Sdk.PosApiHelper instance!");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: This likely means the CS30 SDK is not properly installed or the device is not a CS30");
                        // Don't throw exception here - let the calling code handle null instance
                    }
                    else
                    {
                        Log.Info(TAG, "Successfully obtained Com.Ctk.Sdk.PosApiHelper instance");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Successfully obtained Com.Ctk.Sdk.PosApiHelper instance");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(TAG, $"Exception getting CS30 SDK instance: {ex.Message}");
                    Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Exception getting CS30 SDK instance: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                    _sdkHelperInstance = null;
                }
                
                Log.Info(TAG, $"Instance created, SDK Helper Instance: {(_sdkHelperInstance != null)}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Instance created, SDK Helper Instance: {(_sdkHelperInstance != null)}");
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"Critical error in CS30PosApiHelper constructor: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Critical error in CS30PosApiHelper constructor: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                _sdkHelperInstance = null;
            }
        }

        public int PrintInit(int gray, int density, int mode, int otherOptions)
        {
            lock (_initLock) // Ensure thread safety during initialization
            {
                if (_isPrinterInitialized)
                {
                    Log.Debug(TAG, "Printer already initialized");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Printer already initialized");
                    return 0;
                }

                if (_sdkHelperInstance == null)
                {
                    Log.Error(TAG, "PrintInit Error - SDK Helper Instance is null");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintInit Error - SDK Helper Instance is null");
                    return -1;
                }

                _initializationAttempts++;
                Log.Info(TAG, $"Initialization attempt #{_initializationAttempts}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Initialization attempt #{_initializationAttempts}");
                
                if (_initializationAttempts > MAX_INIT_ATTEMPTS)
                {
                    Log.Error(TAG, $"Exceeded maximum initialization attempts ({MAX_INIT_ATTEMPTS})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Exceeded maximum initialization attempts ({MAX_INIT_ATTEMPTS})");
                    return -99; // Special error code for too many attempts
                }

                try
                {
                    Log.Info(TAG, "Initializing CS30 printer using AndroidPosSdk method");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing CS30 printer using AndroidPosSdk method");
                    
                    // Reset state before initialization
                    _isPrinterInitialized = false;
                    
                    // Step 1: Call the basic PrintInit() first (as per AndroidPosSdk documentation)
                    int basicInitResult = _sdkHelperInstance.PrintInit();
                    Log.Info(TAG, $"Basic PrintInit() result: {basicInitResult}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Basic PrintInit() result: {basicInitResult}");
                    
                    if (basicInitResult != 0)
                    {
                        string errorMessage = GetCS30ErrorMessage(basicInitResult);
                        Log.Error(TAG, $"Basic PrintInit() failed (Result Code: {basicInitResult}) - {errorMessage}");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: Basic PrintInit() failed (Result Code: {basicInitResult}) - {errorMessage}");
                        
                        // For critical errors that won't be fixed by retrying, stop trying
                        if (IsCriticalCS30Error(basicInitResult))
                        {
                            Log.Error(TAG, "Critical error detected - stopping retry attempts");
                            System.Diagnostics.Debug.WriteLine($"{TAG}: Critical error detected - stopping retry attempts");
                            _initializationAttempts = MAX_INIT_ATTEMPTS; // Prevent further retries
                        }
                        
                        return basicInitResult;
                    }
                    
                    // Step 2: Set gray density (1-5 range, where 1=lowest, 3=medium, 5=highest as per PrintSetGray documentation)
                    int grayLevel = Math.Max(1, Math.Min(gray, 5)); // Clamp to 1-5 range
                    int grayResult = _sdkHelperInstance.PrintSetGray(grayLevel);
                    Log.Info(TAG, $"PrintSetGray({grayLevel}) result: {grayResult}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetGray({grayLevel}) result: {grayResult}");
                    
                    if (grayResult != 0)
                    {
                        Log.Error(TAG, $"PrintSetGray failed (Result Code: {grayResult})");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetGray failed (Result Code: {grayResult})");
                        return grayResult;
                    }
                    
                    // Step 3: Set font (font height: 16 or 24, font width: 16 or 24, zoom: 0x00 or 0x33)
                    byte fontHeight = (byte)Math.Max(16, Math.Min(density, 24)); // Use density as font height, clamp to 16-24
                    byte fontWidth = fontHeight; // Keep width same as height for square fonts
                    byte fontZoom = (byte)(mode == 1 ? 0x33 : 0x00); // Use mode parameter for bold/normal
                    
                    int fontResult = _sdkHelperInstance.PrintSetFont((sbyte)fontHeight, (sbyte)fontWidth, (sbyte)fontZoom);
                    Log.Info(TAG, $"PrintSetFont({fontHeight}, {fontWidth}, 0x{fontZoom:X2}) result: {fontResult}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont({fontHeight}, {fontWidth}, 0x{fontZoom:X2}) result: {fontResult}");
                    
                    if (fontResult != 0)
                    {
                        Log.Error(TAG, $"PrintSetFont failed (Result Code: {fontResult})");
                        System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont failed (Result Code: {fontResult})");
                        return fontResult;
                    }
                    
                    // All initialization steps completed successfully
                    _isPrinterInitialized = true;
                    Log.Info(TAG, "CS30 printer successfully initialized using AndroidPosSdk method");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: CS30 printer successfully initialized using AndroidPosSdk method");
                    _initializationAttempts = 0; // Reset attempts counter on success
                    return 0;
                }
                catch (Exception ex)
                {
                    Log.Error(TAG, $"Error during printer initialization: {ex.Message}");
                    Log.Error(TAG, $"Stack Trace: {ex.StackTrace}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Error during printer initialization: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Stack Trace: {ex.StackTrace}");
                    return -1;
                }
            }
        }

        /// <summary>
        /// Gets a human-readable error message for CS30 error codes
        /// </summary>
        private string GetCS30ErrorMessage(int errorCode)
        {
            return errorCode switch
            {
                0 => "Success",
                -1 => "General error",
                -4001 => "PRINT BUSY",
                -4002 => "PRINT NOPAPER",
                -4003 => "PRINT DATAERR",
                -4004 => "PRINT FAULT",
                -4005 => "PRINT TOOHEAT",
                -4006 => "PRINT UNFINISHED",
                -4007 => "PRINT NOFONTLIB",
                -4008 => "PRINT BUFFOVERFLOW",
                -4009 => "PRINT SETFONTERR",
                -4010 => "PRINT GETFONTERR",
                -5555 => "Service not available or communication error",
                _ => $"Unknown error code: {errorCode}"
            };
        }

        /// <summary>
        /// Determines if an error code represents a critical error that won't be fixed by retrying
        /// </summary>
        private bool IsCriticalCS30Error(int errorCode)
        {
            return errorCode switch
            {
                -4002 => true,  // No paper - requires manual intervention
                -4004 => true,  // Printer fault - permanent issue
                -4005 => true,  // Overheat - requires cooldown time
                -4007 => true,  // No font library - configuration issue
                -4009 => true,  // Set font error - invalid font parameters
                -4010 => true,  // Get font error - font system issue
                -5555 => true,  // Service not available - permanent issue
                _ => false      // Other errors might be temporary
            };
        }

        /// <summary>
        /// Sets the default font settings appropriate for the device
        /// </summary>
        /// <returns>0 if successful, -1 if there was an error</returns>
        public int SetDefaultFont()
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "SetDefaultFont Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: SetDefaultFont Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            try
            {
                Log.Debug(TAG, "Setting default font for CS30 device");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Setting default font for CS30 device");
                // TODO: Determine the correct default width/height/style for CS30 from docs if needed.
                // Using 8,8,0 as a plausible default for now.
                sbyte width = 8;
                sbyte height = 8;
                sbyte style = 0; 
                int result = _sdkHelperInstance.PrintSetFont(width, height, style); 
                if (result == 0)
                {
                    Log.Debug(TAG, "Default font set successfully.");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Default font set successfully.");
                    return 0;
                }
                else
                {
                    Log.Error(TAG, $"SetDefaultFont failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: SetDefaultFont failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"SetDefaultFont Error: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: SetDefaultFont Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintSetAlign(int align)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintSetAlign Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetAlign Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            
            try
            {
                Log.Debug(TAG, $"Setting alignment to {align}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Setting alignment to {align}");
                int result = _sdkHelperInstance.PrintSetAlign(align);
                if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintSetAlign failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetAlign failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintSetAlign Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetAlign Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintSetFont(sbyte width, sbyte height, sbyte style)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintSetFont Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            try
            {
                Log.Debug(TAG, $"Setting font - width: {width}, height: {height}, style: {style:X2}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Setting font - width: {width}, height: {height}, style: {style:X2}");
                int result = _sdkHelperInstance.PrintSetFont(width, height, style);
                 if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintSetFont failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintSetFont Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintSetFont Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintStr(string text)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintStr Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    Log.Debug(TAG, "PrintStr called with null or empty text - skipping");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr called with null or empty text - skipping");
                    return 0;
                }

                // Clean up the text for CS30 printing compatibility
                string cleanText = text.Replace("\r\n", "\n").Replace("\r", "\n");
                
                // Log a truncated version for debugging
                string logText = cleanText.Length > 50 ? cleanText.Substring(0, 50) + "..." : cleanText;
                Log.Debug(TAG, $"Printing text ({cleanText.Length} chars): {logText.Replace("\n", "\\n")}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing text ({cleanText.Length} chars): {logText.Replace("\n", "\\n")}");
                
                int result = _sdkHelperInstance.PrintStr(cleanText);
                
                 if (result == 0)
                {
                    Log.Debug(TAG, "PrintStr completed successfully");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr completed successfully");
                return 0;
                }
                else
                {
                    string errorMessage = GetCS30ErrorMessage(result);
                    Log.Error(TAG, $"PrintStr failed (Result Code: {result}) - {errorMessage}");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr failed (Result Code: {result}) - {errorMessage}");
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintStr Error: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStr Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stack trace: {ex.StackTrace}");
                return -1;
            }
        }

        public int PrintQrCode_Cut(string content, int width, int height, string format)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintQrCode_Cut Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode_Cut Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            
            try
            {
                Log.Debug(TAG, $"Printing QR code: {content}, width={width}, height={height}, format={format}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing QR code: {content}, width={width}, height={height}, format={format}");
                // Note: The generated SDK has PrintQrCode_Cut(string p0, int p1, int p2, string p3)
                // Assuming parameters match: content, width, height, format
                int result = _sdkHelperInstance.PrintQrCode_Cut(content, width, height, format); 
                 if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintQrCode_Cut failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode_Cut failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintQrCode_Cut Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode_Cut Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintFeedPaper(int lines)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintFeedPaper Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintFeedPaper Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            try
            {
                Log.Debug(TAG, $"Feeding paper: {lines} lines");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Feeding paper: {lines} lines");
                int result = _sdkHelperInstance.PrintFeedPaper(lines);
                 if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintFeedPaper failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintFeedPaper failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintFeedPaper Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintFeedPaper Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintStart()
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintStart Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStart Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            try
            {
                Log.Debug(TAG, "Starting print job");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Starting print job");
                int result = _sdkHelperInstance.PrintStart();
                 if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintStart failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStart failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintStart Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintStart Error: {ex.Message}");
                return -1;
            }
        }

        public int PrintBitmap(Bitmap bitmap)
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 Log.Error(TAG, "PrintBitmap Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap Error - Printer not initialized or SDK instance null.");
                 return -1;
             }
            try
            {
                 Log.Debug(TAG, $"Printing bitmap: {bitmap?.Width}x{bitmap?.Height}");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: Printing bitmap: {bitmap?.Width}x{bitmap?.Height}");
                int result = _sdkHelperInstance.PrintBmp(bitmap);
                 if (result == 0)
                {
                return 0;
                }
                else
                {
                    Log.Error(TAG, $"PrintBitmap failed (Result Code: {result})");
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"PrintBitmap Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap Error: {ex.Message}");
                return -1;
            }
        }

        public bool IsPaperReady()
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                Log.Error(TAG, "IsPaperReady Error - Printer not initialized or SDK instance null.");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: IsPaperReady Error - Printer not initialized or SDK instance null.");
                 return false;
             }
            
            try
            {
                int status = _sdkHelperInstance.PrintCheckStatus();
                // TODO: Determine the status code for "No Paper" from CS30 documentation.
                // Assuming non-zero might mean error or no paper for now.
                // A specific code for no paper is likely (e.g., 1, 2, -1, etc.)
                bool isReady = (status == 0); 
                Log.Debug(TAG, $"IsPaperReady Check - Status Code: {status}, IsReady: {isReady}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: IsPaperReady Check - Status Code: {status}, IsReady: {isReady}");
                return isReady; 
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"IsPaperReady Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: IsPaperReady Error: {ex.Message}");
                return false;
            }
        }

        public string GetStatusMessage()
        {
             if (!_isPrinterInitialized || _sdkHelperInstance == null)
             {
                 return "Printer not initialized or SDK instance null.";
             }
            
            try
            {
                int status = _sdkHelperInstance.PrintCheckStatus();
                // TODO: Map CS30 status codes to meaningful messages based on documentation.
                string message;
                switch (status)
                {
                    case 0: message = "Printer Ready"; break;
                    // Add cases for other known status codes (No Paper, Overheat, Cover Open, etc.)
                    // case 1: message = "Printer Error: No Paper"; break;
                    // case 2: message = "Printer Error: Overheat"; break;
                    default: message = $"Printer Status Unknown (Code: {status})"; break;
                }
                Log.Debug(TAG, $"GetStatusMessage - Code: {status}, Message: {message}");
                 System.Diagnostics.Debug.WriteLine($"{TAG}: GetStatusMessage - Code: {status}, Message: {message}");
                return message;
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"GetStatusMessage Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetStatusMessage Error: {ex.Message}");
                return $"Error checking printer status: {ex.Message}";
            }
        }

        // This method seems redundant if IsPaperReady is implemented based on status code
        public bool IsPrinterReady()
        {
            if (!_isPrinterInitialized || _sdkHelperInstance == null)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: IsPrinterReady Error - Printer not initialized or SDK instance null.");
                return false;
            }
            return IsPaperReady(); // Reuse the status check logic
        }

        // IServiceConnection methods - Implement if actually binding to a service is required
        // If PosApiHelper.Instance works directly, these might not be needed.
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
             System.Diagnostics.Debug.WriteLine($"{TAG}: OnServiceConnected - {name?.ClassName}");
             // Handle service binding if necessary
        }

        public void OnServiceDisconnected(ComponentName name)
        {
             System.Diagnostics.Debug.WriteLine($"{TAG}: OnServiceDisconnected - {name?.ClassName}");
             // Handle service unbinding if necessary
        }

        // Add the PrintQrCode method with 3 parameters (required by ICs30PosApi)
        public int PrintQrCode(string content, int width, int height)
        {
            if (!_isPrinterInitialized || _sdkHelperInstance == null)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode Error - Printer not initialized or SDK instance null.");
                return -1;
            }
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing QR code: {content?.Substring(0, Math.Min(30, content?.Length ?? 0))}...");
                // Use the SDK's 4-parameter method with empty format string
                int result = _sdkHelperInstance.PrintQrCode_Cut(content, width, height, "");
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintQrCode Error: {ex.Message}");
                return -1;
            }
        }

        // Add PrintBitmap method (required by ICs30PosApi)
        public int PrintBitmap(byte[] bitmapData, int width, int height)
        {
            if (!_isPrinterInitialized || _sdkHelperInstance == null)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap Error - Printer not initialized or SDK instance null.");
                return -1;
            }
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Printing bitmap with size: {width}x{height}");
                // Convert byte array to Bitmap first if needed by the SDK
                var bitmap = BitmapFactory.DecodeByteArray(bitmapData, 0, bitmapData.Length);
                if (bitmap == null)
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: Failed to decode bitmap data");
                    return -1;
                }

                int result = _sdkHelperInstance.PrintBmp(bitmap);
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap failed (Result Code: {result})");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: PrintBitmap Error: {ex.Message}");
                return -1;
            }
        }

        // Add GetPrinterStatus method (required by ICs30PosApi)
        public int GetPrinterStatus()
        {
            if (!_isPrinterInitialized || _sdkHelperInstance == null)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetPrinterStatus Error - Printer not initialized or SDK instance null.");
                return -1;
            }
            try
            {
                int status = _sdkHelperInstance.PrintCheckStatus();
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetPrinterStatus - Status Code: {status}");
                return status;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetPrinterStatus Error: {ex.Message}");
                return -1;
            }
        }

        // Scanner methods (required by ICs30PosApi)
        public int ScannerInit(int mode)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Initializing scanner with mode: {mode}");
                // TODO: Implement scanner initialization with the CS30 SDK
                // For now, return success
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: ScannerInit Error: {ex.Message}");
                return -1;
            }
        }

        public int StartScan()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Starting scan");
                // TODO: Implement start scan with the CS30 SDK
                // For now, return success
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: StartScan Error: {ex.Message}");
                return -1;
            }
        }

        public int StopScan()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Stopping scan");
                // TODO: Implement stop scan with the CS30 SDK
                // For now, return success
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: StopScan Error: {ex.Message}");
                return -1;
            }
        }

        public int GetScannerStatus()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Getting scanner status");
                // TODO: Implement get scanner status with the CS30 SDK
                // For now, return ready status
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: GetScannerStatus Error: {ex.Message}");
                return -1;
            }
        }

        public int ClearBuffer()
        {
            if (!_isPrinterInitialized || _sdkHelperInstance == null)
            {
                Log.Error(TAG, "ClearBuffer Error - Printer not initialized or SDK instance null.");
                System.Diagnostics.Debug.WriteLine($"{TAG}: ClearBuffer Error - Printer not initialized or SDK instance null.");
                return -1;
            }
            
            try
            {
                Log.Debug(TAG, "Clearing print buffer");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Clearing print buffer");
                
                // Try simple reinit to clear buffer state (since PrintClearBuffer doesn't exist)
                var clearResult = _sdkHelperInstance.PrintInit();
                Log.Debug(TAG, $"Buffer cleared via PrintInit with result: {clearResult}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: Buffer cleared via PrintInit with result: {clearResult}");
                return clearResult;
            }
            catch (Exception ex)
            {
                Log.Error(TAG, $"ClearBuffer Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"{TAG}: ClearBuffer Error: {ex.Message}");
                return -1;
            }
        }
    }
}
