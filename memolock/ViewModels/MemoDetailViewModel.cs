using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using memolock.Models;
using memolock.Services;

namespace memolock.ViewModels
{
    [QueryProperty(nameof(Id), "Id")]
    public partial class MemoDetailViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Memo _memo = new();

        [ObservableProperty]
        private int _id;

        public MemoDetailViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        partial void OnIdChanged(int value) => LoadMemoAsync(value);

        private async void LoadMemoAsync(int id)
        {
            Memo = await _databaseService.GetMemoAsync(id);
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            bool answer = await Shell.Current.DisplayAlert("삭제 확인", "정말로 삭제하시겠습니까? 이 작업은 복구할 수 없습니다.", "삭제", "취소");
            if (answer)
            {
                await _databaseService.DeleteMemoAsync(Memo);
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            // 편집 페이지로 이동하면서 현재 메모 객체를 전달합니다.
            var navigationParameter = new Dictionary<string, object>
                {
                    { "Memo", Memo }
                };
            await Shell.Current.GoToAsync($"../{nameof(Views.MemoEditPage)}", navigationParameter);
        }

        [RelayCommand]
        private async Task ShareAsync()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = Memo.Title,
                Text = Memo.Content
            });
        }

        [RelayCommand]
        private async Task SettingsAsync()
        {
            // TODO: 설정 기능 구현 (비밀번호 변경, 숨김 처리 등)
            await Shell.Current.DisplayAlert("알림", "설정 기능은 구현 예정입니다.", "확인");
        }
    }
}
