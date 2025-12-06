using System;
using System.Threading.Tasks;
using wms_android.Interfaces;
using Microsoft.Extensions.Logging;

namespace wms_android.Services
{
    // Simple test class to validate session timeout functionality
    public class SessionTimeoutTest
    {
        public static async Task TestSessionTimeout()
        {
            // Create a session timeout service with 5-second timeout for testing
            var sessionTimeout = new TestSessionTimeoutService(TimeSpan.FromSeconds(5));
            
            bool sessionExpired = false;
            sessionTimeout.SessionExpired += (sender, e) => 
            {
                sessionExpired = true;
                Console.WriteLine("Session expired after inactivity!");
            };

            Console.WriteLine("Starting session timeout test...");
            sessionTimeout.StartSession();
            
            // Wait 3 seconds, then reset (should not expire)
            await Task.Delay(3000);
            sessionTimeout.ResetSession();
            Console.WriteLine("Session reset after 3 seconds");
            
            // Wait another 3 seconds, then reset again
            await Task.Delay(3000);
            sessionTimeout.ResetSession();
            Console.WriteLine("Session reset after another 3 seconds");
            
            // Now wait 6 seconds without reset (should expire)
            await Task.Delay(6000);
            
            if (sessionExpired)
            {
                Console.WriteLine("✅ Test PASSED: Session expired as expected");
            }
            else
            {
                Console.WriteLine("❌ Test FAILED: Session should have expired");
            }
        }
    }

    // Test version of SessionTimeoutService with configurable timeout
    public class TestSessionTimeoutService : ISessionTimeoutService
    {
        private System.Threading.Timer _timer;
        private readonly TimeSpan _timeoutDuration;
        private bool _isActive;

        public event EventHandler SessionExpired;
        public bool IsActive => _isActive;

        public TestSessionTimeoutService(TimeSpan timeoutDuration)
        {
            _timeoutDuration = timeoutDuration;
        }

        public void StartSession()
        {
            _isActive = true;
            ResetSession();
        }

        public void ResetSession()
        {
            if (!_isActive) return;
            
            _timer?.Dispose();
            _timer = new System.Threading.Timer(OnSessionExpired, null, _timeoutDuration, System.Threading.Timeout.InfiniteTimeSpan);
        }

        public void StopSession()
        {
            _isActive = false;
            _timer?.Dispose();
        }

        private void OnSessionExpired(object state)
        {
            _isActive = false;
            SessionExpired?.Invoke(this, EventArgs.Empty);
        }
    }
}