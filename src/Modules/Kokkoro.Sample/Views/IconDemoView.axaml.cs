using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Kokkoro.Core.Apps;
using Kokkoro.Sample.ViewModels;
using ReactiveUI.Avalonia;

namespace Kokkoro.Sample.Views;

public partial class IconDemoView : ReactiveUserControl<IconDemoViewModel>
{
    public IconDemoView()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        ViewModel?.InitializeResources();
    }

    private async void IconCard_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button { DataContext: IconItem icon })
        {
            return;
        }

        var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
        if (clipboard is not null)
        {
            await clipboard.SetTextAsync(icon.ResourceKey);
            await AppRuntime.ToastService.ShowSuccessAsync("Icon copied to clipboard!");
        }
    }
}
