using Avalonia.Controls.Notifications;
using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Notifications;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Sample.ViewModels;

public partial class NotificationDemoViewModel : DocumentPage
{
    [Reactive]
    public partial string CustomMessage { get; set; } = "这是一条自定义消息内容。";

    [Reactive]
    public partial string CustomTitle { get; set; } = "自定义标题";

    [Reactive]
    public partial int Timeout { get; set; } = 5;

    [Reactive]
    public partial int MaxItems { get; set; } = 3;

    [Reactive]
    public partial bool ShowIcon { get; set; } = true;

    [Reactive]
    public partial bool ShowClose { get; set; } = true;

    [Reactive]
    public partial string? SelectedStyleDisplay { get; set; } = "无";

    public string[] StyleOptions { get; } = new[] { "无", "亮色" };

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


    [ReactiveCommand]
    private async Task ShowInfo()
    {
        await AppRuntime.NotificationService.ShowInformationAsync(CustomMessage, CreateNotificationOptions());
    }

    [ReactiveCommand]
    private async Task ShowSuccess()
    {
        await AppRuntime.NotificationService.ShowSuccessAsync(CustomMessage, CreateNotificationOptions());
    }

    [ReactiveCommand]
    private async Task ShowWarning()
    {
        await AppRuntime.NotificationService.ShowWarningAsync(CustomMessage, CreateNotificationOptions());
    }

    [ReactiveCommand]
    private async Task ShowError()
    {
        await AppRuntime.NotificationService.ShowWarningAsync(CustomMessage, CreateNotificationOptions());
    }

    [ReactiveCommand]
    private async Task ShowCustom()
    {
        await Task.CompletedTask;

        Console.WriteLine("Custom");
    }

    private NotificationOptions CreateNotificationOptions()
    {
        return new NotificationOptions
        {
            Title = CustomTitle,
            Expiration = TimeSpan.FromSeconds(Timeout),
            ShowIcon = ShowIcon,
            ShowClose = ShowClose,
            Classes = SelectedStyleDisplay == "亮色" ? new[] { "Light" } : null,
            MaxItems = MaxItems,
            Position = SelectedNotificationPosition
        };
    }
}
