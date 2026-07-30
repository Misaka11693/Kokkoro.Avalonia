using Avalonia.Controls.Notifications;
using Kokkoro.Enums;
using Kokkoro.Services;
using Kokkoro.ViewModels.Core;
using ReactiveUI;
using Notification = Ursa.Controls.Notification;
using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace Kokkoro.ViewModels.Main.Settings;

public sealed class AppearanceSettingsSectionViewModel : ViewModelBase, ISettingsNotificationAware
{
    private readonly IThemeService _themeService;
    private ThemeOptionViewModel? _selectedTheme;
    private WindowNotificationManager? _notificationManager;

    public AppearanceSettingsSectionViewModel(IThemeService themeService)
    {
        _themeService = themeService;
        _selectedTheme = ThemeOptions.First(option => option.Mode == _themeService.CurrentThemeMode);
    }

    public IReadOnlyList<ThemeOptionViewModel> ThemeOptions { get; } =
        Enum.GetValues<AppThemeMode>()
            .Select(mode => new ThemeOptionViewModel(mode))
            .ToArray();

    public ThemeOptionViewModel? SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (EqualityComparer<ThemeOptionViewModel?>.Default.Equals(_selectedTheme, value))
            {
                return;
            }

            this.RaiseAndSetIfChanged(ref _selectedTheme, value);

            if (value is null)
            {
                return;
            }

            _themeService.SetTheme(value.Mode);
            _notificationManager?.Show(
                new Notification("主题已切换", $"已切换为 {value.Label}"),
                type: NotificationType.Success,
                classes: ["Light"]);
        }
    }

    void ISettingsNotificationAware.SetNotificationManager(WindowNotificationManager? notificationManager)
    {
        _notificationManager = notificationManager;
    }
}
