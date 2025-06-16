using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;

namespace memolock.Models
{
    // ObservableObject를 상속하여 속성 변경 알림을 자동으로 처리합니다.
    public partial class Memo : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _content = string.Empty; // Rich Text Editor의 HTML 내용을 저장합니다.

        [ObservableProperty]
        private bool _isPasswordProtected;

        [ObservableProperty]
        private string _passwordHash = string.Empty;

        [ObservableProperty]
        private string _passwordHint = string.Empty;

        [ObservableProperty]
        private bool _isHidden;

        [ObservableProperty]
        private bool _isFavorite;

        [ObservableProperty]
        private bool _isPinned;

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
