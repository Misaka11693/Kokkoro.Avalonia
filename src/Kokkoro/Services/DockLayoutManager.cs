using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Kokkoro.Core.Apps;
using Kokkoro.Core.MetaModels;
using Kokkoro.Core.Workbench.Docking;
using Kokkoro.Docking;
using Kokkoro.ViewModels.Core;

namespace Kokkoro.Services;

public sealed class DockLayoutManager : IDockLayoutManager
{
    private readonly Docking.AppDockFactory _dockFactory;

    public DockLayoutManager(Docking.AppDockFactory dockFactory)
    {
        _dockFactory = dockFactory;

        //NavigationItems =
        //[
        //    new MenuItemViewModel(NavigationRoutes.Home, "首页"),
        //    new MenuItemViewModel(NavigationRoutes.Users, "用户"),
        //    new MenuItemViewModel(NavigationRoutes.Roles, "角色"),
        //    new MenuItemViewModel(NavigationRoutes.Tree, "树形展示"),
        //    new MenuItemViewModel(NavigationRoutes.SimpleTree, "简单树形"),
        //    new MenuItemViewModel(NavigationRoutes.Settings, "设置"),
        //    new MenuItemViewModel(NavigationRoutes.Colors, "颜色"),
        //    new MenuItemViewModel(NavigationRoutes.MessagesDialogs, "对话框演示"),
        //    new MenuItemViewModel(NavigationRoutes.MessagesNotifications, "窗口通知演示"),
        //    new MenuItemViewModel(NavigationRoutes.MessagesToasts, "轻提示演示"),
        //    new MenuItemViewModel(NavigationRoutes.OverlayDialogs, "OverlayDialog 演示"),
        //];

        //NavigationItems = MenuManager.Items;
    }

    //public IReadOnlyList<MenuItemViewModel> NavigationItems { get; }
    public IReadOnlyList<MenuItemMeta> NavigationItems { get; } = MenuManager.Items;

    private IDocumentDock DocumentDock => _dockFactory.GetDocumentDock();

    public IRootDock Layout => _dockFactory.GetLayout();

    public IDockable? ActiveDocument => _dockFactory.FindDockable(Layout, dockable => dockable is Document document && document.IsActive);

    public IReadOnlyList<Document> OpenDocuments => _dockFactory.FindAll(Layout, dockable => dockable is Document).OfType<Document>().ToArray();

    public bool HasOpenDocuments => OpenDocuments.Count > 0;

    public void OpenOrActivate(PageMeta meta)
    {
        var document = FindDocument(meta.Key!);
        if (document is null)
        {
            document = CreateDocument(meta);
            _dockFactory.InitDockable(document, DocumentDock);
            DocumentDock.AddDocument(document);
            DocumentDock.DefaultDockable ??= document;
        }

        if (document.Owner is not IDock ownerDock)
        {
            return;
        }

        ownerDock.ActiveDockable = document;
        _dockFactory.SetActiveDockable(document);
        _dockFactory.SetFocusedDockable(ownerDock, document);
        _dockFactory.ActivateWindow(document);
    }

    public void CloseActiveDocument()
    {
        if (ActiveDocument is Document document && document.Owner is IDock)
        {
            _dockFactory.CloseDockable(document);
        }
    }

    public void CloseDocument(string routeKey)
    {
        if (FindDocument(routeKey) is Document document && document.Owner is IDock)
        {
            _dockFactory.CloseDockable(document);
        }
    }

    public void CloseOtherDocuments()
    {
        if (ActiveDocument is Document document && document.Owner is IDock)
        {
            _dockFactory.CloseOtherDockables(document);
        }
    }

    public void CloseAllDocuments()
    {
        foreach (var document in OpenDocuments.ToArray())
        {
            if (document.Owner is IDock)
            {
                _dockFactory.CloseDockable(document);
            }
        }
    }

    public bool IsDocumentOpen(string routeKey)
    {
        return FindDocument(routeKey) is not null;
    }

    public void ShowHome()
    {
        //OpenOrActivate(NavigationRoutes.Home);
    }

    public void ResetToDefault()
    {
        CloseAllHostWindows();

        _dockFactory.CreateLayout();
        InitializeDocuments();
        _dockFactory.InitLayout(Layout);
        //ShowHome();
    }

    private void CloseAllHostWindows()
    {
        if (!_dockFactory.IsLayoutInitialized)
        {
            return;
        }

        if (Layout.Windows is not { Count: > 0 } windows)
        {
            return;
        }

        foreach (var window in windows.ToArray())
        {
            _dockFactory.CloseWindow(window);
        }
    }

    private void InitializeDocuments()
    {
        //Core.Workbench.Docking.DocumentPage[] documents =
        //[
        //    CreateDocument(NavigationRoutes.Home),
        //    CreateDocument(NavigationRoutes.Users),
        //    CreateDocument(NavigationRoutes.Roles),
        //    CreateDocument(NavigationRoutes.Tree),
        //    CreateDocument(NavigationRoutes.SimpleTree),
        //    CreateDocument(NavigationRoutes.Settings),
        //    CreateDocument(NavigationRoutes.Colors),
        //    CreateDocument(NavigationRoutes.MessagesDialogs),
        //    CreateDocument(NavigationRoutes.MessagesNotifications),
        //    CreateDocument(NavigationRoutes.MessagesToasts),
        //    CreateDocument(NavigationRoutes.OverlayDialogs),
        //];

        //DocumentDock.VisibleDockables = _dockFactory.CreateList(documents.Cast<IDockable>().ToArray());
        //DocumentDock.DefaultDockable = documents[0];
        //DocumentDock.ActiveDockable = documents[0];
    }

    private Core.Workbench.Docking.DocumentPage CreateDocument(PageMeta pageMeta)
    {
        //Core.Workbench.Docking.DocumentPage document = routeKey switch
        //{
        //    NavigationRoutes.Home => AppRuntime.Service.Resolve<HomePageViewModel>(),
        //    NavigationRoutes.Users => AppRuntime.Service.Resolve<UsersPageViewModel>(),
        //    NavigationRoutes.Roles => AppRuntime.Service.Resolve<RolePageViewModel>(),
        //    NavigationRoutes.Tree => AppRuntime.Service.Resolve<TreePageViewModel>(),
        //    NavigationRoutes.SimpleTree => AppRuntime.Service.Resolve<SimpleTreePageViewModel>(),
        //    NavigationRoutes.Settings => AppRuntime.Service.Resolve<SettingsPageViewModel>(),
        //    NavigationRoutes.Colors => AppRuntime.Service.Resolve<ColorsPageViewModel>(),
        //    NavigationRoutes.MessagesDialogs => AppRuntime.Service.Resolve<MessagesDialogsPageViewModel>(),
        //    NavigationRoutes.MessagesNotifications => AppRuntime.Service.Resolve<MessagesNotificationsPageViewModel>(),
        //    NavigationRoutes.MessagesToasts => AppRuntime.Service.Resolve<MessagesToastsPageViewModel>(),
        //    NavigationRoutes.OverlayDialogs => AppRuntime.Service.Resolve<OverlayDialogsPageViewModel>(),
        //    _ => throw new InvalidOperationException($"Unknown route key: {routeKey}")
        //};

        DocumentPage document = (DocumentPage)AppRuntime.Service.Resolve(pageMeta.EntityType!);
        document.Id = pageMeta.Key!;
        document.Title = pageMeta.Title!;
        document.Icon = pageMeta.Icon;
        //document.Id = routeKey;
        //document.Title = routeKey switch
        //{
        //    NavigationRoutes.Home => "首页",
        //    NavigationRoutes.Users => "用户",
        //    NavigationRoutes.Roles => "角色",
        //    NavigationRoutes.Tree => "树形展示",
        //    NavigationRoutes.SimpleTree => "简单树形",
        //    NavigationRoutes.Settings => "设置",
        //    NavigationRoutes.Colors => "颜色",
        //    NavigationRoutes.MessagesDialogs => "对话框演示",
        //    NavigationRoutes.MessagesNotifications => "窗口通知演示",
        //    NavigationRoutes.MessagesToasts => "轻提示演示",
        //    NavigationRoutes.OverlayDialogs => "OverlayDialog 演示",
        //    _ => document.Title
        //};
        document.CanClose = true;
        document.CanFloat = true;
        document.CanPin = false;

        return document;

        //var type = Type.GetType(routeKey) ?? throw new InvalidOperationException($"找不到页面类型：{routeKey}");

        //return (Core.Workbench.Docking.DocumentPage)AppRuntime.Service.Resolve(type);

    }

    private Document? FindDocument(string routeKey)
    {
        return _dockFactory.FindDockable(
            Layout,
            dockable => dockable is Document document && string.Equals(document.Id, routeKey, StringComparison.OrdinalIgnoreCase))
            as Document;
    }
}
