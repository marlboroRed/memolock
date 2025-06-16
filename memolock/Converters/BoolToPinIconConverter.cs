using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace memolock.Converters
{
    public class BoolToPinIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b) return "pin_filled.png";
            return "pin_empty.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}           