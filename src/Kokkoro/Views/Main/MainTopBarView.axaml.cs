using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using Kokkoro.ViewModels.Main;
using ReactiveUI.Avalonia;

namespace Kokkoro.Views.Main;

public partial class MainTopBarView : ReactiveUserControl<MainTopBarViewModel>
{
    private readonly WindowTransientDismissController _dismissController;

    public MainTopBarView()
    {
        InitializeComponent();
        //_dismissController = new WindowTransientDismissController(this, HidePageActionsFlyout);
        //DetachedFromVisualTree += OnDetachedFromVisualTree;
    }

    private void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        DetachedFromVisualTree -= OnDetachedFromVisualTree;
        _dismissController.Dispose();
    }

    private async void OnAboutMenuItemClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        HidePageActionsFlyout();

        if (Application.Current is not App app || TopLevel.GetTopLevel(this) is not Window ownerWindow)
        {
            return;
        }

        await app.ShowAboutWindow(ownerWindow);
    }

    private async void OnSettingsMenuItemClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        HidePageActionsFlyout();

        if (Application.Current is not App app || TopLevel.GetTopLevel(this) is not Window ownerWindow)
        {
            return;
        }

        await app.ShowSettingsWindow(ownerWindow);
    }

    private void HidePageActionsFlyout()
    {
        if (PageActionsButton.Flyout is MenuFlyout menuFlyout)
        {
            menuFlyout.Hide();
        }
    }

    private void OnBorderPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            if (this.VisualRoot is Window window)
                window.BeginMoveDrag(e);
        }
    }
}
