using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Content;
using Microsoft.Extensions.Logging;
using wms_android.Interfaces;
using wms_android.shared.Models;

namespace wms_android.Services
{
    /// <summary>
    /// Adaptive printer initialization service that can work with multiple device types
    /// and automatically detect the best initialization strategy
    /// </summary>
    public class AdaptivePrinterInitializationService
    {
        private const string TAG = "AdaptivePrinterInit";
        private readonly ILogger<AdaptivePrinterInitializationService> _logger;
        private readonly List<IPrinterInitializationStrategy> _strategies;
        private IPrinterInitializationStrategy _successfulStrategy;
        private bool _isInitialized = false;
        private readonly object _lockObject = new object();

        public AdaptivePrinterInitializationService(ILogger<AdaptivePrinterInitializationService> logger)
        {
            _logger = logger;
            _strategies = new List<IPrinterInitializationStrategy>();
            InitializeStrategies();
        }

        /// <summary>
        /// Initialize all available printer strategies in order of preference
        /// </summary>
        private void InitializeStrategies()
        {
            // Strategy 1: CH10 with AIDL Service (for CH10 POS terminals)
            _strategies.Add(new CH10PrinterInitializationStrategy(_logger));
            
            // Strategy 2: CS30 with AndroidPosSdk (most specific)
            _strategies.Add(new CS30PrinterInitializationStrategy(_logger));
            
            // Strategy 3: Vanstone A90 with SystemInit (requires system initialization)
            _strategies.Add(new VanstoneA90PrinterInitializationStrategy(_logger));
            
            // Strategy 4: Generic Vanstone (fallback for Vanstone-like devices)
            _strategies.Add(new GenericVanstonePrinterInitializationStrategy(_logger));
            
            // Strategy 5: Universal fallback (attempts basic initialization patterns)
            _strategies.Add(new UniversalPrinterInitializationStrategy(_logger));

            _logger.LogInformation($"Initialized {_strategies.Count} printer initialization strategies");
        }

        /// <summary>
        /// Attempt to initialize the printer using all available strategies
        /// </summary>
        public async Task<bool> InitializePrinterAsync()
        {
            lock (_lockObject)
            {
                if (_isInitialized && _successfulStrategy != null)
                {
                    _logger.LogDebug("Printer already initialized with strategy: {Strategy}", 
                        _successfulStrategy.GetType().Name);
                    return true;
                }
            }

            _logger.LogInformation("Starting adaptive printer initialization...");
            var deviceInfo = GetDeviceInfo();
            _logger.LogInformation("Device Info: {DeviceInfo}", deviceInfo);

            foreach (var strategy in _strategies)
            {
                try
                {
                    _logger.LogDebug("Attempting initialization with strategy: {Strategy}", 
                        strategy.GetType().Name);

                    if (await strategy.CanHandleDeviceAsync())
                    {
                        _logger.LogDebug("Strategy {Strategy} can handle this device, attempting initialization...", 
                            strategy.GetType().Name);

                        var result = await strategy.InitializeAsync();
                        if (result.Success)
                        {
                            lock (_lockObject)
                            {
                                _successfulStrategy = strategy;
                                _isInitialized = true;
                            }

                            _logger.LogInformation("Printer successfully initialized with strategy: {Strategy}", 
                                strategy.GetType().Name);
                            _logger.LogDebug("Initialization result: {Result}", result.ToString());
                            return true;
                        }
                        else
                        {
                            _logger.LogWarning("Strategy {Strategy} failed: {Error}", 
                                strategy.GetType().Name, result.ErrorMessage);
                        }
                    }
                    else
                    {
                        _logger.LogDebug("Strategy {Strategy} cannot handle this device", 
                            strategy.GetType().Name);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Strategy {Strategy} threw an exception", 
                        strategy.GetType().Name);
                }
            }

            _logger.LogError("All printer initialization strategies failed");
            return false;
        }

        /// <summary>
        /// Get the currently successful strategy (if any)
        /// </summary>
        public IPrinterInitializationStrategy GetSuccessfulStrategy()
        {
            lock (_lockObject)
            {
                return _successfulStrategy;
            }
        }

        /// <summary>
        /// Reset initialization state to force re-detection
        /// </summary>
        public void Reset()
        {
            lock (_lockObject)
            {
                _isInitialized = false;
                _successfulStrategy = null;
            }
            _logger.LogInformation("Printer initialization state reset");
        }

        /// <summary>
        /// Get comprehensive device information for logging
        /// </summary>
        private string GetDeviceInfo()
        {
            try
            {
                return $"Manufacturer: {Android.OS.Build.Manufacturer}, " +
                       $"Model: {Android.OS.Build.Model}, " +
                       $"Brand: {Android.OS.Build.Brand}, " +
                       $"Device: {Android.OS.Build.Device}, " +
                       $"Hardware: {Android.OS.Build.Hardware}, " +
                       $"Product: {Android.OS.Build.Product}, " +
                       $"Android: {Android.OS.Build.VERSION.Release}";
            }
            catch (Exception ex)
            {
                return $"Error getting device info: {ex.Message}";
            }
        }
    }

    /// <summary>
    /// Interface for printer initialization strategies
    /// </summary>
    public interface IPrinterInitializationStrategy
    {
        /// <summary>
        /// Check if this strategy can handle the current device
        /// </summary>
        Task<bool> CanHandleDeviceAsync();

        /// <summary>
        /// Attempt to initialize the printer
        /// </summary>
        Task<PrinterInitializationResult> InitializeAsync();

        /// <summary>
        /// Get the name of this strategy
        /// </summary>
        string StrategyName { get; }
    }

    /// <summary>
    /// Result of a printer initialization attempt
    /// </summary>
    public class PrinterInitializationResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string StrategyUsed { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public override string ToString()
        {
            var metadata = Metadata.Count > 0 ? $", Metadata: {string.Join(", ", Metadata)}" : "";
            return $"Success: {Success}, Strategy: {StrategyUsed}, Error: {ErrorMessage ?? "None"}{metadata}";
        }
    }

    /// <summary>
    /// Strategy for CS30 devices using AndroidPosSdk
    /// </summary>
    public class CS30PrinterInitializationStrategy : IPrinterInitializationStrategy
    {
        private readonly ILogger _logger;
        public string StrategyName => "CS30 AndroidPosSdk";

        public CS30PrinterInitializationStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> CanHandleDeviceAsync()
        {
            try
            {
                // Check for CS30-specific indicators
                var model = Android.OS.Build.Model.ToUpperInvariant();
                var hasCS30Model = model.Contains("CS30");
                
                // Check if CS30 SDK classes are available
                var hasCS30SDK = IsClassAvailable("com.ctk.sdk.PosApiHelper");
                
                // Check if CS30PosApiHelper instance can be created
                var hasValidInstance = false;
                try
                {
                    var instance = Utils.CS30PosApiHelper.Instance;
                    hasValidInstance = instance != null;
                }
                catch
                {
                    hasValidInstance = false;
                }

                var canHandle = hasCS30Model || (hasCS30SDK && hasValidInstance);
                _logger.LogDebug("CS30 Strategy - Model: {HasModel}, SDK: {HasSDK}, Instance: {HasInstance}, CanHandle: {CanHandle}",
                    hasCS30Model, hasCS30SDK, hasValidInstance, canHandle);
                
                return canHandle;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking CS30 device compatibility");
                return false;
            }
        }

        public async Task<PrinterInitializationResult> InitializeAsync()
        {
            var result = new PrinterInitializationResult { StrategyUsed = StrategyName };
            
            try
            {
                var cs30Helper = Utils.CS30PosApiHelper.Instance;
                if (cs30Helper == null)
                {
                    result.ErrorMessage = "CS30PosApiHelper instance is null";
                    return result;
                }

                // Reset any previous initialization state
                cs30Helper.ResetInitializationState();
                
                // Use proper CS30 initialization parameters
                var initResult = cs30Helper.PrintInit(3, 24, 0, 0);
                
                result.Success = initResult == 0;
                result.Metadata["InitResult"] = initResult;
                
                if (!result.Success)
                {
                    result.ErrorMessage = $"CS30 PrintInit failed with code: {initResult}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"CS30 initialization exception: {ex.Message}";
                return result;
            }
        }

        private bool IsClassAvailable(string className)
        {
            try
            {
                var javaClass = Java.Lang.Class.ForName(className);
                return javaClass != null;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Strategy for Vanstone A90 devices requiring SystemInit
    /// </summary>
    public class VanstoneA90PrinterInitializationStrategy : IPrinterInitializationStrategy
    {
        private readonly ILogger _logger;
        public string StrategyName => "Vanstone A90 with SystemInit";

        public VanstoneA90PrinterInitializationStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> CanHandleDeviceAsync()
        {
            try
            {
                var model = Android.OS.Build.Model.ToUpperInvariant();
                var hasA90Model = model.Contains("A90") || model.Contains("VANSTONE");
                
                var hasVanstoneSDK = IsClassAvailable("com.vanstone.trans.api.PrinterApi") &&
                                   IsClassAvailable("com.vanstone.trans.api.SystemApi");
                
                var canHandle = hasA90Model && hasVanstoneSDK;
                _logger.LogDebug("A90 Strategy - Model: {HasModel}, SDK: {HasSDK}, CanHandle: {CanHandle}",
                    hasA90Model, hasVanstoneSDK, canHandle);
                
                return canHandle;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking A90 device compatibility");
                return false;
            }
        }

        public async Task<PrinterInitializationResult> InitializeAsync()
        {
            var result = new PrinterInitializationResult { StrategyUsed = StrategyName };
            
            try
            {
                var context = Android.App.Application.Context;
                if (context == null)
                {
                    result.ErrorMessage = "Android context is null";
                    return result;
                }

                // Step 1: System initialization (critical for A90)
                try
                {
                    Com.Vanstone.Trans.Api.SystemApi.SystemInit_Api(0, new byte[0], context);
                    await Task.Delay(1000); // Allow system to initialize
                    result.Metadata["SystemInit"] = "Success";
                }
                catch (Exception ex)
                {
                    result.Metadata["SystemInit"] = $"Failed: {ex.Message}";
                    _logger.LogWarning("SystemInit_Api failed, continuing anyway: {Error}", ex.Message);
                }

                // Step 2: Printer initialization
                Com.Vanstone.Trans.Api.PrinterApi.SetContext(context);
                await Task.Delay(200);

                var openResult = Com.Vanstone.Trans.Api.PrinterApi.PrnOpen_Api("", context);
                result.Metadata["OpenResult"] = openResult;
                
                if (openResult == 0)
                {
                    Com.Vanstone.Trans.Api.PrinterApi.PrnClrBuff_Api();
                    Com.Vanstone.Trans.Api.PrinterApi.PrnSetGray_Api(5);
                    result.Success = true;
                }
                else
                {
                    result.ErrorMessage = $"PrnOpen_Api failed with code: {openResult}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"A90 initialization exception: {ex.Message}";
                return result;
            }
        }

        private bool IsClassAvailable(string className)
        {
            try
            {
                var javaClass = Java.Lang.Class.ForName(className);
                return javaClass != null;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Strategy for generic Vanstone devices (without SystemInit requirement)
    /// </summary>
    public class GenericVanstonePrinterInitializationStrategy : IPrinterInitializationStrategy
    {
        private readonly ILogger _logger;
        public string StrategyName => "Generic Vanstone";

        public GenericVanstonePrinterInitializationStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> CanHandleDeviceAsync()
        {
            try
            {
                var hasVanstoneSDK = IsClassAvailable("com.vanstone.trans.api.PrinterApi");
                _logger.LogDebug("Generic Vanstone Strategy - SDK: {HasSDK}", hasVanstoneSDK);
                return hasVanstoneSDK;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking generic Vanstone compatibility");
                return false;
            }
        }

        public async Task<PrinterInitializationResult> InitializeAsync()
        {
            var result = new PrinterInitializationResult { StrategyUsed = StrategyName };
            
            try
            {
                var context = Android.App.Application.Context;
                if (context == null)
                {
                    result.ErrorMessage = "Android context is null";
                    return result;
                }

                Com.Vanstone.Trans.Api.PrinterApi.SetContext(context);
                await Task.Delay(200);

                var openResult = Com.Vanstone.Trans.Api.PrinterApi.PrnOpen_Api("", context);
                result.Metadata["OpenResult"] = openResult;
                
                if (openResult == 0)
                {
                    Com.Vanstone.Trans.Api.PrinterApi.PrnClrBuff_Api();
                    Com.Vanstone.Trans.Api.PrinterApi.PrnSetGray_Api(3);
                    result.Success = true;
                }
                else
                {
                    result.ErrorMessage = $"PrnOpen_Api failed with code: {openResult}";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"Generic Vanstone initialization exception: {ex.Message}";
                return result;
            }
        }

        private bool IsClassAvailable(string className)
        {
            try
            {
                var javaClass = Java.Lang.Class.ForName(className);
                return javaClass != null;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Universal fallback strategy that attempts common initialization patterns
    /// </summary>
    public class UniversalPrinterInitializationStrategy : IPrinterInitializationStrategy
    {
        private readonly ILogger _logger;
        public string StrategyName => "Universal Fallback";

        public UniversalPrinterInitializationStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> CanHandleDeviceAsync()
        {
            // This strategy can always attempt to handle a device as a last resort
            return true;
        }

        public async Task<PrinterInitializationResult> InitializeAsync()
        {
            var result = new PrinterInitializationResult { StrategyUsed = StrategyName };
            
            try
            {
                // Try to find any available printer-related service
                var attempts = new List<string>();
                
                // Attempt 1: Check if any printer helper instances exist
                try
                {
                    var vanstonHelper = Utils.VanstonePosApiHelper.Instance;
                    if (vanstonHelper != null)
                    {
                        var initResult = vanstonHelper.PrintInit(2, 2, 0, 0);
                        attempts.Add($"Vanstone: {initResult}");
                        if (initResult == 0)
                        {
                            result.Success = true;
                            result.Metadata["Method"] = "Vanstone";
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    attempts.Add($"Vanstone failed: {ex.Message}");
                }

                // Attempt 2: Try CS30 if available
                try
                {
                    var cs30Helper = Utils.CS30PosApiHelper.Instance;
                    if (cs30Helper != null)
                    {
                        var initResult = cs30Helper.PrintInit(2, 24, 0, 0);
                        attempts.Add($"CS30: {initResult}");
                        if (initResult == 0)
                        {
                            result.Success = true;
                            result.Metadata["Method"] = "CS30";
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    attempts.Add($"CS30 failed: {ex.Message}");
                }

                result.ErrorMessage = $"All universal attempts failed: {string.Join(", ", attempts)}";
                result.Metadata["Attempts"] = attempts.Count;
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = $"Universal strategy exception: {ex.Message}";
                return result;
            }
        }
    }
} 