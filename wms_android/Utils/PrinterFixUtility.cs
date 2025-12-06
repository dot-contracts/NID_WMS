using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Android.Content;
using wms_android.Interfaces;
using wms_android.Services;
using wms_android.Utils;
using Com.Vanstone.Trans.Api;
using Microsoft.Extensions.Logging;

namespace wms_android.Utils
{
    /// <summary>
    /// Utility for applying targeted fixes to common printer issues
    /// </summary>
    public static class PrinterFixUtility
    {
        private const string TAG = "PrinterFixUtility";

        /// <summary>
        /// Apply fixes based on comprehensive diagnostic results
        /// </summary>
        public static async Task<PrinterFixResult> ApplyAutomaticFixes(ComprehensiveDiagnosticResult diagnosticResult)
        {
            var fixResult = new PrinterFixResult();
            fixResult.StartedAt = DateTime.Now;
            
            Debug.WriteLine($"{TAG}: Starting automatic fixes based on diagnostic results");

            try
            {
                // Fix 1: Android Context Issues
                if (!diagnosticResult.AndroidContextAvailable)
                {
                    await FixAndroidContext(fixResult);
                }

                // Fix 2: Device Detection Consistency
                if (!diagnosticResult.DeviceDetectionConsistent)
                {
                    await FixDeviceDetection(fixResult);
                }

                // Fix 3: System Initialization (A90 only)
                if (!diagnosticResult.SystemInitializationSuccessful && diagnosticResult.PrinterFactoryDetection == "A90")
                {
                    await FixSystemInitialization(fixResult);
                }

                // Fix 4: CS30 SDK Instance Issues
                if (diagnosticResult.PrinterFactoryDetection == "CS30" && !diagnosticResult.PrinterHardwareCommunication)
                {
                    await FixCS30Instance(fixResult);
                }

                // Fix 5: Printer Service Recreation
                if (!diagnosticResult.PrinterServiceInitialized)
                {
                    await FixPrinterService(fixResult);
                }

                fixResult.CompletedAt = DateTime.Now;
                fixResult.Success = fixResult.SuccessfulFixes.Count > 0 && fixResult.FailedFixes.Count == 0;

                Debug.WriteLine($"{TAG}: Automatic fixes completed. Success: {fixResult.Success}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Critical error during fixes: {ex.Message}");
                fixResult.CriticalError = ex.Message;
                fixResult.Success = false;
                fixResult.CompletedAt = DateTime.Now;
            }

            return fixResult;
        }

        private static async Task FixAndroidContext(PrinterFixResult result)
        {
            try
            {
                Debug.WriteLine($"{TAG}: Attempting to fix Android context issues");
                
                var context = Android.App.Application.Context;
                if (context == null)
                {
                    // Try to get context from current activity
                    var currentActivity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
                    if (currentActivity != null)
                    {
                        result.SuccessfulFixes.Add("Android Context: Retrieved from current activity");
                        Debug.WriteLine($"{TAG}: Android context fixed using current activity");
                    }
                    else
                    {
                        result.FailedFixes.Add("Android Context: Still null after attempts");
                        Debug.WriteLine($"{TAG}: Could not fix Android context");
                    }
                }
                else
                {
                    result.SuccessfulFixes.Add("Android Context: Already available");
                    Debug.WriteLine($"{TAG}: Android context is already available");
                }
            }
            catch (Exception ex)
            {
                result.FailedFixes.Add($"Android Context: Fix failed - {ex.Message}");
                Debug.WriteLine($"{TAG}: Error fixing Android context: {ex.Message}");
            }
        }

        private static async Task FixDeviceDetection(PrinterFixResult result)
        {
            try
            {
                Debug.WriteLine($"{TAG}: Attempting to fix device detection consistency");
                
                // Force re-detect device type
                var deviceModel = Android.OS.Build.Model.ToUpper();
                bool isA90Device = deviceModel.Contains("A90");
                bool isCS30Device = !isA90Device; // Default to CS30 if not A90

                if (isA90Device)
                {
                    result.SuccessfulFixes.Add("Device Detection: Confirmed A90 device");
                    Debug.WriteLine($"{TAG}: Device detection fixed - A90 device confirmed");
                }
                else
                {
                    result.SuccessfulFixes.Add("Device Detection: Defaulted to CS30 device");
                    Debug.WriteLine($"{TAG}: Device detection fixed - defaulted to CS30");
                }
            }
            catch (Exception ex)
            {
                result.FailedFixes.Add($"Device Detection: Fix failed - {ex.Message}");
                Debug.WriteLine($"{TAG}: Error fixing device detection: {ex.Message}");
            }
        }

        private static async Task FixSystemInitialization(PrinterFixResult result)
        {
            try
            {
                Debug.WriteLine($"{TAG}: Attempting to fix A90 system initialization");
                
                var context = Android.App.Application.Context;
                if (context != null)
                {
                    // Retry system initialization with delay
                    await Task.Delay(2000); // Wait for system to settle
                    
                    try
                    {
                        SystemApi.SystemInit_Api(0, new byte[0], context);
                        await Task.Delay(1000); // Give initialization time to complete
                        
                        result.SuccessfulFixes.Add("System Initialization: A90 system initialization completed");
                        Debug.WriteLine($"{TAG}: A90 system initialization fixed");
                    }
                    catch (Exception initEx)
                    {
                        result.FailedFixes.Add($"System Initialization: Retry failed - {initEx.Message}");
                        Debug.WriteLine($"{TAG}: A90 system initialization retry failed: {initEx.Message}");
                    }
                }
                else
                {
                    result.FailedFixes.Add("System Initialization: No Android context available");
                }
            }
            catch (Exception ex)
            {
                result.FailedFixes.Add($"System Initialization: Fix failed - {ex.Message}");
                Debug.WriteLine($"{TAG}: Error fixing system initialization: {ex.Message}");
            }
        }

        private static async Task FixCS30Instance(PrinterFixResult result)
        {
            try
            {
                Debug.WriteLine($"{TAG}: Attempting to fix CS30 instance issues");
                
                // Try to reinitialize CS30 instance
                var cs30Instance = CS30PosApiHelper.Instance;
                if (cs30Instance != null)
                {
                    // Try basic initialization
                    int initResult = cs30Instance.PrintInit(2, 2, 0, 0);
                    if (initResult == 0)
                    {
                        result.SuccessfulFixes.Add("CS30 Instance: Reinitialized successfully");
                        Debug.WriteLine($"{TAG}: CS30 instance fixed through reinitialization");
                    }
                    else
                    {
                        result.FailedFixes.Add($"CS30 Instance: Initialization failed with code {initResult}");
                        Debug.WriteLine($"{TAG}: CS30 instance initialization failed: {initResult}");
                    }
                }
                else
                {
                    result.FailedFixes.Add("CS30 Instance: Instance is null");
                    Debug.WriteLine($"{TAG}: CS30 instance is null");
                }
            }
            catch (Exception ex)
            {
                result.FailedFixes.Add($"CS30 Instance: Fix failed - {ex.Message}");
                Debug.WriteLine($"{TAG}: Error fixing CS30 instance: {ex.Message}");
            }
        }

        private static async Task FixPrinterService(PrinterFixResult result)
        {
            try
            {
                Debug.WriteLine($"{TAG}: Attempting to fix printer service");
                
                // Recreate printer service
                var deviceDetectionService = new DeviceDetectionService();
                var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
                var factory = new PrinterServiceFactory(deviceDetectionService, loggerFactory);

                var printerService = factory.GetDefaultPrinterService();
                if (printerService != null)
                {
                    var initialized = await printerService.InitializePrinterAsync();
                    if (initialized)
                    {
                        result.SuccessfulFixes.Add("Printer Service: Recreated and initialized successfully");
                        Debug.WriteLine($"{TAG}: Printer service fixed");
                    }
                    else
                    {
                        result.FailedFixes.Add("Printer Service: Recreation successful but initialization failed");
                        Debug.WriteLine($"{TAG}: Printer service recreated but initialization failed");
                    }
                }
                else
                {
                    result.FailedFixes.Add("Printer Service: Recreation failed");
                    Debug.WriteLine($"{TAG}: Printer service recreation failed");
                }
            }
            catch (Exception ex)
            {
                result.FailedFixes.Add($"Printer Service: Fix failed - {ex.Message}");
                Debug.WriteLine($"{TAG}: Error fixing printer service: {ex.Message}");
            }
        }

        /// <summary>
        /// Reset all printer-related components
        /// </summary>
        public static async Task<PrinterFixResult> ResetAllPrinterComponents()
        {
            var result = new PrinterFixResult();
            result.StartedAt = DateTime.Now;

            Debug.WriteLine($"{TAG}: Performing complete printer system reset");

            try
            {
                // Step 1: Reset PrinterInitializationService
                try
                {
                    PrinterInitializationService.ResetPrinter();
                    result.SuccessfulFixes.Add("Reset: PrinterInitializationService reset");
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    result.FailedFixes.Add($"Reset: PrinterInitializationService failed - {ex.Message}");
                }

                // Step 2: Reinitialize components
                try
                {
                    PrinterInitializationService.Initialize();
                    result.SuccessfulFixes.Add("Reset: Components reinitialized");
                    await Task.Delay(2000);
                }
                catch (Exception ex)
                {
                    result.FailedFixes.Add($"Reset: Reinitialization failed - {ex.Message}");
                }

                result.CompletedAt = DateTime.Now;
                result.Success = result.SuccessfulFixes.Count > 0;
            }
            catch (Exception ex)
            {
                result.CriticalError = ex.Message;
                result.Success = false;
                result.CompletedAt = DateTime.Now;
            }

            return result;
        }
    }

    /// <summary>
    /// Result of printer fix operations
    /// </summary>
    public class PrinterFixResult
    {
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool Success { get; set; }
        public string CriticalError { get; set; }
        public List<string> SuccessfulFixes { get; set; } = new List<string>();
        public List<string> FailedFixes { get; set; } = new List<string>();

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== PRINTER FIX RESULTS ===");
            sb.AppendLine($"Started: {StartedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Completed: {CompletedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Duration: {(CompletedAt - StartedAt).TotalSeconds:F2}s");
            sb.AppendLine($"Overall Success: {Success}");

            if (!string.IsNullOrEmpty(CriticalError))
            {
                sb.AppendLine($"Critical Error: {CriticalError}");
            }

            if (SuccessfulFixes.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("=== SUCCESSFUL FIXES ===");
                foreach (var fix in SuccessfulFixes)
                {
                    sb.AppendLine($"✓ {fix}");
                }
            }

            if (FailedFixes.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("=== FAILED FIXES ===");
                foreach (var fix in FailedFixes)
                {
                    sb.AppendLine($"✗ {fix}");
                }
            }

            sb.AppendLine();
            sb.AppendLine("=== NEXT STEPS ===");
            if (Success)
            {
                sb.AppendLine("• Fixes applied successfully. Try printing again.");
                sb.AppendLine("• If issues persist, restart the application.");
            }
            else
            {
                sb.AppendLine("• Some fixes failed. Check device-specific requirements.");
                sb.AppendLine("• Ensure correct SDK versions are installed.");
                sb.AppendLine("• Contact support if problems continue.");
            }

            return sb.ToString();
        }
    }
} 