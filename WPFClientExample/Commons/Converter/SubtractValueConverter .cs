using System.Globalization;
using System.Windows.Data;

namespace WPFClientExample.Commons.Converter
{
    public class SubtractValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double originalValue && parameter is string strParameter && double.TryParse(strParameter, out double subtractValue))
            {
                return Math.Max(0, originalValue - subtractValue);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
