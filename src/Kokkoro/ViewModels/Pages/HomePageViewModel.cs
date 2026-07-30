using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Media;
using Kokkoro.Services;
using Kokkoro.ViewModels.Core;
using Kokkoro.ViewModels.Main;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 首页文档 ViewModel。
/// </summary>
public partial class HomePageViewModel : DocumentPageViewModel
{
    private readonly IDockLayoutManager _dockLayoutManager;

    public HomePageViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;

        Greeting = BuildGreeting();
        Tagline = "轻量桌面工作台 · Avalonia + Semi + Ursa + 停靠布局";
        VersionLabel = "v0.1.0";
        OpenDocumentCount = _dockLayoutManager.OpenDocuments.Count;

        QuickLinks = new ObservableCollection<HomeQuickLinkViewModel>(CreateQuickLinks());
        Highlights =
        [
            new HomeHighlightViewModel("SemiIconGridSquare", "多文档停靠布局", "标签页切换，支持浮动与关闭"),
            new HomeHighlightViewModel("SemiIconContrast", "Semi 主题", "内置多套配色，可在设置中切换"),
            new HomeHighlightViewModel("SemiIconSearch", "查询侧栏", "用户页演示可折叠查询面板"),
        ];
    }

    public string Greeting { get; }

    [Reactive]
    private string _headline = "Kokkoro";

    [Reactive]
    private string _summary = "从这里开始探索示例模块，或从左侧导航打开任意文档。";

    public string Tagline { get; }

    public string VersionLabel { get; }

    public int OpenDocumentCount { get; }

    public ObservableCollection<HomeQuickLinkViewModel> QuickLinks { get; }

    public IReadOnlyList<HomeHighlightViewModel> Highlights { get; }

    [ReactiveCommand]
    private void OpenPage(string routeKey)
    {
        //_dockLayoutManager.OpenOrActivate(routeKey);
    }

    private IEnumerable<HomeQuickLinkViewModel> CreateQuickLinks()
    {
        yield return CreateQuickLink(
            NavigationRoutes.Users,
            "用户管理",
            "增删改查、分页与查询侧栏示例",
            "SemiIconUserGroup",
            "SemiBlue0",
            "SemiBlue5");

        yield return CreateQuickLink(
            NavigationRoutes.Roles,
            "角色管理",
            "顶部折叠查询面板与列表上下布局",
            "SemiIconUserGroup",
            "SemiViolet0",
            "SemiViolet5");

        yield return CreateQuickLink(
            NavigationRoutes.Colors,
            "常用颜色",
            "Semi Design 色板一览",
            "SemiIconContrast",
            "SemiGreen0",
            "SemiGreen5");

        yield return CreateQuickLink(
            NavigationRoutes.Settings,
            "系统设置",
            "外观主题与通用偏好配置",
            "SemiIconSetting",
            "SemiBlue0",
            "SemiBlue5");
    }

    private HomeQuickLinkViewModel CreateQuickLink(
        string routeKey,
        string title,
        string description,
        string iconKey,
        string iconBackgroundKey,
        string iconForegroundKey)
    {
        return new HomeQuickLinkViewModel
        {
            RouteKey = routeKey,
            Title = title,
            Description = description,
            Icon = MenuItemUtilities.GetIcon(iconKey),
            IconBackground = ResolveBrush(iconBackgroundKey),
            IconForeground = ResolveBrush(iconForegroundKey),
            OpenCommand = ReactiveCommand.Create(() => OpenPage(routeKey)),
        };
    }

    private static IBrush? ResolveBrush(string resourceKey)
    {
        return Application.Current?.TryGetResource(resourceKey, null, out var resource) == true
            ? resource as IBrush
            : null;
    }

    private static string BuildGreeting()
    {
        var hour = DateTime.Now.Hour;
        return hour switch
        {
            >= 5 and < 12 => "早上好",
            >= 12 and < 18 => "下午好",
            _ => "晚上好",
        };
    }
}

public sealed class HomeHighlightViewModel
{
    public HomeHighlightViewModel(string iconResourceKey, string title, string description)
    {
        Icon = MenuItemUtilities.GetIcon(iconResourceKey);
        Title = title;
        Description = description;
    }

    public Geometry? Icon { get; }

    public string Title { get; }

    public string Description { get; }
}
