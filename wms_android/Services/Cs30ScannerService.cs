using Android.App; // Required for Application.Context
using Android.Content;
using Android.OS; // Required for Build
using System;
// using System.Diagnostics; // Removing ambiguous reference
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wms_android.Interfaces;

// Ensure Permissions are added to AndroidManifest.xml if required by the SDK for broadcasts or scanner access.
// e.g., <uses-permission android:name="com.ctk.permission.SCANNER" /> (Example, check CS30 docs for actual permission)

namespace wms_android.Services
{
    [BroadcastReceiver(Enabled = true, Exported = false)] // Basic receiver attributes
    public class Cs30ScanReceiver : BroadcastReceiver
    {
        public Action<Intent> OnScanReceived; // Action to invoke when scan data is received
        private const string ScanResultAction = "ACTION_BAR_SCAN";

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Action == ScanResultAction)
            {
                System.Diagnostics.Debug.WriteLine($"[{nameof(Cs30ScanReceiver)}] Scan Received!");
                OnScanReceived?.Invoke(intent);
            }
        }
    }

    public class Cs30ScannerService : IScannerService
    {
        private const string TAG = "Cs30ScannerService";

        // Intent Actions
        private const string ActionStartScan = "ACTION_BAR_TRIGSCAN";
        private const string ActionCancelScan = "ACTION_SCANNER_CANCEL"; // From example code
        private const string ActionResult = "ACTION_BAR_SCAN";
        private const string ActionConfigure = "ACTION_BAR_SCANCFG";

        // Intent Extras
        private const string ExtraTimeout = "timeout";
        private const string ExtraScanData = "EXTRA_SCAN_DATA";
        private const string ExtraEncodingMode = "EXTRA_SCAN_ENCODE_MODE";
        private const string ExtraScanMode = "EXTRA_SCAN_MODE"; // 1=EditText, 2=API
        private const string ExtraAutoEnter = "EXTRA_SCAN_AUTOENT"; // 0=No, 1=Yes
        private const string ExtraPower = "EXTRA_SCAN_POWER"; // 0=Off, 1=On

        private readonly Context _context;
        private Cs30ScanReceiver _receiver;
        private IntentFilter _filter;
        private TaskCompletionSource<string?> _scanCompletionSource;
        private bool _isInitialized = false;
        private readonly object _lock = new object();

        public Cs30ScannerService()
        {
            // Get the application context once
            _context = Android.App.Application.Context;
        }

        public bool InitializeScanner()
        {
            lock (_lock)
            {
                if (_isInitialized)
                {
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Already initialized.");
                    return true;
                }

                System.Diagnostics.Debug.WriteLine($"[{TAG}] Initializing scanner...");
                try
                {
                    // 1. Configure Scanner Settings (send broadcast)
                    ConfigureScannerSettings();

                    // 2. Setup BroadcastReceiver
                    _filter = new IntentFilter(ActionResult);
                    _receiver = new Cs30ScanReceiver { OnScanReceived = HandleScanResult };

                    // 3. Register Receiver
                    // On Android 14+ we might need to specify RECEIVER_EXPORTED or RECEIVER_NOT_EXPORTED
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu) // API 33+
                    {
                         // For broadcasts sent only by the system or apps with same user ID,
                         // RECEIVER_NOT_EXPORTED is often appropriate if using Context.RegisterReceiver
                        _context.RegisterReceiver(_receiver, _filter, ReceiverFlags.NotExported);
                    }
                    else
                    {
                        #pragma warning disable CA1422 // Validate platform compatibility
                        _context.RegisterReceiver(_receiver, _filter);
                        #pragma warning restore CA1422
                    }

                    _isInitialized = true;
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Scanner initialized and receiver registered.");
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Initialization failed: {ex.Message}");
                    // Cleanup if partial initialization occurred
                    if (_receiver != null)
                    {
                        try { _context.UnregisterReceiver(_receiver); } catch { /* Ignore */ }
                        _receiver = null;
                    }
                    _isInitialized = false;
                    return false;
                }
            }
        }

        private void ConfigureScannerSettings()
        {
            System.Diagnostics.Debug.WriteLine($"[{TAG}] Configuring scanner settings...");
            try
            {
                using var intent = new Intent(ActionConfigure);
                intent.PutExtra(ExtraPower, 1);        // Ensure scanner power is on
                intent.PutExtra(ExtraScanMode, 2);     // Set to API/Broadcast mode
                intent.PutExtra(ExtraAutoEnter, 1);  // Automatically add 'Enter' (optional, adjust if needed)
                // Add other settings like sound, vibration, LED if desired
                // intent.PutExtra("EXTRA_SCAN_NOTY_SND", 1);
                // intent.PutExtra("EXTRA_SCAN_NOTY_VIB", 1);
                // intent.PutExtra("EXTRA_SCAN_NOTY_LED", 1);
                _context.SendBroadcast(intent);
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Configuration broadcast sent.");
            }
            catch (Exception ex)
            {
                 System.Diagnostics.Debug.WriteLine($"[{TAG}] Failed to send configuration broadcast: {ex.Message}");
                 // Continue initialization, maybe settings are already correct
            }
        }

        public async Task<string?> ScanAsync(TimeSpan timeout)
        {
            lock (_lock)
            {
                if (!_isInitialized)
                {
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Scanner not initialized. Cannot scan.");
                    // Attempt to initialize first
                    if (!InitializeScanner())
                    {
                        return null;
                    }
                }

                // Cancel previous scan attempt if any
                _scanCompletionSource?.TrySetCanceled(); 
                _scanCompletionSource = new TaskCompletionSource<string?>(TaskCreationOptions.RunContinuationsAsynchronously);
            }

            System.Diagnostics.Debug.WriteLine($"[{TAG}] Starting scan...");
            try
            {
                using var intent = new Intent(ActionStartScan);
                // Convert TimeSpan to seconds for the timeout extra, max 9
                int timeoutSeconds = Math.Min(9, (int)Math.Max(1, Math.Ceiling(timeout.TotalSeconds)));
                intent.PutExtra(ExtraTimeout, timeoutSeconds);

                _context.SendBroadcast(intent);
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Start Scan broadcast sent (Timeout: {timeoutSeconds}s).");

                // Wait for the scan result or timeout
                var completedTask = await Task.WhenAny(_scanCompletionSource.Task, Task.Delay(timeout));

                if (completedTask == _scanCompletionSource.Task)
                {
                    // Scan completed (or was cancelled by receiver setting null)
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan task completed. Result: {_scanCompletionSource.Task.Result ?? "<null>"}");
                    return _scanCompletionSource.Task.Result;
                }
                else
                {
                    // Timeout occurred
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan timed out after {timeout}.");
                     _scanCompletionSource?.TrySetCanceled(); // Ensure TCS is cancelled
                     // Optionally send a cancel scan intent?
                     // CancelScan(); 
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[{TAG}] ScanAsync failed: {ex.Message}");
                _scanCompletionSource?.TrySetException(ex); // Propagate exception
                return null;
            }
        }
        
        // Method to cancel an ongoing scan explicitly if needed
        public void CancelScan()
        {
            System.Diagnostics.Debug.WriteLine($"[{TAG}] Sending Cancel Scan broadcast...");
            _scanCompletionSource?.TrySetCanceled();
            try
            {
                using var intent = new Intent(ActionCancelScan);
                _context.SendBroadcast(intent);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Failed to send cancel scan broadcast: {ex.Message}");
            }
        }

        private void HandleScanResult(Intent intent)
        {
            if (intent == null) return;

            if (_scanCompletionSource == null || _scanCompletionSource.Task.IsCompleted)
            {
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan result received but no active scan task waiting.");
                return; // Not waiting for a scan
            }

            try
            {
                string? scannedData = null;
                
                // Get the encoding mode (if available in the Intent)
                int encodeMode = intent.GetIntExtra(ExtraEncodingMode, 0); // 0=Raw, 1=UTF8, 2=GBK (?) - Verify with docs
                
                if (encodeMode == 0) // Raw Bytes
                {
                    byte[]? dataBytes = intent.GetByteArrayExtra(ExtraScanData);
                    if (dataBytes != null && dataBytes.Length > 0)
                    {
                        // Attempt to decode as UTF-8 by default, adjust if another encoding is expected for raw bytes
                        scannedData = Encoding.UTF8.GetString(dataBytes);
                        System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan result (Raw Bytes, Decoded as UTF8): {scannedData}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan result received (Raw Bytes Mode) but data was null.");
                    }
                }
                else // UTF-8 or GBK (Treat GBK as string for now, may need specific encoding library if GBK content expected)
                {
                    scannedData = intent.GetStringExtra(ExtraScanData);
                    System.Diagnostics.Debug.WriteLine($"[{TAG}] Scan result (String Mode, Encoding={encodeMode}): {scannedData}");
                }

                // Complete the scan Task with the scanned data (null if data couldn't be extracted)
                _scanCompletionSource.TrySetResult(scannedData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Error processing scan result: {ex.Message}");
                _scanCompletionSource.TrySetException(ex);
            }
        }

        public void CloseScanner()
        {
            lock (_lock)
            {
                System.Diagnostics.Debug.WriteLine($"[{TAG}] Closing scanner...");
                if (_receiver != null)
                {
                    try
                    {
                        _context.UnregisterReceiver(_receiver);
                        System.Diagnostics.Debug.WriteLine($"[{TAG}] BroadcastReceiver unregistered.");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[{TAG}] Error unregistering receiver: {ex.Message}");
                    }
                    _receiver = null;
                }
                
                // Cancel any pending scan operation
                _scanCompletionSource?.TrySetCanceled();
                _isInitialized = false;
            }
        }

        // IDisposable pattern implementation for proper cleanup if needed
        private bool _disposed = false;

        ~Cs30ScannerService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Release managed resources
                    CloseScanner();
                }
                // Release unmanaged resources if any
                _disposed = true;
            }
        }
    }
}