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
        private string title = string.Empty;

        [ObservableProperty]
        private string content = string.Empty; // Rich Text Editor의 HTML 내용을 저장합니다.

        [ObservableProperty]
        private bool isPasswordProtected;

        [ObservableProperty]
        private string passwordHash = string.Empty;

        [ObservableProperty]
        private string passwordHint = string.Empty;

        [ObservableProperty]
        private bool isHidden;

        [ObservableProperty]
        private bool isFavorite;

        [ObservableProperty]
        private bool isPinned;

        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
