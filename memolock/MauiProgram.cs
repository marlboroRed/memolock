using Microsoft.Extensions.Logging;
using memolock.Services;
using memolock.ViewModels;
using memolock.Views;
using CommunityToolkit.Maui;

namespace memolock;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 필요한 서비스 등록 (예시)
        // builder.Services.AddSingleton<MainPage>();
        // builder.Services.AddSingleton<MemoViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
