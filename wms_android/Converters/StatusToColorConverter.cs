using System.Globalization;
using Microsoft.Maui.Graphics;
using wms_android.shared.Models;

namespace wms_android.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        // Define colors based on our new color scheme
        private static readonly Color PrimaryColor = Color.FromArgb("#40916C");     // Medium Green
        private static readonly Color PrimaryDarkColor = Color.FromArgb("#1B4332"); // Dark Forest Green
        private static readonly Color PrimaryLightColor = Color.FromArgb("#74C69D"); // Lighter Green
        private static readonly Color BackgroundColor = Color.FromArgb("#D8F3DC");   // Light Mint Green
        
        // Additional colors for various statuses
        private static readonly Color WarningColor = Color.FromArgb("#FF9800");     // Orange for warnings
        private static readonly Color ErrorColor = Color.FromArgb("#B71C1C");       // Dark Red for errors
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Handle both enum values and string representations
            if (value is ParcelStatus status)
            {
                return GetColorForStatus(status);
            }
            else if (value is string statusStr && Enum.TryParse<ParcelStatus>(statusStr, out var parsedStatus))
            {
                return GetColorForStatus(parsedStatus);
            }
            
            return Colors.Gray;
        }

        private Color GetColorForStatus(ParcelStatus status)
        {
            return status switch
            {
                ParcelStatus.Delivered => PrimaryColor,       // Delivered uses our primary green
                ParcelStatus.Pending => WarningColor,         // Pending uses orange warning color
                ParcelStatus.Finalized => PrimaryDarkColor,   // Finalized uses dark forest green
                ParcelStatus.InTransit => PrimaryLightColor,  // In transit uses lighter green
                ParcelStatus.Cancelled => ErrorColor,         // Cancelled uses error red
                _ => Colors.Gray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for one-way binding
            throw new NotImplementedException();
        }
    }
} 