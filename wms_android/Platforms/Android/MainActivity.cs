using Android.App;
using Android.Content.PM;
using Android.OS;
using wms_android.Services;
using System.Threading.Tasks;
using Java.Lang;
using Android.Util;
using System;

namespace wms_android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private const string TAG = "PrinterDirectTest";
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                
                // We'll attempt to initialize the printer in the background, but this shouldn't 
                // block the app startup or crash if it fails
                Task.Run(InitializePrinterAsync);
            }
            catch (System.Exception ex)
            {
                // Log any unexpected exceptions but don't let them crash the app
                Log.Error(TAG, $"Unhandled exception in OnCreate: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
            }
        }
        
        private async Task InitializePrinterAsync()
        {
            try
            {
                // Wait a moment for the app to fully initialize
                await Task.Delay(2000);
                
                Log.Debug(TAG, "Starting printer initialization sequence");
                
                // Optional: Check if classes are available first
                if (IsPrinterSDKAvailable())
                {
                    await InitializePrinterWithReflectionAsync();
                }
                else
                {
                    Log.Info(TAG, "Printer SDK not available, skipping direct initialization");
                }
                
                // Wait a bit more before trying the service initialization
                await Task.Delay(1000);
                
                // Try the service-based printer initialization as fallback
                Log.Debug(TAG, "Attempting printer initialization through service");
                try
                {
                    PrinterInitializationService.ResetPrinter();
                    PrinterInitializationService.Initialize();
                    Log.Debug(TAG, $"PrinterInitializationService result: {PrinterInitializationService.IsInitialized}");
                    Log.Debug(TAG, $"Printer status: {PrinterInitializationService.GetPrinterStatus()}");
                }
                catch (System.Exception ex)
                {
                    Log.Error(TAG, $"Error in printer service initialization: {ex.Message}");
                    // We'll continue anyway - printer functionality is optional
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(TAG, $"Critical error in printer initialization: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
                // Continue app execution even if printer initialization fails
            }
        }
        
        private bool IsPrinterSDKAvailable()
        {
            try
            {
                bool sdkEngineAvailable = IsClassAvailable("com.vanstone.appsdk.api.engine.SdkApiEngine");
                bool printerApiAvailable = IsClassAvailable("com.vanstone.trans.api.PrinterApi");
                bool printerHandlerAvailable = IsClassAvailable("com.vanstone.appsdk.api.printer.PrinterHandler");
                
                Log.Debug(TAG, $"SDK Classes availability: SdkEngine={sdkEngineAvailable}, PrinterApi={printerApiAvailable}, PrinterHandler={printerHandlerAvailable}");
                
                return sdkEngineAvailable && printerApiAvailable && printerHandlerAvailable;
            }
            catch (System.Exception ex)
            {
                Log.Error(TAG, $"Error checking printer SDK availability: {ex.Message}");
                return false;
            }
        }
        
        private async Task InitializePrinterWithReflectionAsync()
        {
            try
            {
                // Force class loading
                Class.ForName("com.vanstone.trans.api.PrinterApi");
                Class.ForName("com.vanstone.appsdk.api.printer.PrinterHandler");
                Log.Debug(TAG, "PrinterApi and PrinterHandler classes loaded successfully");
                
                // Wait a moment for classes to load
                await Task.Delay(500);
                
                var engineType = Class.ForName("com.vanstone.appsdk.api.engine.SdkApiEngine");
                Log.Debug(TAG, "SdkApiEngine class found");
                
                var instanceMethod = engineType.GetMethod("getInstance");
                var apiEngine = instanceMethod.Invoke(null, new Java.Lang.Object[0]);
                Log.Debug(TAG, "SdkApiEngine instance obtained");
                
                // Initialize the APIs
                var initMethod = engineType.GetMethod("initAPIs", new Class[] { 
                    Class.FromType(typeof(Android.Content.Context)) 
                });
                
                var result = (bool)initMethod.Invoke(apiEngine, new Java.Lang.Object[] { this });
                Log.Debug(TAG, $"SDK initialization result: {result}");
                
                if (result)
                {
                    // Get the printer handler
                    var getPrinterMethod = engineType.GetMethod("getPrinterHandler");
                    var printer = getPrinterMethod.Invoke(apiEngine, new Java.Lang.Object[0]);
                    
                    if (printer != null)
                    {
                        Log.Debug(TAG, "Printer handler obtained");
                        
                        // Set the handler in PrinterApi
                        try
                        {
                            var printerField = Class.ForName("com.vanstone.trans.api.PrinterApi")
                                                    .GetDeclaredField("mPrinter");
                            printerField.Accessible = true;
                            printerField.Set(null, printer);
                            Log.Debug(TAG, "Successfully set PrinterHandler in PrinterApi field");
                        }
                        catch (System.Exception ex)
                        {
                            Log.Error(TAG, $"Error setting printer handler: {ex.Message}");
                        }
                        
                        // Check if printer is available
                        var printerClass = printer.Class;
                        var checkMethod = printerClass.GetMethod("checkPrinter");
                        var checkResult = (int)checkMethod.Invoke(printer, new Java.Lang.Object[0]);
                        Log.Debug(TAG, $"Printer check result: {checkResult}");
                    }
                    else
                    {
                        Log.Error(TAG, "Failed to get printer handler from SDK");
                    }
                }
                else
                {
                    Log.Error(TAG, "SDK initialization failed");
                }
            }
            catch (System.Exception ex) 
            {
                Log.Error(TAG, $"Error in SDK initialization: {ex.Message}");
                Log.Error(TAG, $"Stack trace: {ex.StackTrace}");
            }
        }
        
        // Helper method to check if a class is available
        private bool IsClassAvailable(string className)
        {
            try
            {
                var cls = Class.ForName(className);
                return cls != null;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
