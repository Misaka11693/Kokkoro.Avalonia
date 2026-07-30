using Kokkoro.ViewModels.Core;
using Kokkoro.ViewModels.Main.Settings;
using Kokkoro.Core.Apps;
using ReactiveUI.SourceGenerators;
using WindowNotificationManager = Ursa.Controls.WindowNotificationManager;

namespace Kokkoro.ViewModels.Main;

public sealed partial class SettingsWindowViewModel : ViewModelBase
{
    private readonly IReadOnlyList<ISettingsNotificationAware> _notificationAwareSections;

    [Reactive]
    private MenuItemViewModel? _selectedMenuItem;

    public SettingsWindowViewModel()
    {
        SettingsMenuItems = CreateMenuItems();

        _notificationAwareSections = MenuItemUtilities.EnumerateLeafItems(SettingsMenuItems)
            .Select(item => item.Content)
            .OfType<ISettingsNotificationAware>()
            .ToArray();
    }

    public IReadOnlyList<MenuItemViewModel> SettingsMenuItems { get; }

    private MenuItemViewModel[] CreateMenuItems()
    {
        var appearanceSection = AppRuntime.Service.Resolve<AppearanceSettingsSectionViewModel>();
        var generalSection = AppRuntime.Service.Resolve<GeneralSettingsSectionViewModel>();
        var aboutSection = AppRuntime.Service.Resolve<AboutSettingsSectionViewModel>();

        return
        [
            new MenuItemViewModel("preferences", "首选项")
            {
                Icon = MenuItemUtilities.GetIcon("SemiIconSetting"),
                Children =
                [
                    new MenuItemViewModel("appearance", "外观")
                    {
                        Icon = MenuItemUtilities.GetIcon("SemiIconContrast"),
                        Content = appearanceSection
                    },
                    new MenuItemViewModel("general", "通用")
                    {
                        Icon = MenuItemUtilities.GetIcon("SemiIconHome"),
                        Content = generalSection
                    }
                ]
            },
            new MenuItemViewModel("information", "信息")
            {
                Icon = MenuItemUtilities.GetIcon("SemiIconHelpCircle"),
                Children =
                [
                    new MenuItemViewModel("about", "关于")
                    {
                        Icon = MenuItemUtilities.GetIcon("SemiIconInfoCircle"),
                        Content = aboutSection
                    }
                ]
            }
        ];
    }

    public void SetNotificationManager(WindowNotificationManager? notificationManager)
    {
        foreach (var section in _notificationAwareSections)
        {
            section.SetNotificationManager(notificationManager);
        }
    }
}
