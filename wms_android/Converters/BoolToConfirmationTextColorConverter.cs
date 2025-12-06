using System.Globalization;

namespace wms_android.Converters
{
    public class BoolToConfirmationTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isConfirmed)
            {
                return isConfirmed ? Color.FromArgb("#51CF66") : Color.FromArgb("#FF6B6B");
            }
            return Color.FromArgb("#FF6B6B");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}