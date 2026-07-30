using Avalonia.Controls.Notifications;
using Kokkoro.Core.UI.Notifications;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 窗口通知演示区域 ViewModel。
/// </summary>
public partial class MessagesNotificationsSectionViewModel : ViewModelBase
{
    private readonly INotificationService _notificationService;
    private readonly IMessagesDemoContext _context;


    [Reactive]
    public partial int NotificationTimeout { get; set; } = 5;


    [Reactive]
    public partial int NotificationMaxItems { get; set; } = 3;


    [Reactive]
    public partial bool NotificationShowIcon { get; set; } = true;


    [Reactive]
    public partial bool NotificationShowClose { get; set; } = true;


    [Reactive]
    public partial MessageFeedbackStyleMode SelectedNotificationStyleMode { get; set; } = MessageFeedbackStyleMode.Light;


    [Reactive]
    public partial NotificationPosition SelectedNotificationPosition { get; set; } = NotificationPosition.TopRight;


    public IReadOnlyList<NotificationPosition> NotificationPositions { get; } =
    [
        NotificationPosition.TopLeft,
        NotificationPosition.TopCenter,
        NotificationPosition.TopRight,
        NotificationPosition.BottomLeft,
        NotificationPosition.BottomCenter,
        NotificationPosition.BottomRight
    ];


    public IReadOnlyList<MessageFeedbackStyleMode> StyleModes { get; } =
    [
        MessageFeedbackStyleMode.Default,
        MessageFeedbackStyleMode.Light
    ];


    public MessagesNotificationsSectionViewModel(
        INotificationService notificationService,
        IMessagesDemoContext context)
    {
        _notificationService = notificationService;
        _context = context;
    }


    [ReactiveCommand]
    private async Task ShowInfoNotification()
    {
        await _notificationService.ShowInformationAsync(
            _context.CustomMessage,
            CreateNotificationOptions());

        _context.SetLastResult("已发送：信息通知");
    }


    [ReactiveCommand]
    private async Task ShowSuccessNotification()
    {
        await _notificationService.ShowSuccessAsync(
            _context.CustomMessage,
            CreateNotificationOptions());

        _context.SetLastResult("已发送：成功通知");
    }


    [ReactiveCommand]
    private async Task ShowWarningNotification()
    {
        await _notificationService.ShowWarningAsync(
            _context.CustomMessage,
            CreateNotificationOptions());

        _context.SetLastResult("已发送：警告通知");
    }


    [ReactiveCommand]
    private async Task ShowErrorNotification()
    {
        await _notificationService.ShowErrorAsync(
            _context.CustomMessage,
            CreateNotificationOptions());

        _context.SetLastResult("已发送：错误通知");
    }


    private NotificationOptions CreateNotificationOptions()
    {
        return new NotificationOptions
        {
            Title = MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle),

            Expiration = TimeSpan.FromSeconds(NotificationTimeout),

            ShowIcon = NotificationShowIcon,

            ShowClose = NotificationShowClose,

            Classes = MessagesViewModelHelper.CreateStyleClasses(
                SelectedNotificationStyleMode),

            MaxItems = NotificationMaxItems,

            Position = SelectedNotificationPosition
        };
    }
}