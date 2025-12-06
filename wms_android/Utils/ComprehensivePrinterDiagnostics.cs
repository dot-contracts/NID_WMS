using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using wms_android.Interfaces;
using wms_android.Services;
using wms_android.Utils;
using Com.Vanstone.Trans.Api;
using wms_android.shared.Models;
using Microsoft.Extensions.Logging;

namespace wms_android.Utils
{
    /// <summary>
    /// Comprehensive printer diagnostics utility that systematically checks all potential failure points
    /// </summary>
    public static class ComprehensivePrinterDiagnostics
    {
        private const string TAG = "ComprehensiveDiagnostics";

        public static async Task<ComprehensiveDiagnosticResult> RunFullDiagnosticAsync()
        {
            var result = new ComprehensiveDiagnosticResult();
            result.StartedAt = DateTime.Now;
            
            Debug.WriteLine($"{TAG}: Starting comprehensive printer diagnostics");

            try
            {
                // Step 1: Basic System Information
                await CheckSystemInfo(result);

                // Step 2: Device Detection Consistency
                await CheckDeviceDetection(result);

                // Step 3: SDK Availability
                await CheckSDKAvailability(result);

                // Step 4: Android Context
                await CheckAndroidContext(result);

                // Step 5: System Initialization
                await CheckSystemInitialization(result);

                // Step 6: Printer Service Creation
                await CheckPrinterServiceCreation(result);

                // Step 7: Printer Hardware Communication
                await CheckPrinterHardware(result);

                // Step 8: End-to-End Print Test
                if (result.AllBasicChecksPass)
                {
                    await CheckEndToEndPrinting(result);
                }

                result.CompletedAt = DateTime.Now;
                result.Success = result.AllBasicChecksPass && result.EndToEndTestPassed;

                Debug.WriteLine($"{TAG}: Comprehensive diagnostics completed. Success: {result.Success}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Critical error during diagnostics: {ex.Message}");
                result.CriticalError = ex.Message;
                result.Success = false;
                result.CompletedAt = DateTime.Now;
            }

            return result;
        }

        private static async Task CheckSystemInfo(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking system information");
            
            try
            {
                result.DeviceModel = Android.OS.Build.Model;
                result.DeviceManufacturer = Android.OS.Build.Manufacturer;
                result.AndroidVersion = Android.OS.Build.VERSION.Release;
                result.BuildNumber = Android.OS.Build.VERSION.Incremental;

                Debug.WriteLine($"{TAG}: Device: {result.DeviceManufacturer} {result.DeviceModel}");
                Debug.WriteLine($"{TAG}: Android: {result.AndroidVersion}");
                
                result.SystemInfoCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error getting system info: {ex.Message}");
                result.SystemInfoCheck = false;
                result.SystemInfoError = ex.Message;
            }
        }

        private static async Task CheckDeviceDetection(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking device detection consistency");
            
            try
            {
                // Check PrinterFactory detection
                bool isA90Factory = PrinterFactory.IsA90Device();
                bool isCS30Factory = PrinterFactory.IsCS30Device();
                var factoryImplementation = PrinterFactory.GetPrinterImplementation();

                // Check DeviceDetectionService detection  
                var deviceDetectionService = new DeviceDetectionService();
                var detectedDeviceType = deviceDetectionService.DetectDevice();

                result.PrinterFactoryDetection = isA90Factory ? "A90" : isCS30Factory ? "CS30" : "Unknown";
                result.DeviceServiceDetection = detectedDeviceType.ToString();
                result.FactoryImplementation = factoryImplementation?.GetType().Name ?? "NULL";

                // Check for consistency
                bool isConsistent = (isA90Factory && detectedDeviceType == PosDeviceType.A90) ||
                                   (isCS30Factory && detectedDeviceType == PosDeviceType.CS30) ||
                                   (!isA90Factory && !isCS30Factory && detectedDeviceType == PosDeviceType.CS30); // Unknown defaults to CS30

                result.DeviceDetectionConsistent = isConsistent;

                if (!isConsistent)
                {
                    result.DeviceDetectionError = $"Mismatch: PrinterFactory says {result.PrinterFactoryDetection}, DeviceService says {result.DeviceServiceDetection}";
                    Debug.WriteLine($"{TAG}: {result.DeviceDetectionError}");
                }

                result.DeviceDetectionCheck = true;
                Debug.WriteLine($"{TAG}: Device detection - Factory: {result.PrinterFactoryDetection}, Service: {result.DeviceServiceDetection}, Consistent: {isConsistent}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking device detection: {ex.Message}");
                result.DeviceDetectionCheck = false;
                result.DeviceDetectionError = ex.Message;
            }
        }

        private static async Task CheckSDKAvailability(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking SDK availability");
            
            try
            {
                // Check Vanstone SDK classes
                result.VanstoneSDKAvailable = IsClassAvailable("com.vanstone.trans.api.PrinterApi") &&
                                            IsClassAvailable("com.vanstone.trans.api.SystemApi") &&
                                            IsClassAvailable("com.vanstone.appsdk.api.engine.SdkApiEngine");

                // Check CS30 SDK classes
                result.CS30SDKAvailable = IsClassAvailable("com.ctk.sdk.PosApiHelper");

                result.SDKAvailabilityCheck = true;
                Debug.WriteLine($"{TAG}: SDK availability - Vanstone: {result.VanstoneSDKAvailable}, CS30: {result.CS30SDKAvailable}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking SDK availability: {ex.Message}");
                result.SDKAvailabilityCheck = false;
                result.SDKAvailabilityError = ex.Message;
            }
        }

        private static async Task CheckAndroidContext(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking Android context");
            
            try
            {
                var context = Android.App.Application.Context;
                result.AndroidContextAvailable = context != null;
                
                if (context != null)
                {
                    result.ContextPackageName = context.PackageName;
                    Debug.WriteLine($"{TAG}: Android context available: {result.ContextPackageName}");
                }
                else
                {
                    result.AndroidContextError = "Android Application Context is null";
                    Debug.WriteLine($"{TAG}: {result.AndroidContextError}");
                }

                result.AndroidContextCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking Android context: {ex.Message}");
                result.AndroidContextCheck = false;
                result.AndroidContextError = ex.Message;
            }
        }

        private static async Task CheckSystemInitialization(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking system initialization");
            
            try
            {
                var context = Android.App.Application.Context;
                if (context == null)
                {
                    result.SystemInitializationCheck = false;
                    result.SystemInitializationError = "Cannot test - Android context is null";
                    return;
                }

                // Only test system initialization if Vanstone SDK is available and device is A90
                if (result.VanstoneSDKAvailable && result.PrinterFactoryDetection == "A90")
                {
                    try
                    {
                        Debug.WriteLine($"{TAG}: Testing SystemApi.SystemInit_Api");
                        SystemApi.SystemInit_Api(0, new byte[0], context);
                        result.SystemInitializationSuccessful = true;
                        Debug.WriteLine($"{TAG}: SystemApi.SystemInit_Api completed successfully");
                        
                        // Wait a moment for initialization to complete
                        await Task.Delay(1000);
                    }
                    catch (Exception sysEx)
                    {
                        result.SystemInitializationSuccessful = false;
                        result.SystemInitializationError = $"SystemInit_Api failed: {sysEx.Message}";
                        Debug.WriteLine($"{TAG}: {result.SystemInitializationError}");
                    }
                }
                else
                {
                    result.SystemInitializationSuccessful = true; // Not required for CS30
                    Debug.WriteLine($"{TAG}: System initialization not required for this device type");
                }

                result.SystemInitializationCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking system initialization: {ex.Message}");
                result.SystemInitializationCheck = false;
                result.SystemInitializationError = ex.Message;
            }
        }

        private static async Task CheckPrinterServiceCreation(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking printer service creation");
            
            try
            {
                // Test factory-based service creation
                var deviceDetectionService = new DeviceDetectionService();
                var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
                var factory = new PrinterServiceFactory(deviceDetectionService, loggerFactory);

                var printerService = factory.GetDefaultPrinterService();
                result.PrinterServiceCreated = printerService != null;
                result.PrinterServiceType = printerService?.GetType().Name ?? "NULL";

                if (printerService != null)
                {
                    Debug.WriteLine($"{TAG}: Printer service created successfully: {result.PrinterServiceType}");
                    
                    // Test service initialization
                    try
                    {
                        var initialized = await printerService.InitializePrinterAsync();
                        result.PrinterServiceInitialized = initialized;
                        
                        if (initialized)
                        {
                            Debug.WriteLine($"{TAG}: Printer service initialized successfully");
                            
                            // Test status retrieval
                            try
                            {
                                var status = await printerService.GetPrinterStatusAsync();
                                result.PrinterStatus = status;
                                Debug.WriteLine($"{TAG}: Printer status: {status}");
                            }
                            catch (Exception statusEx)
                            {
                                result.PrinterStatusError = statusEx.Message;
                                Debug.WriteLine($"{TAG}: Error getting printer status: {statusEx.Message}");
                            }
                        }
                        else
                        {
                            result.PrinterServiceError = "Printer service initialization returned false";
                            Debug.WriteLine($"{TAG}: {result.PrinterServiceError}");
                        }
                    }
                    catch (Exception initEx)
                    {
                        result.PrinterServiceInitialized = false;
                        result.PrinterServiceError = $"Initialization failed: {initEx.Message}";
                        Debug.WriteLine($"{TAG}: {result.PrinterServiceError}");
                    }
                }
                else
                {
                    result.PrinterServiceError = "Factory returned null printer service";
                    Debug.WriteLine($"{TAG}: {result.PrinterServiceError}");
                }

                result.PrinterServiceCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking printer service creation: {ex.Message}");
                result.PrinterServiceCheck = false;
                result.PrinterServiceError = ex.Message;
            }
        }

        private static async Task CheckPrinterHardware(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking printer hardware communication");
            
            try
            {
                if (result.PrinterFactoryDetection == "A90" && result.VanstoneSDKAvailable)
                {
                    await CheckVanstonePrinterHardware(result);
                }
                else if (result.PrinterFactoryDetection == "CS30" && result.CS30SDKAvailable)
                {
                    await CheckCS30PrinterHardware(result);
                }
                else
                {
                    result.PrinterHardwareError = $"Cannot test hardware - unsupported device type: {result.PrinterFactoryDetection}";
                    Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                }

                result.PrinterHardwareCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error checking printer hardware: {ex.Message}");
                result.PrinterHardwareCheck = false;
                result.PrinterHardwareError = ex.Message;
            }
        }

        private static async Task CheckVanstonePrinterHardware(ComprehensiveDiagnosticResult result)
        {
            var context = Android.App.Application.Context;
            
            try
            {
                // Set context
                PrinterApi.SetContext(context);
                Debug.WriteLine($"{TAG}: Vanstone PrinterApi context set");

                // Test printer open
                int openResult = PrinterApi.PrnOpen_Api("", context);
                result.PrinterOpenResult = openResult;
                
                if (openResult == 0)
                {
                    Debug.WriteLine($"{TAG}: Printer opened successfully");
                    
                    // Test printer status
                    try
                    {
                        int statusResult = PrinterApi.PrnStatus_Api();
                        result.PrinterStatusCode = statusResult;
                        result.PrinterHardwareCommunication = true;
                        
                        Debug.WriteLine($"{TAG}: Printer status code: {statusResult}");
                        
                        // Interpret status code
                        result.PrinterStatusInterpretation = statusResult switch
                        {
                            0 => "Ready",
                            136 => "Ready (0x88)",
                            2 => "Out of paper",
                            3 => "Overheated", 
                            170 => "Busy (0xaa)",
                            _ => $"Unknown status: {statusResult}"
                        };
                    }
                    catch (Exception statusEx)
                    {
                        result.PrinterHardwareError = $"Status check failed: {statusEx.Message}";
                        Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                    }
                }
                else
                {
                    result.PrinterHardwareCommunication = false;
                    result.PrinterHardwareError = $"Printer open failed with code: {openResult}";
                    Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                }
            }
            catch (Exception ex)
            {
                result.PrinterHardwareCommunication = false;
                result.PrinterHardwareError = $"Hardware communication error: {ex.Message}";
                Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
            }
        }

        private static async Task CheckCS30PrinterHardware(ComprehensiveDiagnosticResult result)
        {
            try
            {
                var cs30Helper = CS30PosApiHelper.Instance;
                if (cs30Helper != null)
                {
                    // Test basic printer initialization
                    int initResult = cs30Helper.PrintInit(2, 2, 0, 0);
                    result.PrinterOpenResult = initResult;
                    
                    if (initResult == 0)
                    {
                        result.PrinterHardwareCommunication = true;
                        Debug.WriteLine($"{TAG}: CS30 printer initialized successfully");
                        
                        // Test status
                        try
                        {
                            var status = cs30Helper.GetPrinterStatus();
                            result.PrinterStatusCode = status;
                            result.PrinterStatusInterpretation = status switch
                            {
                                0 => "Ready",
                                1 => "Error",
                                2 => "Paper out",
                                3 => "Busy",
                                _ => $"Unknown status: {status}"
                            };
                            Debug.WriteLine($"{TAG}: CS30 printer status: {result.PrinterStatusInterpretation}");
                        }
                        catch (Exception statusEx)
                        {
                            result.PrinterHardwareError = $"CS30 status check failed: {statusEx.Message}";
                            Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                        }
                    }
                    else
                    {
                        result.PrinterHardwareCommunication = false;
                        result.PrinterHardwareError = $"CS30 printer init failed with code: {initResult}";
                        Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                    }
                }
                else
                {
                    result.PrinterHardwareCommunication = false;
                    result.PrinterHardwareError = "CS30PosApiHelper.Instance is null";
                    Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
                }
            }
            catch (Exception ex)
            {
                result.PrinterHardwareCommunication = false;
                result.PrinterHardwareError = $"CS30 hardware communication error: {ex.Message}";
                Debug.WriteLine($"{TAG}: {result.PrinterHardwareError}");
            }
        }

        private static async Task CheckEndToEndPrinting(ComprehensiveDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Checking end-to-end printing");
            
            try
            {
                if (!result.PrinterServiceInitialized)
                {
                    result.EndToEndTestError = "Skipping - printer service not initialized";
                    return;
                }

                // Create a test printer service
                var deviceDetectionService = new DeviceDetectionService();
                var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
                var factory = new PrinterServiceFactory(deviceDetectionService, loggerFactory);
                var printerService = factory.GetDefaultPrinterService();

                if (printerService != null)
                {
                    var testContent = $@"===== DIAGNOSTIC TEST =====
Device: {result.DeviceManufacturer} {result.DeviceModel}
Service: {result.PrinterServiceType}
Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
Test Status: SUCCESS
===========================";

                    bool textResult = await printerService.PrintTextAsync(testContent);
                    if (textResult)
                    {
                        bool startResult = await printerService.StartPrintJobAsync();
                        result.EndToEndTestPassed = startResult;
                        
                        if (startResult)
                        {
                            Debug.WriteLine($"{TAG}: End-to-end print test successful");
                        }
                        else
                        {
                            result.EndToEndTestError = "Print job start failed";
                            Debug.WriteLine($"{TAG}: {result.EndToEndTestError}");
                        }
                    }
                    else
                    {
                        result.EndToEndTestError = "Print text preparation failed";
                        Debug.WriteLine($"{TAG}: {result.EndToEndTestError}");
                    }
                }
                else
                {
                    result.EndToEndTestError = "Cannot create printer service for test";
                }

                result.EndToEndTestCheck = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{TAG}: Error in end-to-end test: {ex.Message}");
                result.EndToEndTestCheck = false;
                result.EndToEndTestError = ex.Message;
            }
        }

        private static bool IsClassAvailable(string className)
        {
            try
            {
                Java.Lang.Class.ForName(className);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Comprehensive diagnostic result with detailed information about all checks
    /// </summary>
    public class ComprehensiveDiagnosticResult
    {
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool Success { get; set; }
        public string CriticalError { get; set; }

        // System Info
        public bool SystemInfoCheck { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceManufacturer { get; set; }
        public string AndroidVersion { get; set; }
        public string BuildNumber { get; set; }
        public string SystemInfoError { get; set; }

        // Device Detection
        public bool DeviceDetectionCheck { get; set; }
        public string PrinterFactoryDetection { get; set; }
        public string DeviceServiceDetection { get; set; }
        public string FactoryImplementation { get; set; }
        public bool DeviceDetectionConsistent { get; set; }
        public string DeviceDetectionError { get; set; }

        // SDK Availability
        public bool SDKAvailabilityCheck { get; set; }
        public bool VanstoneSDKAvailable { get; set; }
        public bool CS30SDKAvailable { get; set; }
        public string SDKAvailabilityError { get; set; }

        // Android Context
        public bool AndroidContextCheck { get; set; }
        public bool AndroidContextAvailable { get; set; }
        public string ContextPackageName { get; set; }
        public string AndroidContextError { get; set; }

        // System Initialization
        public bool SystemInitializationCheck { get; set; }
        public bool SystemInitializationSuccessful { get; set; }
        public string SystemInitializationError { get; set; }

        // Printer Service
        public bool PrinterServiceCheck { get; set; }
        public bool PrinterServiceCreated { get; set; }
        public string PrinterServiceType { get; set; }
        public bool PrinterServiceInitialized { get; set; }
        public string PrinterStatus { get; set; }
        public string PrinterStatusError { get; set; }
        public string PrinterServiceError { get; set; }

        // Printer Hardware
        public bool PrinterHardwareCheck { get; set; }
        public bool PrinterHardwareCommunication { get; set; }
        public int PrinterOpenResult { get; set; }
        public int PrinterStatusCode { get; set; }
        public string PrinterStatusInterpretation { get; set; }
        public string PrinterHardwareError { get; set; }

        // End-to-End Test
        public bool EndToEndTestCheck { get; set; }
        public bool EndToEndTestPassed { get; set; }
        public string EndToEndTestError { get; set; }

        public bool AllBasicChecksPass => 
            SystemInfoCheck && 
            DeviceDetectionCheck && 
            SDKAvailabilityCheck && 
            AndroidContextCheck && 
            SystemInitializationCheck && 
            PrinterServiceCheck &&
            PrinterHardwareCheck &&
            AndroidContextAvailable &&
            SystemInitializationSuccessful &&
            PrinterServiceCreated &&
            PrinterServiceInitialized &&
            PrinterHardwareCommunication;

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("=== COMPREHENSIVE PRINTER DIAGNOSTICS ===");
            sb.AppendLine($"Started: {StartedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Completed: {CompletedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Duration: {(CompletedAt - StartedAt).TotalSeconds:F2}s");
            sb.AppendLine($"Overall Success: {Success}");
            
            if (!string.IsNullOrEmpty(CriticalError))
            {
                sb.AppendLine($"Critical Error: {CriticalError}");
            }
            
            sb.AppendLine();
            sb.AppendLine("=== SYSTEM INFORMATION ===");
            sb.AppendLine($"Check Passed: {SystemInfoCheck}");
            if (SystemInfoCheck)
            {
                sb.AppendLine($"Device: {DeviceManufacturer} {DeviceModel}");
                sb.AppendLine($"Android: {AndroidVersion}");
                sb.AppendLine($"Build: {BuildNumber}");
            }
            if (!string.IsNullOrEmpty(SystemInfoError))
                sb.AppendLine($"Error: {SystemInfoError}");

            sb.AppendLine();
            sb.AppendLine("=== DEVICE DETECTION ===");
            sb.AppendLine($"Check Passed: {DeviceDetectionCheck}");
            if (DeviceDetectionCheck)
            {
                sb.AppendLine($"PrinterFactory: {PrinterFactoryDetection}");
                sb.AppendLine($"DeviceService: {DeviceServiceDetection}");
                sb.AppendLine($"Implementation: {FactoryImplementation}");
                sb.AppendLine($"Consistent: {DeviceDetectionConsistent}");
            }
            if (!string.IsNullOrEmpty(DeviceDetectionError))
                sb.AppendLine($"Error: {DeviceDetectionError}");

            sb.AppendLine();
            sb.AppendLine("=== SDK AVAILABILITY ===");
            sb.AppendLine($"Check Passed: {SDKAvailabilityCheck}");
            if (SDKAvailabilityCheck)
            {
                sb.AppendLine($"Vanstone SDK: {VanstoneSDKAvailable}");
                sb.AppendLine($"CS30 SDK: {CS30SDKAvailable}");
            }
            if (!string.IsNullOrEmpty(SDKAvailabilityError))
                sb.AppendLine($"Error: {SDKAvailabilityError}");

            sb.AppendLine();
            sb.AppendLine("=== ANDROID CONTEXT ===");
            sb.AppendLine($"Check Passed: {AndroidContextCheck}");
            if (AndroidContextCheck)
            {
                sb.AppendLine($"Context Available: {AndroidContextAvailable}");
                if (!string.IsNullOrEmpty(ContextPackageName))
                    sb.AppendLine($"Package: {ContextPackageName}");
            }
            if (!string.IsNullOrEmpty(AndroidContextError))
                sb.AppendLine($"Error: {AndroidContextError}");

            sb.AppendLine();
            sb.AppendLine("=== SYSTEM INITIALIZATION ===");
            sb.AppendLine($"Check Passed: {SystemInitializationCheck}");
            if (SystemInitializationCheck)
            {
                sb.AppendLine($"Initialization Successful: {SystemInitializationSuccessful}");
            }
            if (!string.IsNullOrEmpty(SystemInitializationError))
                sb.AppendLine($"Error: {SystemInitializationError}");

            sb.AppendLine();
            sb.AppendLine("=== PRINTER SERVICE ===");
            sb.AppendLine($"Check Passed: {PrinterServiceCheck}");
            if (PrinterServiceCheck)
            {
                sb.AppendLine($"Service Created: {PrinterServiceCreated}");
                sb.AppendLine($"Service Type: {PrinterServiceType}");
                sb.AppendLine($"Service Initialized: {PrinterServiceInitialized}");
                if (!string.IsNullOrEmpty(PrinterStatus))
                    sb.AppendLine($"Status: {PrinterStatus}");
            }
            if (!string.IsNullOrEmpty(PrinterServiceError))
                sb.AppendLine($"Error: {PrinterServiceError}");
            if (!string.IsNullOrEmpty(PrinterStatusError))
                sb.AppendLine($"Status Error: {PrinterStatusError}");

            sb.AppendLine();
            sb.AppendLine("=== PRINTER HARDWARE ===");
            sb.AppendLine($"Check Passed: {PrinterHardwareCheck}");
            if (PrinterHardwareCheck)
            {
                sb.AppendLine($"Hardware Communication: {PrinterHardwareCommunication}");
                sb.AppendLine($"Open Result: {PrinterOpenResult}");
                sb.AppendLine($"Status Code: {PrinterStatusCode}");
                if (!string.IsNullOrEmpty(PrinterStatusInterpretation))
                    sb.AppendLine($"Status: {PrinterStatusInterpretation}");
            }
            if (!string.IsNullOrEmpty(PrinterHardwareError))
                sb.AppendLine($"Error: {PrinterHardwareError}");

            sb.AppendLine();
            sb.AppendLine("=== END-TO-END TEST ===");
            sb.AppendLine($"Check Passed: {EndToEndTestCheck}");
            if (EndToEndTestCheck)
            {
                sb.AppendLine($"Test Passed: {EndToEndTestPassed}");
            }
            if (!string.IsNullOrEmpty(EndToEndTestError))
                sb.AppendLine($"Error: {EndToEndTestError}");

            sb.AppendLine();
            sb.AppendLine("=== RECOMMENDATIONS ===");
            
            if (!Success)
            {
                if (!AndroidContextAvailable)
                {
                    sb.AppendLine("• Fix Android Context: Ensure app initialization is complete before printer access");
                }
                
                if (!DeviceDetectionConsistent)
                {
                    sb.AppendLine("• Fix Device Detection: Update PrinterFactory and DeviceDetectionService to use consistent logic");
                }
                
                if (!SystemInitializationSuccessful && PrinterFactoryDetection == "A90")
                {
                    sb.AppendLine("• Fix System Init: SystemApi.SystemInit_Api() is failing - check SDK version and permissions");
                }
                
                if (!VanstoneSDKAvailable && PrinterFactoryDetection == "A90")
                {
                    sb.AppendLine("• Install Vanstone SDK: Required Vanstone SDK classes are missing");
                }
                
                if (!CS30SDKAvailable && PrinterFactoryDetection == "CS30")
                {
                    sb.AppendLine("• Install CS30 SDK: Required CS30 SDK classes are missing");
                }
                
                if (!PrinterHardwareCommunication)
                {
                    sb.AppendLine("• Check Hardware: Printer hardware is not responding - check physical connection");
                }
            }
            else
            {
                sb.AppendLine("• All checks passed! Printer should be working correctly.");
            }

            return sb.ToString();
        }
    }
} 