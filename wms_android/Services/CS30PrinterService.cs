using System;
using System.Threading.Tasks;
using Android.Graphics;
using wms_android.Interfaces;
using wms_android.Utils;
using Microsoft.Extensions.Logging;
using System.Text;

namespace wms_android.Services
{
    public class CS30PrinterService : IPrinterService
    {
        private readonly ICs30PosApi _printerHelper;
        private bool _isInitialized;
        private readonly ILogger<CS30PrinterService> _logger;

        public CS30PrinterService(ILogger<CS30PrinterService> logger)
        {
            _logger = logger;
            
            try
            {
                // Safely get the CS30PosApiHelper instance
                var cs30Helper = CS30PosApiHelper.Instance;
                if (cs30Helper == null)
                {
                    _logger.LogError("CS30PosApiHelper.Instance is null - CS30 SDK may not be available");
                    throw new InvalidOperationException("CS30PosApiHelper.Instance is null - CS30 SDK not available");
                }
                
                _printerHelper = (ICs30PosApi)cs30Helper;
                if (_printerHelper == null)
                {
                    _logger.LogError("Failed to cast CS30PosApiHelper to ICs30PosApi interface");
                    throw new InvalidOperationException("Failed to cast CS30PosApiHelper to ICs30PosApi interface");
                }
                
                _logger.LogInformation("CS30PrinterService initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize CS30PrinterService");
                throw; // Re-throw to prevent service creation with invalid state
            }
        }

        public async Task<bool> InitializePrinterAsync()
        {
            try
            {
                if (_isInitialized)
                {
                    return true;
                }

                _logger.LogInformation("Starting CS30 printer initialization...");
                
                // Check if the CS30 helper is still valid
                if (_printerHelper == null)
                {
                    _logger.LogError("CS30 printer helper is null - cannot initialize");
                    return false;
                }
                
                // Reset initialization state to clear any stuck retry counters
                if (_printerHelper is Utils.CS30PosApiHelper cs30Helper)
                {
                    cs30Helper.ResetInitializationState();
                    _logger.LogInformation("CS30 initialization state reset");
                }
                
                // Use AndroidPosSdk proper initialization parameters according to official documentation:
                // Gray: 1-5 range (1=lowest, 3=medium, 5=highest) - use 3 for medium density
                // FontHeight: 16 or 24 (use 24 for good readability)
                // FontWidth: 16 or 24 (use 24 to match height)
                // FontZoom: 0x00 (normal) or 0x33 (bold, use 0 for normal)
                var result = await Task.Run(() => _printerHelper.PrintInit(3, 24, 0, 0));
                _isInitialized = result == 0;
                
                if (_isInitialized)
                {
                    _logger.LogInformation("CS30 printer initialized successfully");
                }
                else
                {
                    _logger.LogError($"Failed to initialize CS30 printer - PrintInit returned error code: {result}");
                    
                    // Provide specific guidance for common AndroidPosSdk error codes
                    string troubleshootingMessage = result switch
                    {
                        -4001 => "Printer is busy - wait for current job to complete",
                        -4002 => "No paper loaded - please load paper and try again",
                        -4003 => "Data error - check print content format",
                        -4004 => "Printer hardware fault - check device connections and restart",
                        -4005 => "Printer overheated - allow device to cool down",
                        -4006 => "Print job unfinished - previous job may be stuck",
                        -4007 => "Font library not found - check SDK installation",
                        -4008 => "Print buffer overflow - reduce print content size",
                        -4009 => "Font setting error - check font parameters",
                        -4010 => "Font reading error - font system issue",
                        -5555 => "CS30 service not available - device may not be CS30 or SDK not installed",
                        _ => "Unknown error - check device and SDK status"
                    };
                    
                    _logger.LogError($"CS30 Error {result}: {troubleshootingMessage}");
                    
                    // Try to get more detailed error information
                    try
                    {
                        var status = _printerHelper.GetPrinterStatus();
                        var statusMessage = status switch
                        {
                            0 => "Ready",
                            1 => "Error",
                            2 => "Paper out", 
                            3 => "Busy",
                            -1 => "SDK not available or hardware error",
                            _ => $"Unknown status: {status}"
                        };
                        
                        _logger.LogError($"CS30 printer status: {statusMessage} (code: {status})");
                        
                        // Additional diagnostic information
                        if (result == -1)
                        {
                            _logger.LogError("CS30 PrintInit failed - possible causes:");
                            _logger.LogError("1. Device is not a CS30 device");
                            _logger.LogError("2. CS30 SDK is not properly installed");
                            _logger.LogError("3. Printer hardware is not available");
                            _logger.LogError("4. Required permissions are missing");
                        }
                        else if (result == -99)
                        {
                            _logger.LogError("CS30 PrintInit failed - exceeded maximum initialization attempts");
                        }
                    }
                    catch (Exception statusEx)
                    {
                        _logger.LogError(statusEx, "Could not retrieve CS30 printer status for additional diagnostics");
                    }
                }

                return _isInitialized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during CS30 printer initialization");
                return false;
            }
        }

        public async Task<bool> PrintTextAsync(string text)
        {
            return await PrintTextAsync(text, 24, false);
        }

        public async Task<bool> PrintTextAsync(string text, int fontSize, bool isBold)
        {
            try
            {
                if (!_isInitialized)
                {
                    var initialized = await InitializePrinterAsync();
                    if (!initialized)
                    {
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(text))
                {
                    _logger.LogWarning("PrintTextAsync called with null or empty text");
                    return false;
                }

                // Clear buffer before printing to ensure clean state (CS30 specific)
                // Note: CS30 SDK doesn't have a clear buffer method, so we skip this step
                _logger.LogDebug("Skipping buffer clear (not available in CS30 SDK)");

                // Set font
                var fontResult = await Task.Run(() => _printerHelper.PrintSetFont((sbyte)fontSize, (sbyte)fontSize, (sbyte)(isBold ? 1 : 0)));
                if (fontResult != 0)
                {
                    _logger.LogError($"Failed to set font: {fontResult}");
                    return false;
                }

                // Print text (prepare for printing, but don't start the print job yet)
                var printResult = await Task.Run(() => _printerHelper.PrintStr(text));
                if (printResult != 0)
                {
                    _logger.LogError($"Failed to print text: {printResult}");
                    return false;
                }

                _logger.LogDebug("Text prepared for printing successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing text");
                return false;
            }
        }

        public async Task<bool> PrintQRCodeAsync(string content, int width, int height)
        {
            try
            {
                if (!_isInitialized)
                {
                    var initialized = await InitializePrinterAsync();
                    if (!initialized)
                    {
                        return false;
                    }
                }

                // Print QR code with specified dimensions (prepare for printing, but don't start the print job yet)
                var result = await Task.Run(() => _printerHelper.PrintQrCode(content, width, height));
                if (result != 0)
                {
                    _logger.LogError($"Failed to print QR code: {result}");
                    return false;
                }

                // Don't call PrintStart here - let the caller decide when to start printing
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing QR code");
                return false;
            }
        }

        public async Task<bool> PrintImageAsync(byte[] imageData, int width)
        {
            try
            {
                if (!await InitializePrinterAsync())
                {
                    return false;
                }

                using var bitmap = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length);
                if (bitmap == null)
                {
                    _logger.LogError("Failed to decode image data");
                    return false;
                }

                // Scale bitmap to printer width while maintaining aspect ratio
                float scale = (float)width / bitmap.Width;
                int height = (int)(bitmap.Height * scale);

                using var scaledBitmap = Bitmap.CreateScaledBitmap(bitmap, width, height, true);
                
                // Convert bitmap to bytes
                using var stream = new System.IO.MemoryStream();
                await scaledBitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, stream);
                var bitmapBytes = stream.ToArray();

                var result = await Task.Run(() => _printerHelper.PrintBitmap(bitmapBytes, width, height));
                if (result != 0)
                {
                    _logger.LogError($"Failed to print image: {result}");
                    return false;
                }

                var startResult = await Task.Run(() => _printerHelper.PrintStart());
                return startResult == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing image on CS30 printer");
                return false;
            }
        }

        public async Task<bool> FeedPaperAsync(int lines)
        {
            try
            {
                if (!await InitializePrinterAsync())
                {
                    return false;
                }

                var result = await Task.Run(() => _printerHelper.PrintFeedPaper(lines));
                return result == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error feeding paper on CS30 printer");
                return false;
            }
        }

        public async Task<bool> CutPaperAsync()
        {
            try
            {
                if (!_isInitialized)
                {
                    return false;
                }

                // Feed paper before cutting
                var feedResult = await Task.Run(() => _printerHelper.PrintFeedPaper(3));
                if (feedResult != 0)
                {
                    _logger.LogError($"Failed to feed paper: {feedResult}");
                    return false;
                }

                // Start printing to execute the cut
                var startResult = await Task.Run(() => _printerHelper.PrintStart());
                return startResult == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cutting paper");
                return false;
            }
        }

        public async Task<string> GetPrinterStatusAsync()
        {
            try
            {
                if (!_isInitialized)
                {
                    return "Printer not initialized";
                }

                var status = await Task.Run(() => _printerHelper.GetPrinterStatus());
                return status switch
                {
                    0 => "Printer is ready",
                    1 => "Printer error",
                    2 => "Paper out",
                    3 => "Printer busy",
                    _ => "Unknown error"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting printer status");
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> StartPrintJobAsync()
        {
            try
            {
                if (!_isInitialized)
                {
                    var initialized = await InitializePrinterAsync();
                    if (!initialized)
                    {
                        return false;
                    }
                }

                var result = await Task.Run(() => _printerHelper.PrintStart());
                return result == 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting print job");
                return false;
            }
        }

        public void ClosePrinter()
        {
            try
            {
                if (_isInitialized)
                {
                    _printerHelper?.Dispose();
                    _isInitialized = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing CS30 printer");
            }
        }

        /// <summary>
        /// Performs comprehensive diagnostics of the CS30 printer system
        /// </summary>
        public async Task<string> PerformDiagnosticsAsync()
        {
            var diagnostics = new StringBuilder();
            diagnostics.AppendLine("=== CS30 Printer Diagnostics ===");
            diagnostics.AppendLine($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            diagnostics.AppendLine();

            try
            {
                // Check if service is initialized
                diagnostics.AppendLine($"Service Initialized: {_isInitialized}");
                diagnostics.AppendLine($"Printer Helper Available: {_printerHelper != null}");
                
                if (_printerHelper != null)
                {
                    try
                    {
                        // Check printer status
                        var status = _printerHelper.GetPrinterStatus();
                        var statusMessage = status switch
                        {
                            0 => "Ready",
                            1 => "Error",
                            2 => "Paper out",
                            3 => "Busy",
                            -1 => "SDK not available or hardware error",
                            _ => $"Unknown status: {status}"
                        };
                        diagnostics.AppendLine($"Printer Status: {statusMessage} (code: {status})");
                        
                        // Test basic operations
                        if (!_isInitialized)
                        {
                            diagnostics.AppendLine();
                            diagnostics.AppendLine("Attempting printer initialization...");
                            var initResult = await InitializePrinterAsync();
                            diagnostics.AppendLine($"Initialization Result: {initResult}");
                        }
                        
                        // Additional hardware checks
                        if (_isInitialized)
                        {
                            try
                            {
                                var paperStatus = "Unknown";
                                // We can't directly check paper status without more SDK documentation
                                diagnostics.AppendLine($"Paper Status: {paperStatus}");
                            }
                            catch (Exception paperEx)
                            {
                                diagnostics.AppendLine($"Paper Status Check Failed: {paperEx.Message}");
                            }
                        }
                    }
                    catch (Exception helperEx)
                    {
                        diagnostics.AppendLine($"Printer Helper Error: {helperEx.Message}");
                    }
                }
                
                // System information
                diagnostics.AppendLine();
                diagnostics.AppendLine("=== System Information ===");
                try
                {
                    var deviceModel = Android.OS.Build.Model;
                    var deviceManufacturer = Android.OS.Build.Manufacturer;
                    var androidVersion = Android.OS.Build.VERSION.Release;
                    
                    diagnostics.AppendLine($"Device: {deviceManufacturer} {deviceModel}");
                    diagnostics.AppendLine($"Android Version: {androidVersion}");
                    diagnostics.AppendLine($"Build: {Android.OS.Build.Display}");
                }
                catch (Exception sysEx)
                {
                    diagnostics.AppendLine($"System Info Error: {sysEx.Message}");
                }
                
                // CS30 SDK specific checks
                diagnostics.AppendLine();
                diagnostics.AppendLine("=== CS30 SDK Information ===");
                try
                {
                    var cs30Helper = Utils.CS30PosApiHelper.Instance;
                    diagnostics.AppendLine($"CS30PosApiHelper Instance: {cs30Helper != null}");
                    
                    if (cs30Helper != null)
                    {
                        diagnostics.AppendLine("CS30 SDK appears to be available");
                    }
                    else
                    {
                        diagnostics.AppendLine("CS30 SDK not available - possible reasons:");
                        diagnostics.AppendLine("1. Device is not a CS30 device");
                        diagnostics.AppendLine("2. CS30 SDK not installed");
                        diagnostics.AppendLine("3. SDK initialization failed");
                    }
                }
                catch (Exception sdkEx)
                {
                    diagnostics.AppendLine($"CS30 SDK Check Failed: {sdkEx.Message}");
                }
                
            }
            catch (Exception ex)
            {
                diagnostics.AppendLine($"Diagnostics Error: {ex.Message}");
                diagnostics.AppendLine($"Stack Trace: {ex.StackTrace}");
            }
            
            var result = diagnostics.ToString();
            _logger.LogInformation("CS30 Printer Diagnostics:\n{Diagnostics}", result);
            return result;
        }
    }
} 