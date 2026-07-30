using Avalonia.Controls;
using Avalonia.VisualTree;
using Kokkoro.Core.Apps;
using Kokkoro.ViewModels.Auth;
using Kokkoro.ViewModels.Main;
using Ursa.Controls;

namespace Kokkoro.Views.Main;

public partial class UserAccountButtonView : ReactiveUserControl<UserAccountButtonViewModel>
{
    private readonly WindowTransientDismissController _dismissController;

    public UserAccountButtonView()
    {
        InitializeComponent();
        ViewModel = AppRuntime.Service.Resolve<UserAccountButtonViewModel>();
        //_dismissController = new WindowTransientDismissController(this, HideFlyout);
        ViewModel.SignOutRequested += OnSignOutRequested;
        //DetachedFromVisualTree += OnDetachedFromVisualTree;
    }

    private void OnAccountButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.Flyout is Flyout accountFlyout)
        {
            accountFlyout.ShowAt(button);
        }
    }

    private async void OnSignOutRequested(object? sender, EventArgs e)
    {
        if (Application.Current is not App app)
        {
            return;
        }

        if (TopLevel.GetTopLevel(this) is not Window window)
        {
            return;
        }

        HideFlyout();

        var result = await OverlayMessageBox.ShowAsync(
            "Confirm sign out and return to the sign-in screen?",
            "Sign out",
            icon: MessageBoxIcon.Question,
            button: MessageBoxButton.OKCancel);

        if (result == MessageBoxResult.OK)
        {
            ViewModel?.CurrentUser.SignOut();
            //app.ShowAuthWindow(window);
            app.SwitchToWindow<AuthWindowViewModel>(window, true);
        }
    }

    private void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        DetachedFromVisualTree -= OnDetachedFromVisualTree;

        if (ViewModel is not null)
        {
            ViewModel.SignOutRequested -= OnSignOutRequested;
        }

        _dismissController.Dispose();
    }

    private void HideFlyout()
    {
        if (AccountButton.Flyout is Flyout accountFlyout)
        {
            accountFlyout.Hide();
        }
    }
}
