namespace memolock;

public partial class App : Application
{
    public App(AppShell appShell)
    {
        InitializeComponent();

        // MauiProgram.cs에 등록된 AppShell을 메인 페이지로 지정합니다.
        MainPage = appShell;
    }
}