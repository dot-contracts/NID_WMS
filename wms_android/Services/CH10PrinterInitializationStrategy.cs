using Microsoft.Extensions.Logging;
using wms_android.Interfaces;
using wms_android.shared.Models;
using System.Collections.Generic;

namespace wms_android.Services
{
    /// <summary>
    /// CH10 printer initialization strategy for CH10 POS terminals
    /// </summary>
    public class CH10PrinterInitializationStrategy : IPrinterInitializationStrategy
    {
        private const string TAG = "CH10PrinterInit";
        private readonly ILogger _logger;

        public string StrategyName => "CH10 Printer with AIDL Service";
        public int Priority => 1; // High priority for specific device support

        public CH10PrinterInitializationStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> CanHandleDeviceAsync()
        {
            try
            {
                // Check if this is a CH10 device by looking at device model/manufacturer
                var deviceModel = Android.OS.Build.Model;
                var deviceManufacturer = Android.OS.Build.Manufacturer;
                
                _logger.LogDebug($"CH10Strategy: Device Model: {deviceModel}, Manufacturer: {deviceManufacturer}");
                
                // CH10 devices typically have specific model names or manufacturers
                // You may need to adjust these values based on actual CH10 device properties
                bool isCH10Device = deviceModel?.Contains("CH10") == true || 
                                   deviceModel?.Contains("H10") == true ||
                                   deviceManufacturer?.ToLower().Contains("sunmi") == true ||
                                   deviceManufacturer?.ToLower().Contains("ch") == true;

                if (isCH10Device)
                {
                    _logger.LogInformation($"CH10Strategy: Detected CH10-compatible device - Model: {deviceModel}, Manufacturer: {deviceManufacturer}");
                }
                else
                {
                    _logger.LogDebug($"CH10Strategy: Not a CH10 device - Model: {deviceModel}, Manufacturer: {deviceManufacturer}");
                }

                // For testing purposes, always return true to test CH10 service availability
                // In production, you would want to check specific device properties
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "CH10Strategy: Error checking device compatibility");
                return false;
            }
        }

        public async Task<PrinterInitializationResult> InitializeAsync()
        {
            var result = new PrinterInitializationResult { StrategyUsed = StrategyName };
            
            try
            {
                _logger.LogInformation("CH10Strategy: Attempting to initialize CH10 printer service");
                
                var ch10Service = new CH10PrinterService();
                var initResult = await ch10Service.InitializeAsync();
                
                result.Metadata["ServiceInitialization"] = initResult ? "Success" : "Failed";
                
                if (initResult)
                {
                    _logger.LogInformation("CH10Strategy: CH10 printer service initialized successfully");
                    
                    // Test basic connectivity
                    var status = await ch10Service.GetPrinterStatusAsync();
                    result.Metadata["PrinterStatus"] = status;
                    _logger.LogInformation($"CH10Strategy: Printer status: {status}");
                    
                    // Test scanner status
                    var scannerStatus = await ch10Service.GetScannerStatusAsync();
                    result.Metadata["ScannerStatus"] = scannerStatus;
                    
                    // Clean up test service
                    ch10Service.Dispose();
                    
                    result.Success = true;
                    return result;
                }
                else
                {
                    _logger.LogWarning("CH10Strategy: CH10 printer service initialization failed");
                    ch10Service.Dispose();
                    result.ErrorMessage = "CH10 printer service initialization failed";
                    return result;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "CH10Strategy: Exception during initialization");
                result.ErrorMessage = $"CH10 initialization exception: {ex.Message}";
                return result;
            }
        }

        public IPrinterService CreatePrinterService()
        {
            _logger.LogInformation("CH10Strategy: Creating CH10 printer service");
            return new CH10PrinterService();
        }

        public PosDeviceType GetDeviceType()
        {
            return PosDeviceType.CH10;
        }
    }
}