using System;

namespace wms_android.Interfaces
{
    public interface ISessionTimeoutService
    {
        /// <summary>
        /// Starts the session timeout timer
        /// </summary>
        void StartSession();

        /// <summary>
        /// Resets the session timeout timer (called on user activity)
        /// </summary>
        void ResetSession();

        /// <summary>
        /// Stops the session timeout timer
        /// </summary>
        void StopSession();

        /// <summary>
        /// Event fired when the session expires due to inactivity
        /// </summary>
        event EventHandler SessionExpired;

        /// <summary>
        /// Gets whether the session timeout is currently active
        /// </summary>
        bool IsActive { get; }
    }
}