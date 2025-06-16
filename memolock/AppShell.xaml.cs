using memolock.Views;
using memolock.Services;

namespace memolock;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // 페이지 라우팅(이동 경로) 등록
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(SecuritySetupPage), typeof(SecuritySetupPage));
        Routing.RegisterRoute(nameof(MemoDetailPage), typeof(MemoDetailPage));
        Routing.RegisterRoute(nameof(MemoEditPage), typeof(MemoEditPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // SecurityService를 수동으로 가져옵니다.
        var securityService = IPlatformApplication.Current.Services.GetService<SecurityService>();

        if (securityService != null && !securityService.IsSetupComplete())
        {
            // 보안 설정이 완료되지 않았으면 설정 페이지로 이동
            await GoToAsync($"//{nameof(SecuritySetupPage)}");
        }
        else
        {
            // 설정이 완료되었으면 잠금 페이지로 이동
            await GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}