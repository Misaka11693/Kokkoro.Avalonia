using Kokkoro.Core.Helpers;
using Kokkoro.Core.ViewModels;
using Kokkoro.Core.Workbench.Managers.DockLayoutManagers;
using Kokkoro.Core.Workbench.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Core.Workbench.Regions.Sidebar;

public partial class SidebarViewModel : ViewModelBase
{
    private readonly IDockLayoutManager _dockLayoutManager;
    private readonly IReadOnlyList<MenuItemViewModel> _navigationItems;

    [Reactive]
    private bool _isCollapsed;

    [Reactive]
    private MenuItemViewModel? _selectedMenuItem;

    public SidebarViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;
        _navigationItems = CreateNavigationItems();
        ShowHome();
    }

    public IReadOnlyList<MenuItemViewModel> NavigationItems => _navigationItems;

    public void ShowHome()
    {
        _dockLayoutManager.ShowHome();
        // _selectedMenuItem = FindNavigationItem(NavigationRoutes.Home);
    }

    private MenuItemViewModel[] CreateNavigationItems()
    {
        return
        [
            new MenuItemViewModel("workspace", "Workspace")
            {
                Icon = MenuItemUtilities.GetIcon("SemiIconGridSquare"),
                Children =
                [
                    // CreatePageMenuItem(NavigationRoutes.Home, "SemiIconHome"),
                    // CreatePageMenuItem(NavigationRoutes.Users, "SemiIconUserGroup"),
                    // CreatePageMenuItem(NavigationRoutes.Roles, "SemiIconUserGroup")
                ]
            },
            new MenuItemViewModel("system", "System")
            {
                Icon = MenuItemUtilities.GetIcon("SemiIconSetting"),
                Children =
                [
                    // CreatePageMenuItem(NavigationRoutes.Settings, "SemiIconSetting"),
                    // CreatePageMenuItem(NavigationRoutes.Colors, "SemiIconContrast")
                ]
            }
        ];
    }

    private MenuItemViewModel CreatePageMenuItem(string routeKey, string iconResourceKey)
    {
        var sourceItem = _dockLayoutManager.NavigationItems.FirstOrDefault(item =>string.Equals(item.Key, routeKey, StringComparison.OrdinalIgnoreCase));
        var title = sourceItem?.Title ?? routeKey;

        return new MenuItemViewModel(routeKey, title)
        {
            Icon = MenuItemUtilities.GetIcon(iconResourceKey),
            ActivateCommand = ReactiveCommand.Create(() => OpenMenu(routeKey))
        };
    }

    private MenuItemViewModel? FindNavigationItem(string routeKey)
    {
        return MenuItemUtilities.FindByKey(NavigationItems, routeKey);
    }

    private void OpenMenu(string routeKey)
    {
        _dockLayoutManager.OpenOrActivate(routeKey);
    }
}
