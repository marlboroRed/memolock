using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using memolock.Services;

namespace memolock.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly SecurityService _securityService;

        [ObservableProperty] private string _password = string.Empty;
        [ObservableProperty] private string _errorMessage = string.Empty;
        [ObservableProperty] private bool _hasError;
        [ObservableProperty] private bool _isPasswordMode;
        [ObservableProperty] private bool _isBiometricMode;

        public LoginViewModel(SecurityService securityService)
        {
            _securityService = securityService;
            Initialize();
        }

        private void Initialize()
        {
            string lockMethod = _securityService.GetLockMethod();
            if (lockMethod == "Biometric")
            {
                IsBiometricMode = true;
                IsPasswordMode = false;
                // 앱이 시작되자마자 생체인증 시도
                BiometricLoginCommand.Execute(null);
            }
            else
            {
                IsPasswordMode = true;
                IsBiometricMode = false;
            }
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            var success = await _securityService.VerifyAppPasswordAsync(Password);
            if (success)
            {
                HasError = false;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                ErrorMessage = "비밀번호가 올바르지 않습니다.";
                HasError = true;
            }
        }

        [RelayCommand]
        private async Task BiometricLoginAsync()
        {
            var success = await _securityService.AuthenticateByBiometricsAsync();
            if (success)
            {
                HasError = false;
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                // 생체인증 실패 시 (취소 포함) 비밀번호 모드로 전환할 수 있도록 안내
                ErrorMessage = "인증에 실패했습니다.";
                HasError = true;
            }
        }

        [RelayCommand]
        private void SwitchToPasswordMode()
        {
            IsPasswordMode = true;
            IsBiometricMode = false;
        }
    }
}
