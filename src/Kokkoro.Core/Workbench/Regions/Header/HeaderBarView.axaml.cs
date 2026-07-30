using Avalonia;
using Avalonia.Controls;
using ReactiveUI.Avalonia;

namespace Kokkoro.Core.Workbench.Regions.Header;

public partial class HeaderBarView : ReactiveUserControl<HeaderBarViewModel>
{

    public HeaderBarView()
    {
        InitializeComponent();
    }



    private async void OnAboutMenuItemClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        HidePageActionsFlyout();

        // if (Application.Current is not App app || TopLevel.GetTopLevel(this) is not Window ownerWindow)
        // {
        //     return;
        // }
        //
        // await app.ShowAboutWindow(ownerWindow);
    }

    private async void OnSettingsMenuItemClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // HidePageActionsFlyout();
        //
        // if (Application.Current is not App app || TopLevel.GetTopLevel(this) is not Window ownerWindow)
        // {
        //     return;
        // }
        //
        // await app.ShowSettingsWindow(ownerWindow);
    }

    private void HidePageActionsFlyout()
    {
        if (PageActionsButton.Flyout is MenuFlyout menuFlyout)
        {
            menuFlyout.Hide();
        }
    }
}
