using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wms_android.Services;
using System.Diagnostics;
using System.Collections.Generic;

namespace wms_android.Utils
{
    /// <summary>
    /// Diagnostic utilities for testing the adaptive printer system
    /// </summary>
    public static class AdaptivePrinterDiagnostics
    {
        private const string TAG = "AdaptivePrinterDiag";

        /// <summary>
        /// Comprehensive test of the adaptive printer system
        /// </summary>
        public static async Task<AdaptivePrinterDiagnosticResult> RunComprehensiveTestAsync()
        {
            var result = new AdaptivePrinterDiagnosticResult
            {
                StartTime = DateTime.Now
            };

            try
            {
                Debug.WriteLine($"{TAG}: Starting comprehensive adaptive printer test");

                // Step 1: Test device detection and strategy evaluation
                await TestDeviceDetectionAndStrategies(result);

                // Step 2: Test adaptive initialization service
                await TestAdaptiveInitializationService(result);

                // Step 3: Test adaptive printer service
                await TestAdaptivePrinterService(result);

                // Step 4: Test printing functionality
                await TestPrintingFunctionality(result);

                result.Success = result.StrategyTestResults.Count > 0 && 
                               result.InitializationServiceWorking && 
                               result.PrinterServiceWorking;

            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Success = false;
                Debug.WriteLine($"{TAG}: Error during comprehensive test: {ex.Message}");
            }
            finally
            {
                result.EndTime = DateTime.Now;
                result.Duration = result.EndTime - result.StartTime;
            }

            return result;
        }

        private static async Task TestDeviceDetectionAndStrategies(AdaptivePrinterDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Testing device detection and strategies");

            try
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<AdaptivePrinterInitializationService>();
                var initService = new AdaptivePrinterInitializationService(logger);

                // Test each strategy individually
                var strategies = new IPrinterInitializationStrategy[]
                {
                    new CH10PrinterInitializationStrategy(logger),
                    new CS30PrinterInitializationStrategy(logger),
                    new VanstoneA90PrinterInitializationStrategy(logger),
                    new GenericVanstonePrinterInitializationStrategy(logger),
                    new UniversalPrinterInitializationStrategy(logger)
                };

                foreach (var strategy in strategies)
                {
                    var strategyResult = new StrategyTestResult
                    {
                        StrategyName = strategy.StrategyName
                    };

                    try
                    {
                        strategyResult.CanHandleDevice = await strategy.CanHandleDeviceAsync();
                        Debug.WriteLine($"{TAG}: Strategy {strategy.StrategyName} can handle device: {strategyResult.CanHandleDevice}");

                        if (strategyResult.CanHandleDevice)
                        {
                            var initResult = await strategy.InitializeAsync();
                            strategyResult.InitializationSuccess = initResult.Success;
                            strategyResult.InitializationError = initResult.ErrorMessage;
                            strategyResult.Metadata = initResult.Metadata;

                            Debug.WriteLine($"{TAG}: Strategy {strategy.StrategyName} initialization: {initResult.Success}");
                            if (!initResult.Success)
                            {
                                Debug.WriteLine($"{TAG}: Strategy {strategy.StrategyName} error: {initResult.ErrorMessage}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strategyResult.TestError = ex.Message;
                        Debug.WriteLine($"{TAG}: Strategy {strategy.StrategyName} test error: {ex.Message}");
                    }

                    result.StrategyTestResults.Add(strategyResult);
                }

                result.DeviceDetectionWorking = true;
            }
            catch (Exception ex)
            {
                result.DeviceDetectionError = ex.Message;
                result.DeviceDetectionWorking = false;
                Debug.WriteLine($"{TAG}: Device detection test error: {ex.Message}");
            }
        }

        private static async Task TestAdaptiveInitializationService(AdaptivePrinterDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Testing adaptive initialization service");

            try
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<AdaptivePrinterInitializationService>();
                var initService = new AdaptivePrinterInitializationService(logger);

                var initResult = await initService.InitializePrinterAsync();
                result.InitializationServiceWorking = initResult;

                if (initResult)
                {
                    var strategy = initService.GetSuccessfulStrategy();
                    result.SuccessfulStrategyName = strategy?.StrategyName ?? "Unknown";
                    Debug.WriteLine($"{TAG}: Adaptive initialization successful with strategy: {result.SuccessfulStrategyName}");
                }
                else
                {
                    result.InitializationServiceError = "Failed to initialize with any strategy";
                    Debug.WriteLine($"{TAG}: Adaptive initialization failed with all strategies");
                }
            }
            catch (Exception ex)
            {
                result.InitializationServiceError = ex.Message;
                result.InitializationServiceWorking = false;
                Debug.WriteLine($"{TAG}: Adaptive initialization service test error: {ex.Message}");
            }
        }

        private static async Task TestAdaptivePrinterService(AdaptivePrinterDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Testing adaptive printer service");

            try
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<AdaptivePrinterService>();
                var printerService = new AdaptivePrinterService(logger, loggerFactory);

                // Initialize the printer
                await printerService.InitializePrinterAsync();

                var serviceInitResult = await printerService.InitializePrinterAsync();
                result.PrinterServiceWorking = serviceInitResult;

                if (serviceInitResult)
                {
                    result.PrinterServiceDiagnostics = printerService.GetDiagnostics();
                    Debug.WriteLine($"{TAG}: Adaptive printer service initialized successfully");
                }
                else
                {
                    result.PrinterServiceError = "Failed to initialize adaptive printer service";
                    Debug.WriteLine($"{TAG}: Adaptive printer service initialization failed");
                }
            }
            catch (Exception ex)
            {
                result.PrinterServiceError = ex.Message;
                result.PrinterServiceWorking = false;
                Debug.WriteLine($"{TAG}: Adaptive printer service test error: {ex.Message}");
            }
        }

        private static async Task TestPrintingFunctionality(AdaptivePrinterDiagnosticResult result)
        {
            Debug.WriteLine($"{TAG}: Testing printing functionality");

            if (!result.PrinterServiceWorking)
            {
                result.PrintingTestSkipped = true;
                Debug.WriteLine($"{TAG}: Skipping printing test - printer service not working");
                return;
            }

            try
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<AdaptivePrinterService>();
                var printerService = new AdaptivePrinterService(logger, loggerFactory);

                // Initialize the printer
                await printerService.InitializePrinterAsync();

                // Test text printing
                var testText = $"Adaptive Printer Test\nDate: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nDevice: {GetDeviceInfo()}\n";
                result.TextPrintingWorking = await printerService.PrintTextAsync(testText);

                // Test QR code printing
                result.QRCodePrintingWorking = await printerService.PrintQRCodeAsync("ADAPTIVE_PRINTER_TEST", 200, 200);

                // Test print job execution
                result.PrintJobExecutionWorking = await printerService.StartPrintJobAsync();

                Debug.WriteLine($"{TAG}: Printing functionality test completed - Text: {result.TextPrintingWorking}, QR: {result.QRCodePrintingWorking}, Job: {result.PrintJobExecutionWorking}");
            }
            catch (Exception ex)
            {
                result.PrintingTestError = ex.Message;
                Debug.WriteLine($"{TAG}: Printing functionality test error: {ex.Message}");
            }
        }

        private static string GetDeviceInfo()
        {
            try
            {
                return $"{Android.OS.Build.Manufacturer} {Android.OS.Build.Model}";
            }
            catch
            {
                return "Unknown Device";
            }
        }
    }

    /// <summary>
    /// Result of adaptive printer diagnostic tests
    /// </summary>
    public class AdaptivePrinterDiagnosticResult
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        // Device Detection Tests
        public bool DeviceDetectionWorking { get; set; }
        public string DeviceDetectionError { get; set; }
        public List<StrategyTestResult> StrategyTestResults { get; set; } = new List<StrategyTestResult>();

        // Initialization Service Tests
        public bool InitializationServiceWorking { get; set; }
        public string InitializationServiceError { get; set; }
        public string SuccessfulStrategyName { get; set; }

        // Printer Service Tests
        public bool PrinterServiceWorking { get; set; }
        public string PrinterServiceError { get; set; }
        public string PrinterServiceDiagnostics { get; set; }

        // Printing Functionality Tests
        public bool PrintingTestSkipped { get; set; }
        public bool TextPrintingWorking { get; set; }
        public bool QRCodePrintingWorking { get; set; }
        public bool PrintJobExecutionWorking { get; set; }
        public string PrintingTestError { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine("=== Adaptive Printer Diagnostic Report ===");
            result.AppendLine($"Test Duration: {Duration.TotalSeconds:F2} seconds");
            result.AppendLine($"Overall Success: {Success}");
            
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                result.AppendLine($"Error: {ErrorMessage}");
            }

            result.AppendLine();
            result.AppendLine("=== Device Detection & Strategies ===");
            result.AppendLine($"Device Detection Working: {DeviceDetectionWorking}");
            
            if (!string.IsNullOrEmpty(DeviceDetectionError))
            {
                result.AppendLine($"Device Detection Error: {DeviceDetectionError}");
            }

            foreach (var strategy in StrategyTestResults)
            {
                result.AppendLine($"Strategy: {strategy.StrategyName}");
                result.AppendLine($"  Can Handle Device: {strategy.CanHandleDevice}");
                result.AppendLine($"  Initialization Success: {strategy.InitializationSuccess}");
                
                if (!string.IsNullOrEmpty(strategy.InitializationError))
                {
                    result.AppendLine($"  Initialization Error: {strategy.InitializationError}");
                }
                
                if (!string.IsNullOrEmpty(strategy.TestError))
                {
                    result.AppendLine($"  Test Error: {strategy.TestError}");
                }
            }

            result.AppendLine();
            result.AppendLine("=== Adaptive Services ===");
            result.AppendLine($"Initialization Service Working: {InitializationServiceWorking}");
            
            if (!string.IsNullOrEmpty(InitializationServiceError))
            {
                result.AppendLine($"Initialization Service Error: {InitializationServiceError}");
            }
            
            if (!string.IsNullOrEmpty(SuccessfulStrategyName))
            {
                result.AppendLine($"Successful Strategy: {SuccessfulStrategyName}");
            }

            result.AppendLine($"Printer Service Working: {PrinterServiceWorking}");
            
            if (!string.IsNullOrEmpty(PrinterServiceError))
            {
                result.AppendLine($"Printer Service Error: {PrinterServiceError}");
            }

            if (!string.IsNullOrEmpty(PrinterServiceDiagnostics))
            {
                result.AppendLine("Printer Service Diagnostics:");
                result.AppendLine(PrinterServiceDiagnostics);
            }

            result.AppendLine();
            result.AppendLine("=== Printing Functionality ===");
            
            if (PrintingTestSkipped)
            {
                result.AppendLine("Printing tests skipped (printer service not working)");
            }
            else
            {
                result.AppendLine($"Text Printing: {TextPrintingWorking}");
                result.AppendLine($"QR Code Printing: {QRCodePrintingWorking}");
                result.AppendLine($"Print Job Execution: {PrintJobExecutionWorking}");
                
                if (!string.IsNullOrEmpty(PrintingTestError))
                {
                    result.AppendLine($"Printing Test Error: {PrintingTestError}");
                }
            }

            return result.ToString();
        }
    }

    /// <summary>
    /// Result of testing a specific initialization strategy
    /// </summary>
    public class StrategyTestResult
    {
        public string StrategyName { get; set; }
        public bool CanHandleDevice { get; set; }
        public bool InitializationSuccess { get; set; }
        public string InitializationError { get; set; }
        public string TestError { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
} 