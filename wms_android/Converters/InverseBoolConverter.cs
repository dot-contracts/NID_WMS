using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace wms_android.Converters // Ensure this namespace matches your project structure
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            // Default behavior if the value is not a boolean or is null
            // Depending on the use case, you might want to return false, true, or throw an exception
            return false; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert back is often the same logic for a simple inverter
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }
    }
} 