using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace memolock.Converters
{
    public class BoolToStarIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? "star_filled.png" : "star_empty.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
