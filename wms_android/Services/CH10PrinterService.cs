using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using wms_android.Interfaces;
using wms_android.shared.Models;
using SysException = System.Exception;

namespace wms_android.Services
{
    public class CH10PrinterService : ICH10PrinterService
    {
        private readonly Context _context;
        private IServiceConnection? _serviceConnection;
        private Recieptservice.Com.Recieptservice.IPrinterInterface? _printerInterface;
        private bool _isServiceBound = false;
        private readonly TaskCompletionSource<bool> _serviceBindingTask = new();

        public CH10PrinterService()
        {
            _context = Platform.CurrentActivity?.ApplicationContext ?? Android.App.Application.Context;
        }

        public async Task<bool> InitializeAsync()
        {
            if (_isServiceBound)
                return true;

            try
            {
                var intent = new Intent();
                intent.SetClassName("recieptservice.com.recieptservice", "recieptservice.com.recieptservice.service.PrinterService");

                _serviceConnection = new CH10ServiceConnection(this);
                var bindResult = _context.BindService(intent, _serviceConnection, Bind.AutoCreate);

                if (!bindResult)
                    return false;

                // Wait for service to bind
                return await _serviceBindingTask.Task.ConfigureAwait(false);
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10PrinterService initialization error: {ex.Message}");
                return false;
            }
        }

        public void OnServiceConnected(IBinder binder)
        {
            _printerInterface = Recieptservice.Com.Recieptservice.IPrinterInterface.Stub.AsInterface(binder);
            _isServiceBound = true;
            _serviceBindingTask.SetResult(true);
        }

        public void OnServiceDisconnected()
        {
            _printerInterface = null;
            _isServiceBound = false;
        }

        public async Task<bool> PrintReceiptAsync(string receiptContent)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                await BeginWorkAsync();
                await PrintTextAsync(receiptContent);
                await EndWorkAsync();
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print receipt error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrintTextAsync(string text)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.PrintText(text);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print text error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrintBitmapAsync(Bitmap bitmap)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.PrintBitmap(bitmap);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print bitmap error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrintQRCodeAsync(string data, int moduleSize = 4, int errorLevel = 3)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.PrintQRCode(data, moduleSize, errorLevel);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print QR code error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrintBarcodeAsync(string data, int type = 3, int height = 80, int width = 2)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.Print128BarCode(data, type, height, width);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print barcode error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SetTextBoldAsync(bool bold)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.SetTextBold(bold);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Set text bold error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SetTextSizeAsync(float textSize)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.SetTextSize(textSize);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Set text size error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SetAlignmentAsync(int alignment)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.SetAlignment(alignment);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Set alignment error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> NextLineAsync(int lines = 1)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.NextLine(lines);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Next line error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PrintTableTextAsync(string[] text, int[] weight, int[] alignment)
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.PrintTableText(text, weight, alignment);
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Print table text error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> BeginWorkAsync()
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.BeginWork();
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Begin work error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EndWorkAsync()
        {
            if (!_isServiceBound || _printerInterface == null)
                return false;

            try
            {
                _printerInterface.EndWork();
                return true;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 End work error: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetServiceVersionAsync()
        {
            if (!_isServiceBound || _printerInterface == null)
                return "Service not connected";

            try
            {
                return _printerInterface.ServiceVersion ?? "Unknown version";
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10 Get service version error: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> GetScannerStatusAsync()
        {
            // Note: getScannerStatus() method is not available in the generated C# binding
            // This is likely due to AIDL binding generation issues
            // Return service bound status as a fallback
            return _isServiceBound;
        }

        public async Task<string> GetPrinterStatusAsync()
        {
            if (!_isServiceBound)
            {
                return "Service not bound";
            }

            try
            {
                var version = await GetServiceVersionAsync();
                return $"Connected - Version: {version}";
            }
            catch (SysException ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<bool> InitializePrinterAsync()
        {
            return await InitializeAsync();
        }

        public async Task<bool> StartPrintJobAsync()
        {
            return await BeginWorkAsync();
        }

        public void ClosePrinter()
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                if (_isServiceBound && _serviceConnection != null)
                {
                    _context.UnbindService(_serviceConnection);
                    _isServiceBound = false;
                }
                _printerInterface = null;
            }
            catch (SysException ex)
            {
                System.Diagnostics.Debug.WriteLine($"CH10PrinterService dispose error: {ex.Message}");
            }
        }

        private class CH10ServiceConnection : Java.Lang.Object, IServiceConnection
        {
            private readonly CH10PrinterService _printerService;

            public CH10ServiceConnection(CH10PrinterService printerService)
            {
                _printerService = printerService;
            }

            public void OnServiceConnected(ComponentName? name, IBinder? service)
            {
                if (service != null)
                    _printerService.OnServiceConnected(service);
            }

            public void OnServiceDisconnected(ComponentName? name)
            {
                _printerService.OnServiceDisconnected();
            }
        }
    }
}