using Android.Content;
using System;
using Java.Lang;
using Java.Lang.Reflect;

namespace VanstonePosSdk.Additions
{
    /// <summary>
    /// Helper class for working with the Vanstone A90 POS device
    /// </summary>
    public class VanstoneSdkHelper
    {
        private readonly Java.Lang.Object _apiEngine;
        private Context _context;

        /// <summary>
        /// Initializes a new instance of the VanstoneSdkHelper class
        /// </summary>
        /// <param name="context">The Android context</param>
        public VanstoneSdkHelper(Context context)
        {
            _context = context;
            
            try
            {
                // Get the SDK API engine using reflection since we need to verify class names
                var engineType = Class.ForName("com.vanstone.appsdk.api.engine.SdkApiEngine");
                var instanceMethod = engineType.GetMethod("getInstance");
                _apiEngine = instanceMethod.Invoke(null, new Java.Lang.Object[0]);
                
                // Initialize the SDK
                var initMethod = engineType.GetMethod("initAPIs", new Class[] { Class.FromType(typeof(Context)) });
                var result = (bool)initMethod.Invoke(_apiEngine, new Java.Lang.Object[] { _context });
                
                if (!result)
                {
                    throw new System.InvalidOperationException("Failed to initialize Vanstone SDK");
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error initializing SDK: {ex.Message}");
                throw new System.InvalidOperationException("Failed to initialize Vanstone SDK", ex);
            }
        }

        /// <summary>
        /// Prints a text receipt
        /// </summary>
        /// <param name="text">The text to print</param>
        /// <returns>True if printing was successful, false otherwise</returns>
        public bool PrintText(string text)
        {
            try
            {
                // Get the printer handler using reflection
                Java.Lang.Class engineType = _apiEngine.Class;
                Method getPrinterMethod = engineType.GetMethod("getPrinterHandler");
                var printer = getPrinterMethod.Invoke(_apiEngine, new Java.Lang.Object[0]);
                
                // Get printer type
                Java.Lang.Class printerType = printer.Class;
                
                // Check if printer is available
                Method checkMethod = printerType.GetMethod("checkPrinter");
                var checkResult = (int)checkMethod.Invoke(printer, new Java.Lang.Object[0]);
                if (checkResult != 0)
                {
                    return false;
                }
                
                // Print text
                Method printMethod = printerType.GetMethod("printStrInPosition", 
                    new Class[] { 
                        Class.FromType(typeof(string)), 
                        Class.FromType(typeof(int)), 
                        Class.FromType(typeof(int)), 
                        Class.FromType(typeof(int)), 
                        Class.FromType(typeof(int)) 
                    });
                
                printMethod.Invoke(printer, new Java.Lang.Object[] { text, 1, 0, 0, 24 });
                
                // Add space after text
                printMethod.Invoke(printer, new Java.Lang.Object[] { " ", 1, 0, 0, 24 });
                printMethod.Invoke(printer, new Java.Lang.Object[] { " ", 1, 0, 0, 24 });
                printMethod.Invoke(printer, new Java.Lang.Object[] { " ", 1, 0, 0, 24 });
                
                // Cut paper
                Method cutMethod = printerType.GetMethod("cutPaper", new Class[] { Class.FromType(typeof(int)) });
                cutMethod.Invoke(printer, new Java.Lang.Object[] { 0 });
                
                return true;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error printing text: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the device serial number
        /// </summary>
        /// <returns>The device serial number</returns>
        public string GetDeviceSerialNumber()
        {
            try
            {
                // Get the system handler using reflection
                Java.Lang.Class engineType = _apiEngine.Class;
                Method getSystemMethod = engineType.GetMethod("getSystemHandler");
                var sysHandler = getSystemMethod.Invoke(_apiEngine, new Java.Lang.Object[0]);
                
                // Get serial number
                Java.Lang.Class sysType = sysHandler.Class;
                Method getSerialMethod = sysType.GetMethod("getSerialNo");
                return (string)getSerialMethod.Invoke(sysHandler, new Java.Lang.Object[0]);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error getting serial number: {ex.Message}");
                return string.Empty;
            }
        }
    }
} 