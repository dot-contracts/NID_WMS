using System;
using System.Diagnostics;
using wms_android.Interfaces;
using wms_android.Services;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Extensions.Logging;

namespace wms_android.Utils
{
    /// <summary>
    /// Factory class for getting the appropriate printer implementation based on device model
    /// </summary>
    public static class PrinterFactory
    {
        private const string TAG = "PrinterFactory";

        /// <summary>
        /// Returns a printer implementation appropriate for the current device
        /// </summary>
        public static object GetPrinterImplementation()
        {
            string deviceModel = Android.OS.Build.Model;
            System.Diagnostics.Debug.WriteLine($"{TAG}: Detecting printer implementation for device: {deviceModel}");

            // Check if it's an A90 device (Vanstone)
            if (deviceModel.Contains("A90") || deviceModel.Contains("Vanstone"))
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Detected A90/Vanstone device, using VanstonePosApiHelper");
                return VanstonePosApiHelper.Instance;
            }
            // Check if it's a CS30 device
            else if (deviceModel.Contains("CS30") || deviceModel.Contains("Android"))
            {
                System.Diagnostics.Debug.WriteLine($"{TAG}: Detected CS30 device, using CS30PosApiHelper");
                return CS30PosApiHelper.Instance;
            }
            else
            {
                // Default to Vanstone implementation for unknown devices
                System.Diagnostics.Debug.WriteLine($"{TAG}: Unknown device model, defaulting to VanstonePosApiHelper");
                return VanstonePosApiHelper.Instance;
            }
        }

        /// <summary>
        /// Check if the current device supports printing
        /// </summary>
        public static bool DeviceSupportsPrinting()
        {
            string deviceModel = Android.OS.Build.Model;
            
            // Check for known printer-capable devices
            return deviceModel.Contains("A90") || 
                   deviceModel.Contains("Vanstone") || 
                   deviceModel.Contains("CS30") ||
                   deviceModel.Contains("POS");
        }
        
        /// <summary>
        /// Returns the device model name
        /// </summary>
        public static string GetDeviceModel()
        {
            return Android.OS.Build.Model;
        }
        
        /// <summary>
        /// Check if the current device is an A90 model
        /// </summary>
        public static bool IsA90Device()
        {
            string deviceModel = Android.OS.Build.Model;
            return deviceModel.Contains("A90") || deviceModel.Contains("Vanstone");
        }
        
        /// <summary>
        /// Check if the current device is a CS30 model
        /// </summary>
        public static bool IsCS30Device()
        {
            string deviceModel = Android.OS.Build.Model;
            return deviceModel.Contains("CS30");
        }

        public static IPrinterService CreateCS30PrinterService()
        {
            var cs30ApiHelper = CS30PosApiHelper.Instance;
            if (cs30ApiHelper != null)
            {
                // For now, we'll pass a simple logger - ideally this should come from DI
                var loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<CS30PrinterService>();
                return new CS30PrinterService(logger);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CS30PosApiHelper.Instance is null - cannot create CS30PrinterService");
                return null;
            }
        }
    }
} 