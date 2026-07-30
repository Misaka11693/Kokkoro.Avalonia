using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Kokkoro.Core.Threading;
using Kokkoro.Core.UI.Dialogs;
using Kokkoro.Core.UI.Messages;
using Kokkoro.Core.UI.Notifications;
using Kokkoro.Core.UI.OverlayDialogs;
using Kokkoro.Core.UI.Toasts;
using Kokkoro.Threading;

namespace Kokkoro.Core.Apps;

/// <summary>
/// Kokkoro 应用程序基类。
/// 提供统一的应用程序生命周期与模块初始化流程。
/// </summary>
public abstract class KokkoroApplicationBase : Application, IApp
{
    /// <summary>
    /// 应用程序启动时间。
    /// </summary>
    public static DateTime StartupTime { get; } = DateTime.Now;

    /// <summary>
    /// 模块菜单配置事件。
    /// 所有模块初始化完成后触发。
    /// </summary>
    public event EventHandler? ModuleOperations;

    /// <summary>
    /// 应用程序启动完成事件。
    /// 模块菜单初始化完成后触发。
    /// 可用于启动 MQ、Socket、定时任务等后台服务。
    /// </summary>
    public event EventHandler? StartupCompleted;

    /// <summary>
    /// 应用程序退出事件。
    /// 应用程序关闭前触发，可用于释放资源。
    /// </summary>
    public event EventHandler? Exiting;

    /// <inheritdoc/>
    public override void Initialize()
    {
        base.Initialize();

        InitializeApp();
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += (_, _) =>
            {
                RaiseAppExiting();
            };
        }

        base.OnFrameworkInitializationCompleted();

        RaiseStartupCompleted();
    }

    /// <summary>
    /// 启动框架。
    /// </summary>
    private void InitializeApp()
    {
        InitializeFramework();

        InitializeModules();

        RaiseModuleOperations();
    }

    /// <summary>
    /// 框架初始化。
    /// 注册框架基础服务。
    /// </summary>
    private void InitializeFramework()
    {
        AppRuntime.Service.Register<IMessageLoop>(new AvaloniaMessageLoop(Current!.Dispatcher, SynchronizationContext.Current));
        AppRuntime.Service.Register<IKokkoroDialogService, KokkoroDialogService>();
        AppRuntime.Service.Register<IOverlayDialogService, UrsaOverlayDialogService>();
        AppRuntime.Service.Register<IMessageService, UrsaMessageService>();
        AppRuntime.Service.Register<INotificationService, UrsaNotificationService>();
        AppRuntime.Service.Register<IToastService, UrsaToastService>();
    }

    /// <summary>
    /// 初始化所有模块。
    /// </summary>
    protected virtual void InitializeModules()
    {
        foreach (var moduleAssembly in AppRuntime.GetAllModules())
        {
            moduleAssembly.Instance.Initialize(this);
        }
    }

    /// <summary>
    /// 触发模块菜单配置事件。
    /// </summary>
    protected virtual void RaiseModuleOperations()
    {
        ModuleOperations?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 触发应用程序启动完成事件。
    /// </summary>
    protected virtual void RaiseStartupCompleted()
    {
        StartupCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 触发应用程序退出事件。
    /// </summary>
    protected virtual void RaiseAppExiting()
    {
        Exiting?.Invoke(this, EventArgs.Empty);
    }
}
