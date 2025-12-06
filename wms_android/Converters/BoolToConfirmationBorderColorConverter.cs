using System.Globalization;

namespace wms_android.Converters
{
    public class BoolToConfirmationBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isConfirmed)
            {
                return isConfirmed ? Color.FromArgb("#51CF66") : Color.FromArgb("#E0E0E0");
            }
            return Color.FromArgb("#E0E0E0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}