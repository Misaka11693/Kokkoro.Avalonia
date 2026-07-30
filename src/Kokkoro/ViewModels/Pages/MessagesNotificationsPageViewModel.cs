using Kokkoro.Core.UI.Notifications;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 窗口通知演示页面。
/// </summary>
public sealed class MessagesNotificationsPageViewModel : MessagesDemoPageViewModelBase
{
    public MessagesNotificationsPageViewModel(INotificationService notificationService)
    {
        Notifications = new MessagesNotificationsSectionViewModel(notificationService, DemoContext);
    }

    public MessagesNotificationsSectionViewModel Notifications { get; }
}
