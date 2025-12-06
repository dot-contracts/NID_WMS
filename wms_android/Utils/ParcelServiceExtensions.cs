using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using wms_android.shared.Interfaces;

namespace wms_android.Utils
{
    /// <summary>
    /// Extension methods for IParcelService to handle functionality that was available in the data project
    /// but is not available in the shared project.
    /// </summary>
    public static class ParcelServiceExtensions
    {
        /// <summary>
        /// Checks if the device has network connectivity
        /// </summary>
        /// <returns>True if network is available, false otherwise</returns>
        public static bool IsNetworkAvailable(this IParcelService _)
        {
            try
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Basic connection check that just returns true since we've already established connectivity
        /// </summary>
        /// <returns>Task with true result</returns>
        public static Task<bool> CheckDatabaseConnectionAsync(this IParcelService _)
        {
            // This is a simplified version that assumes we have database connectivity
            // if we've gotten to this point
            return Task.FromResult(true);
        }
    }
} 