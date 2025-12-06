using System;
using System.Threading;
using wms_android.Interfaces;
using Microsoft.Extensions.Logging;

namespace wms_android.Services
{
    public class SessionTimeoutService : ISessionTimeoutService
    {
        private Timer _timer;
        private readonly TimeSpan _timeoutDuration = TimeSpan.FromMinutes(10);
        private readonly ILogger<SessionTimeoutService> _logger;
        private bool _isActive;

        public event EventHandler SessionExpired;

        public bool IsActive => _isActive;

        public SessionTimeoutService(ILogger<SessionTimeoutService> logger = null)
        {
            _logger = logger;
            _isActive = false;
        }

        public void StartSession()
        {
            _logger?.LogInformation("Starting session timeout timer for 10 minutes");
            _isActive = true;
            ResetSession();
        }

        public void ResetSession()
        {
            if (!_isActive) return;

            _logger?.LogDebug("Resetting session timeout timer");
            _timer?.Dispose();
            _timer = new Timer(OnSessionExpired, null, _timeoutDuration, Timeout.InfiniteTimeSpan);
        }

        public void StopSession()
        {
            _logger?.LogInformation("Stopping session timeout timer");
            _isActive = false;
            _timer?.Dispose();
            _timer = null;
        }

        private void OnSessionExpired(object state)
        {
            _logger?.LogWarning("Session expired due to inactivity");
            _isActive = false;
            _timer?.Dispose();
            _timer = null;
            
            // Fire the event on the main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SessionExpired?.Invoke(this, EventArgs.Empty);
            });
        }

        public void Dispose()
        {
            StopSession();
        }
    }
}