using Microsoft.Extensions.Logging;
using memolock.Services;
using memolock.ViewModels;
using memolock.Views;

namespace memolock;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // 서비스 등록 (Singleton으로 한번만 생성)
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<SecurityService>();

        // AppShell과 ViewModel들은 앱 생명주기 동안 계속 유지되어야 합니다.
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<SecuritySetupViewModel>();

        // 페이지들은 필요할 때마다 생성하고 파괴하는 것이 메모리 관리에 효율적입니다.
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<SecuritySetupPage>();

        builder.Services.AddTransient<MemoDetailViewModel>();
        builder.Services.AddTransient<MemoDetailPage>();

        builder.Services.AddTransient<MemoEditViewModel>();
        builder.Services.AddTransient<MemoEditPage>();

        builder.Services.AddTransient<SettingsPage>();
        // HiddenMemosPage와 ViewModel도 필요하다면 여기에 추가해야 합니다.

        return builder.Build();
    }
}
