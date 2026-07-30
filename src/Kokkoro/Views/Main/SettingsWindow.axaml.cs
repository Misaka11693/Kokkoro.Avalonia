using Avalonia;
using Kokkoro.ViewModels.Main;
using Ursa.Controls;
using Ursa.ReactiveUIExtension;

namespace Kokkoro.Views.Main;

public partial class SettingsWindow : ReactiveUrsaWindow<SettingsWindowViewModel>
{
    public SettingsWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
        Closed += OnClosed;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        ViewModel?.SetNotificationManager(
            WindowNotificationManager.TryGetNotificationManager(this, out var manager)
                ? manager
                : new WindowNotificationManager(this) { MaxItems = 1 });
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        ViewModel?.SetNotificationManager(null);
    }
}
