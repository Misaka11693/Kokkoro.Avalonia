using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Kokkoro.Core.Workbench.Docking;
using Kokkoro.Core.Workbench.Models;

namespace Kokkoro.Core.Workbench.Managers.DockLayoutManagers;

public sealed class DockLayoutManager : IDockLayoutManager
{
    private readonly AppDockFactory _dockFactory;

    public DockLayoutManager(AppDockFactory dockFactory)
    {
        _dockFactory = dockFactory;

        NavigationItems =
        [
            // new MenuItemViewModel(NavigationRoutes.Home, "Home"),
            // new MenuItemViewModel(NavigationRoutes.Users, "Users"),
            // new MenuItemViewModel(NavigationRoutes.Roles, "角色"),
            // new MenuItemViewModel(NavigationRoutes.Settings, "Settings"),
            // new MenuItemViewModel(NavigationRoutes.Colors, "Colors"),
        ];
    }

    public IReadOnlyList<MenuItemViewModel> NavigationItems { get; }

    private IDocumentDock DocumentDock => _dockFactory.GetDocumentDock();

    public IRootDock Layout => _dockFactory.GetLayout();

    public IDockable? ActiveDocument =>
        _dockFactory.FindDockable(Layout, dockable => dockable is Document document && document.IsActive);

    public IReadOnlyList<Document> OpenDocuments =>
        _dockFactory.FindAllDocument(Layout, dockable => dockable is Document).OfType<Document>().ToArray();

    public bool HasOpenDocuments => OpenDocuments.Count > 0;

    public void OpenOrActivate(string routeKey)
    {
        var document = FindDocument(routeKey);
        if (document is null)
        {
            document = CreateDocument(routeKey);
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
        // OpenOrActivate(NavigationRoutes.Home);
    }

    public void ResetToDefault()
    {
        CloseAllHostWindows();

        _dockFactory.CreateLayout();
        InitializeDocuments();
        _dockFactory.InitLayout(Layout);
        ShowHome();
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
        Document[] documents =
        [
            // CreateDocument(NavigationRoutes.Home),
            // CreateDocument(NavigationRoutes.Users),
            // CreateDocument(NavigationRoutes.Roles),
            // CreateDocument(NavigationRoutes.Settings),
            // CreateDocument(NavigationRoutes.Colors),
        ];

        //DocumentDock.VisibleDockables = _dockFactory.CreateList(documents.Cast<IDockable>().ToArray());
        //DocumentDock.DefaultDockable = documents[0];
        //DocumentDock.ActiveDockable = documents[0];
    }

    private Document CreateDocument(string routeKey)
    {
        Document document = routeKey switch
        {
            // NavigationRoutes.Home => AppRuntime.Service.Resolve<HomePageViewModel>(),
            // NavigationRoutes.Users => AppRuntime.Service.Resolve<UsersPageViewModel>(),
            // NavigationRoutes.Roles => AppRuntime.Service.Resolve<RolePageViewModel>(),
            // NavigationRoutes.Settings => AppRuntime.Service.Resolve<SettingsPageViewModel>(),
            // NavigationRoutes.Colors => AppRuntime.Service.Resolve<ColorsPageViewModel>(),
            _ => throw new InvalidOperationException($"Unknown route key: {routeKey}")
        };

        document.Id = routeKey;
        document.Title = routeKey switch
        {
            // NavigationRoutes.Home => "Home",
            // NavigationRoutes.Users => "Users",
            // NavigationRoutes.Roles => "角色",
            // NavigationRoutes.Settings => "Settings",
            // NavigationRoutes.Colors => "Colors",
            _ => document.Title
        };
        document.CanClose = true;
        document.CanFloat = true;
        document.CanPin = true;

        return document;
    }

    private Document? FindDocument(string routeKey)
    {
        return _dockFactory.FindDockable(
                Layout,
                dockable => dockable is Document document &&
                            string.Equals(document.Id, routeKey, StringComparison.OrdinalIgnoreCase))
            as Document;
    }
}