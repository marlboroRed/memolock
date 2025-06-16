using memolock.ViewModels;

    namespace memolock.Views;

public partial class SecuritySetupPage : ContentPage
{
    public SecuritySetupPage(SecuritySetupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}