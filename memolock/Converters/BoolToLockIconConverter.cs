using System.Globalization;

namespace memolock.Converters
{
    // bool 값을 아이콘 파일 이름으로 변환하는 컨버터
    public class BoolToLockIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 값이 true이면 "lock_icon.png", false이면 투명한 아이콘(또는 null) 반환
            return (bool)value ? "lock_icon.png" : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
