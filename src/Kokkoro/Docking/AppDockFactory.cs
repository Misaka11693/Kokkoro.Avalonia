using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;
using Dock.Model.ReactiveUI.Controls;
using Dock.Settings;
using Kokkoro.ViewModels.Main;

namespace Kokkoro.Docking;

/// <summary>
/// 负责创建并初始化应用使用的 Dock 布局。
/// </summary>
public sealed class AppDockFactory : Factory
{
    private IDocumentDock? _documentDock;
    private ProportionalDock? _mainLayoutDock;
    private IRootDock? _rootDock;

    public bool IsLayoutInitialized => _rootDock is not null;

    public override IRootDock CreateLayout()
    {
        var documentDock = new DocumentDock
        {
            Id = "Documents",
            Title = "Documents",
            Proportion = 1d,
            IsCollapsable = false,
            CanCreateDocument = false,
            CanCloseLastDockable = true,
            EnableWindowDrag = true,
            EmptyContent = new DocumentEmptyStateViewModel(),
            VisibleDockables = CreateList<IDockable>(),
        };

        var mainLayoutDock = new ProportionalDock
        {
            Id = "MainLayout",
            Title = "MainLayout",
            Orientation = Orientation.Horizontal,
            IsCollapsable = false,
            VisibleDockables = CreateList<IDockable>(documentDock),
            ActiveDockable = documentDock,
            DefaultDockable = documentDock,
        };

        var rootDock = CreateRootDock();
        rootDock.Id = "Root";
        rootDock.Title = "Root";
        rootDock.IsCollapsable = false;
        rootDock.VisibleDockables = CreateList<IDockable>(mainLayoutDock);
        rootDock.ActiveDockable = mainLayoutDock;
        rootDock.DefaultDockable = mainLayoutDock;
        rootDock.LeftPinnedDockables = CreateList<IDockable>();
        rootDock.RightPinnedDockables = CreateList<IDockable>();
        rootDock.TopPinnedDockables = CreateList<IDockable>();
        rootDock.BottomPinnedDockables = CreateList<IDockable>();
        rootDock.PinnedDock = null;

        _documentDock = documentDock;
        _mainLayoutDock = mainLayoutDock;
        _rootDock = rootDock;

        return rootDock;
    }

    public override void InitLayout(IDockable layout)
    {
        ContextLocator = new Dictionary<string, Func<object?>>();

        DockableLocator = new Dictionary<string, Func<IDockable?>>
        {
            ["Root"] = () => _rootDock,
            ["MainLayout"] = () => _mainLayoutDock,
            ["Documents"] = () => _documentDock
        };

        HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
        {
            [nameof(IDockWindow)] = () =>
            {
                if (DockSettings.UseManagedWindows)
                {
                    return new ManagedHostWindow();
                }

                var titleText = new TextBlock
                {
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    FontWeight = FontWeight.SemiBold,
                    Text = "浮动弹窗"
                };
                titleText.Bind(TextBlock.FontSizeProperty,new DynamicResourceExtension("SemiFontSizeRegular"));
                titleText.Bind(TextBlock.ForegroundProperty,new DynamicResourceExtension("SemiColorText0"));

                var window = new AppHostWindow
                {
                    Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://Kokkoro/Assets/avalonia-logo.ico"))),

                    LeftContent = new StackPanel
                    {
                        Orientation = Avalonia.Layout.Orientation.Horizontal,
                        Spacing = 8,
                        Margin = new Thickness(12, 0, 0, 0),
                        VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,

                        Children =
                        {
                            new Image
                            {
                                Width = 24,
                                Height = 24,
                                Source = new Bitmap(AssetLoader.Open(new Uri("avares://Kokkoro/Assets/avalonia-logo.ico")))
                            },
                            titleText
                        }
                    }
                };

                return window;
            }
        };

        base.InitLayout(layout);
    }

    /// <summary>
    /// 取得主文档区
    /// </summary>
    public IDocumentDock GetDocumentDock()
    {
        return _documentDock ?? throw new InvalidOperationException("The document dock has not been initialized.");
    }

    public IRootDock GetLayout()
    {
        return _rootDock ?? throw new InvalidOperationException("The dock layout has not been initialized.");
    }
}
