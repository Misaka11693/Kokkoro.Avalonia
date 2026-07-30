using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Kokkoro.Core.Apps;
using Kokkoro.Core.Workbench;
using Kokkoro.Services;
using Kokkoro.ViewModels.Main;
using Kokkoro.ViewModels.Startup;
using Kokkoro.Views.Main;
using Kokkoro.Views.Startup;
using ReactiveUI.Builder;
using System.Reactive;

namespace Kokkoro;

public partial class App : KokkoroApplication
{
    private TrayIcon? _mainTrayIcon;

    /// <summary>
    /// App 初始化
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    /// <summary>
    /// App 初始化完成
    /// </summary>
    public override void OnInitialized()
    {
        GlobalExceptionHandler.Init();
        RxAppBuilder.CreateReactiveUIBuilder().WithExceptionHandler(Observer.Create<Exception>(GlobalExceptionHandler.Handle)).BuildApp();
        ServiceCollectionExtensions.RegisterAppRuntimeServices();
        DataTemplates.Insert(0, (IDataTemplate)AppRuntime.Service.Resolve<IViewLocator>());
        InitializeTrayIcon();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = CreateStartupSplashWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private Window CreateMainWindow()
    {
        var viewModel = AppRuntime.Service.Resolve<MainWindowViewModel>();
        var view = AppRuntime.Service.Resolve<IViewFor<MainWindowViewModel>>();
        view.ViewModel = viewModel;

        return view as Window
            ?? throw new InvalidOperationException("The registered main window view must inherit from Window.");
    }

    private Window CreateMainWindow2()
    {
        var viewModel = AppRuntime.Service.Resolve<KokkoroWorkbenchViewModel>();
        var view = AppRuntime.Service.Resolve<IViewFor<KokkoroWorkbenchViewModel>>();
        view.ViewModel = viewModel;

        return view as Window
            ?? throw new InvalidOperationException("The registered main window view must inherit from Window.");
    }

    private Window CreateAboutWindow()
    {
        var viewModel = AppRuntime.Service.Resolve<AboutWindowViewModel>();
        var view = AppRuntime.Service.Resolve<IViewFor<AboutWindowViewModel>>();

        view.ViewModel = viewModel;

        return view as Window
            ?? throw new InvalidOperationException("The registered about window view must inherit from Window.");
    }

    private Window CreateSettingsWindow()
    {
        var viewModel = AppRuntime.Service.Resolve<SettingsWindowViewModel>();
        var view = AppRuntime.Service.Resolve<IViewFor<SettingsWindowViewModel>>();

        view.ViewModel = viewModel;

        return view as Window
            ?? throw new InvalidOperationException("The registered settings window view must inherit from Window.");
    }

    private Window CreateStartupSplashWindow()
    {
        var viewModel = AppRuntime.Service.Resolve<StartupSplashWindowViewModel>();
        var view = AppRuntime.Service.Resolve<StartupSplashWindow>();
        view.DataContext = viewModel;

        return view;
    }

    public void ShowMainWindow(Window currentWindow)
    {
        AppRuntime.Service.Resolve<IDockLayoutManager>().ResetToDefault();
        AppRuntime.Service.Resolve<Kokkoro.Core.Workbench.Managers.DockLayoutManagers.IDockLayoutManager>().ResetToDefault();

        var mainWindow = CreateMainWindow();
        //var mainWindow = CreateMainWindow2();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
        }

        mainWindow.Show();
        currentWindow.Close();
    }

    public Task ShowAboutWindow(Window ownerWindow)
    {
        var aboutWindow = CreateAboutWindow();
        return aboutWindow.ShowDialog(ownerWindow);
    }

    public Task ShowSettingsWindow(Window ownerWindow)
    {
        var settingsWindow = CreateSettingsWindow();
        return settingsWindow.ShowDialog(ownerWindow);
    }

    public void MinimizeMainWindowToTray(Window mainWindow)
    {
        _mainTrayIcon?.SetCurrentValue(TrayIcon.IsVisibleProperty, true);
        mainWindow.Hide();
    }

    public void RestoreMainWindowFromTray()
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop
            || desktop.MainWindow is not Window mainWindow)
        {
            return;
        }

        if (!mainWindow.IsVisible)
        {
            mainWindow.Show();
        }

        mainWindow.WindowState = WindowState.Normal;
        mainWindow.Activate();
        _mainTrayIcon?.SetCurrentValue(TrayIcon.IsVisibleProperty, false);
    }

    private void OnTrayShowMainWindowClick(object? sender, EventArgs e)
    {
        RestoreMainWindowFromTray();
    }

    private void OnTrayExitClick(object? sender, EventArgs e)
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            return;
        }

        if (desktop.MainWindow is MainWindow mainWindow)
        {
            _mainTrayIcon?.SetCurrentValue(TrayIcon.IsVisibleProperty, false);
            mainWindow.CloseDirectly();
            return;
        }

        desktop.Shutdown();
    }

    private void InitializeTrayIcon()
    {
        var showMainWindowItem = new NativeMenuItem
        {
            Header = "显示主窗口"
        };
        showMainWindowItem.Click += OnTrayShowMainWindowClick;

        var exitItem = new NativeMenuItem
        {
            Header = "退出应用"
        };
        exitItem.Click += OnTrayExitClick;

        _mainTrayIcon = new TrayIcon
        {
            Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://Kokkoro/Assets/avalonia-logo.ico"))),
            ToolTipText = "Kokkoro",
            IsVisible = false,
            Menu = new NativeMenu
            {
                Items =
                {
                    showMainWindowItem,
                    new NativeMenuItemSeparator(),
                    exitItem
                }
            }
        };

        SetValue(TrayIcon.IconsProperty, new TrayIcons
        {
            _mainTrayIcon
        });
    }
}
