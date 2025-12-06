using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace wms_android.Converters
{
    public class InitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string username && !string.IsNullOrEmpty(username))
            {
                var words = username.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1)
                {
                    return username.Length >= 2 ? username.Substring(0, 2).ToUpper() : username.ToUpper();
                }
                else if (words.Length >= 2)
                {
                    return (words[0][0].ToString() + words[1][0].ToString()).ToUpper();
                }
                return username.Substring(0, 1).ToUpper();
            }
            return "U";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}