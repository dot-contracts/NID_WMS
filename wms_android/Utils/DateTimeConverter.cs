using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace wms_android.Utils
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Check if the date is UTC and convert to local time
                if (dateTime.Kind == DateTimeKind.Utc)
                {
                    return dateTime.ToLocalTime();
                }
                // If it's already local or unspecified, return as-is
                return dateTime;
            }
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for this converter
            return value;
        }
    }
} 