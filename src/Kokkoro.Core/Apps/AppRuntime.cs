using Kokkoro.Core.Modules;
using Kokkoro.Core.Services;
using Kokkoro.Core.Threading;
using Kokkoro.Core.UI.Dialogs;
using Kokkoro.Core.UI.Messages;
using Kokkoro.Core.UI.Notifications;
using Kokkoro.Core.UI.OverlayDialogs;
using Kokkoro.Core.UI.Toasts;

namespace Kokkoro.Core.Apps;

/// <summary>
/// App运行时环境
/// </summary>
public static class AppRuntime
{
    /// <summary>
    /// 服务容器
    /// </summary>
    public static ServiceContainer Service { get; } = new ServiceContainer();

    /// <summary>
    /// UI 主线程
    /// </summary>
    public static IMessageLoop MainThread => Service.Resolve<IMessageLoop>();

    /// <summary>
    /// 弹窗服务
    /// </summary>
    public static IKokkoroDialogService DialogService => Service.Resolve<IKokkoroDialogService>();

    /// <summary>
    /// 消息服务
    /// </summary>
    public static IMessageService MessageService => Service.Resolve<IMessageService>();

    /// <summary>
    /// 覆盖对话框服务
    /// </summary>
    public static IOverlayDialogService OverlayDialogService => Service.Resolve<IOverlayDialogService>();

    /// <summary>
    /// Toast 服务
    /// </summary>
    public static IToastService ToastService => Service.Resolve<IToastService>();

    /// <summary>
    /// 消息提醒
    /// </summary>
    public static INotificationService NotificationService => Service.Resolve<INotificationService>();

    /// <summary>
    /// 当前环境被初始化的所有模块
    /// </summary>
    public static IEnumerable<ModuleAssembly> GetAllModules() => ModuleLoader.ModuleAssemblys;
}
