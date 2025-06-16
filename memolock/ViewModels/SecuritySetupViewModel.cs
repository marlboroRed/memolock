using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using memolock.Services;

namespace memolock.ViewModels
{
    public partial class SecuritySetupViewModel : ObservableObject
    {
        private readonly SecurityService _securityService;

        [ObservableProperty]
        private string _password = string.Empty;
        [ObservableProperty]
        private string _confirmPassword = string.Empty;
        [ObservableProperty]
        private string _errorMessage = string.Empty;
        [ObservableProperty]
        private bool _hasError;

        public SecuritySetupViewModel(SecurityService securityService)
        {
            _securityService = securityService;
        }

        [RelayCommand]
        private async Task CompleteSetupAsync()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "비밀번호를 입력해주세요.";
                HasError = true;
                return;
            }
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "비밀번호가 일치하지 않습니다.";
                HasError = true;
                return;
            }

            HasError = false;
            await _securityService.SetAppPasswordAsync(Password);
            _securityService.SetLockMethod("Password"); // 기본 잠금은 비밀번호로 설정
            _securityService.SetSetupComplete();

            // 설정이 완료되면 메인 페이지로 이동
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
