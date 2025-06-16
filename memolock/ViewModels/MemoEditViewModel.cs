using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using memolock.Models;
using memolock.Services;

namespace memolock.ViewModels
{
    [QueryProperty(nameof(Memo), "Memo")]
    public partial class MemoEditViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Memo _memo = new();

        [ObservableProperty]
        private string _password = string.Empty;

        public MemoEditViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task SaveMemoAsync()
        {
            if (string.IsNullOrWhiteSpace(Memo.Title))
            {
                await Shell.Current.DisplayAlert("오류", "제목을 입력해주세요.", "확인");
                return;
            }

            if (Memo.IsPasswordProtected && string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("오류", "비밀번호를 입력해주세요.", "확인");
                return;
            }

            if (Memo.IsPasswordProtected)
            {
                Memo.PasswordHash = SecurityService.HashPassword(Password);
            }
            else
            {
                Memo.PasswordHash = string.Empty;
                Memo.PasswordHint = string.Empty;
            }

            Memo.ModifiedDate = DateTime.Now;
            if (Memo.Id == 0) // 새 메모
            {
                Memo.CreationDate = DateTime.Now;
            }

            await _databaseService.SaveMemoAsync(Memo);
            await Shell.Current.GoToAsync(".."); // 이전 페이지로 돌아가기
        }
    }
}
