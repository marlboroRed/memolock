using memolock.ViewModels;

namespace memolock.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // 페이지가 나타날 때마다 메모 목록을 로드합니다.
        _viewModel.LoadMemosCommand.Execute(null);
    }
}