using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wms_android.Interfaces;

namespace wms_android.Services
{
    /// <summary>
    /// Device-agnostic printer service that automatically adapts to different printer types
    /// and provides a unified printing interface
    /// </summary>
    public class AdaptivePrinterService : IPrinterService
    {
        private readonly ILogger<AdaptivePrinterService> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AdaptivePrinterInitializationService _initializationService;
        private IPrinterService? _underlyingService;
        private bool _isInitialized = false;
        private readonly object _lockObject = new object();

        public AdaptivePrinterService(ILogger<AdaptivePrinterService> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            var initLogger = loggerFactory.CreateLogger<AdaptivePrinterInitializationService>();
            _initializationService = new AdaptivePrinterInitializationService(initLogger);
        }

        public async Task<bool> InitializePrinterAsync()
        {
            lock (_lockObject)
            {
                if (_isInitialized && _underlyingService != null)
                {
                    _logger.LogDebug("Printer already initialized");
                    return true;
                }
            }

            try
            {
                _logger.LogInformation("Starting adaptive printer initialization...");
                
                var initResult = await _initializationService.InitializePrinterAsync();
                if (!initResult)
                {
                    _logger.LogError("Failed to initialize printer with any strategy");
                    return false;
                }

                // Get the successful strategy and create the appropriate service
                var strategy = _initializationService.GetSuccessfulStrategy();
                _underlyingService = await CreateServiceForStrategy(strategy);

                if (_underlyingService != null)
                {
                    // Initialize the underlying service
                    var serviceInitResult = await _underlyingService.InitializePrinterAsync();
                    
                    lock (_lockObject)
                    {
                        _isInitialized = serviceInitResult;
                    }

                    _logger.LogInformation("Adaptive printer service initialized successfully with strategy: {Strategy}", 
                        strategy.StrategyName);
                    return serviceInitResult;
                }
                else
                {
                    _logger.LogError("Failed to create underlying printer service for strategy: {Strategy}", 
                        strategy?.StrategyName ?? "Unknown");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during adaptive printer initialization");
                return false;
            }
        }

        public async Task<bool> PrintTextAsync(string textContent)
        {
            if (!await EnsureInitialized())
            {
                _logger.LogError("Cannot print text - printer not initialized");
                return false;
            }

            try
            {
                return await _underlyingService.PrintTextAsync(textContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing text");
                return false;
            }
        }

        public async Task<bool> PrintQRCodeAsync(string data, int width = 200, int height = 200)
        {
            if (!await EnsureInitialized())
            {
                _logger.LogError("Cannot print QR code - printer not initialized");
                return false;
            }

            try
            {
                return await _underlyingService.PrintQRCodeAsync(data, width, height);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing QR code");
                return false;
            }
        }

        public async Task<bool> StartPrintJobAsync()
        {
            if (!await EnsureInitialized())
            {
                _logger.LogError("Cannot start print job - printer not initialized");
                return false;
            }

            try
            {
                return await _underlyingService.StartPrintJobAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting print job");
                return false;
            }
        }

        public async Task<string> GetPrinterStatusAsync()
        {
            if (!await EnsureInitialized())
            {
                _logger.LogError("Cannot get printer status - printer not initialized");
                return "Not Initialized";
            }

            try
            {
                return await _underlyingService.GetPrinterStatusAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting printer status");
                return $"Error: {ex.Message}";
            }
        }

        public void ClosePrinter()
        {
            try
            {
                _underlyingService?.ClosePrinter();
                
                lock (_lockObject)
                {
                    _isInitialized = false;
                    _underlyingService = null;
                }
                
                _initializationService.Reset();
                _logger.LogInformation("Adaptive printer service closed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing printer");
            }
        }

        /// <summary>
        /// Reset the adaptive printer service to force re-detection
        /// </summary>
        public async Task<bool> ResetAndReinitializeAsync()
        {
            lock (_lockObject)
            {
                _isInitialized = false;
                _underlyingService = null;
            }

            _initializationService.Reset();
            _logger.LogInformation("Adaptive printer service reset, attempting reinitialization...");
            
            return await InitializePrinterAsync();
        }

        /// <summary>
        /// Get diagnostics information about the current printer setup
        /// </summary>
        public string GetDiagnostics()
        {
            try
            {
                var strategy = _initializationService.GetSuccessfulStrategy();
                var deviceInfo = GetDeviceInfo();
                
                return $"Adaptive Printer Service Diagnostics:\n" +
                       $"Initialized: {_isInitialized}\n" +
                       $"Strategy Used: {strategy?.StrategyName ?? "None"}\n" +
                       $"Underlying Service: {_underlyingService?.GetType().Name ?? "None"}\n" +
                       $"Device Info: {deviceInfo}\n" +
                       $"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            }
            catch (Exception ex)
            {
                return $"Error getting diagnostics: {ex.Message}";
            }
        }

        private async Task<bool> EnsureInitialized()
        {
            lock (_lockObject)
            {
                if (_isInitialized && _underlyingService != null)
                {
                    return true;
                }
            }

            _logger.LogDebug("Printer not initialized, attempting initialization...");
            return await InitializePrinterAsync();
        }

        private async Task<IPrinterService> CreateServiceForStrategy(IPrinterInitializationStrategy strategy)
        {
            try
            {
                switch (strategy.StrategyName)
                {
                    case "CH10 Printer with AIDL Service":
                        _logger.LogDebug("Creating CH10PrinterService for strategy");
                        return new CH10PrinterService();
                    case "CS30 AndroidPosSdk":
                        _logger.LogDebug("Creating CS30PrinterService for strategy");
                        var cs30Logger = _loggerFactory.CreateLogger<CS30PrinterService>();
                        return new CS30PrinterService(cs30Logger);

                    case "Vanstone A90 with SystemInit":
                    case "Generic Vanstone":
                        _logger.LogDebug("Creating VanstonePrinterService for strategy");
                        var vanstoneLogger = _loggerFactory.CreateLogger<VanstonePrinterService>();
                        return new VanstonePrinterService(vanstoneLogger);

                    case "Universal Fallback":
                        // For universal fallback, try to determine the best service based on what worked
                        var fallbackStrategy = strategy as UniversalPrinterInitializationStrategy;
                        // We need to check the metadata to see which method succeeded
                        // For now, default to CS30 service as it's more commonly available
                        _logger.LogDebug("Creating fallback service (defaulting to CS30PrinterService)");
                        var fallbackLogger = _loggerFactory.CreateLogger<CS30PrinterService>();
                        return new CS30PrinterService(fallbackLogger);

                    default:
                        _logger.LogWarning("Unknown strategy name: {Strategy}, defaulting to CS30PrinterService", 
                            strategy.StrategyName);
                        var defaultLogger = _loggerFactory.CreateLogger<CS30PrinterService>();
                        return new CS30PrinterService(defaultLogger);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating printer service for strategy: {Strategy}", 
                    strategy?.StrategyName ?? "Unknown");
                return null;
            }
        }

        private string GetDeviceInfo()
        {
            try
            {
                return $"{Android.OS.Build.Manufacturer} {Android.OS.Build.Model} " +
                       $"(Android {Android.OS.Build.VERSION.Release})";
            }
            catch (Exception ex)
            {
                return $"Error getting device info: {ex.Message}";
            }
        }
    }
} 