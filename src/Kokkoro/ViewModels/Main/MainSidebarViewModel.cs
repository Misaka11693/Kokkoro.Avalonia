using Kokkoro.Core.MetaModels;
using Kokkoro.Services;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Main;

public partial class MainSidebarViewModel : ViewModelBase
{
    private readonly IDockLayoutManager _dockLayoutManager;
    private readonly IReadOnlyList<MenuItemMeta> _navigationItems;

    [Reactive]
    private bool _isCollapsed;

    [Reactive]
    private MenuItemMeta? _selectedMenuItem;

    public MainSidebarViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;
        _navigationItems = CreateNavigationItems();
        //ShowHome();
    }

    public IReadOnlyList<MenuItemMeta> NavigationItems => _navigationItems;

    public void ShowHome()
    {
        _dockLayoutManager.ShowHome();
        //_selectedMenuItem = FindNavigationItem(NavigationRoutes.Home);
    }

    private IReadOnlyList<MenuItemMeta> CreateNavigationItems()
    {
        var items = MenuManager.Items;
        BindMenuCommands(items);
        return items;

        //return
        //[
        //    new MenuItemViewModel("workspace", "工作区","SemiIconGridSquare")
        //    {
        //        Children =
        //        [
        //            CreatePageMenuItem(NavigationRoutes.Home, "SemiIconHome"),
        //            CreatePageMenuItem(NavigationRoutes.Users, "SemiIconUserGroup"),
        //            CreatePageMenuItem(NavigationRoutes.Roles, "SemiIconUserGroup"),
        //            CreatePageMenuItem(NavigationRoutes.Tree, "SemiIconTreeSelect"),
        //            CreatePageMenuItem(NavigationRoutes.SimpleTree, "SemiIconTree")
        //        ]
        //    },
        //    new MenuItemViewModel("system", "系统","SemiIconSetting")
        //    {
        //        Children =
        //        [
        //            CreatePageMenuItem(NavigationRoutes.Settings, "SemiIconSetting"),
        //            CreatePageMenuItem(NavigationRoutes.Colors, "SemiIconContrast"),
        //            new MenuItemViewModel("message-feedback", "消息反馈")
        //            {
        //                Icon = MenuItemUtilities.GetIcon("SemiIconComment"),
        //                Children =
        //                [
        //                    CreatePageMenuItem(NavigationRoutes.MessagesDialogs, "SemiIconComment"),
        //                    CreatePageMenuItem(NavigationRoutes.MessagesNotifications, "SemiIconBell"),
        //                    CreatePageMenuItem(NavigationRoutes.MessagesToasts, "SemiIconInfoCircle"),
        //                    CreatePageMenuItem(NavigationRoutes.OverlayDialogs, "SemiIconLayers")
        //                ]
        //            }
        //        ]
        //    }
        //];
    }

    private MenuItemMeta CreatePageMenuItem(string routeKey, string iconResourceKey)
    {
        var sourceItem = _dockLayoutManager.NavigationItems.FirstOrDefault(item => string.Equals(item.Key, routeKey, StringComparison.OrdinalIgnoreCase));
        var title = sourceItem?.Title ?? routeKey;

        return new MenuItemMeta(routeKey, title)
        {
            Icon = MenuItemUtilities.GetIcon(iconResourceKey),
            //ActivateCommand = ReactiveCommand.Create(() => OpenMenu(routeKey))
        };
    }

    //private MenuItemMeta? FindNavigationItem(string routeKey)
    //{
    //    //return MenuItemUtilities.FindByKey(NavigationItems, routeKey);
    //}

    private void BindMenuCommands(IEnumerable<MenuItemMeta> items)
    {
        foreach (var item in items)
        {
            if (item.EntityType != null && !item.IsSeparator)
            {
                item.ActivateCommand = ReactiveCommand.Create(() => OpenMenu(item));
            }

            // 递归处理子菜单项
            if (item.Children != null && item.Children.Any())
            {
                BindMenuCommands(item.Children);
            }
        }
    }

    private void OpenMenu(MenuItemMeta meta)
    {
        var pageMeta = new PageMeta()
        {
            Key = meta.Key,
            Title = meta.Title,
            EntityType = meta.EntityType,
            Icon = meta.Icon,
        };
        _dockLayoutManager.OpenOrActivate(pageMeta);
    }
}
