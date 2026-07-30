using Avalonia.Controls;
using Kokkoro.Core.UI.Toasts;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 轻提示演示区域 ViewModel。
/// </summary>
public partial class MessagesToastsSectionViewModel : ViewModelBase
{
    private readonly IToastService _toastService;
    private readonly IMessagesDemoContext _context;

    [Reactive]
    public partial int ToastTimeout { get; set; } = 3;

    [Reactive]
    public partial int ToastMaxItems { get; set; } = 3;

    [Reactive]
    public partial bool ToastShowIcon { get; set; } = true;

    [Reactive]
    public partial bool ToastShowClose { get; set; } = true;

    [Reactive]
    public partial MessageFeedbackStyleMode SelectedToastStyleMode { get; set; } = MessageFeedbackStyleMode.Light;

    public IReadOnlyList<MessageFeedbackStyleMode> StyleModes { get; } =
    [
        MessageFeedbackStyleMode.Default,
        MessageFeedbackStyleMode.Light
    ];

    public MessagesToastsSectionViewModel(
        IToastService toastService,
        IMessagesDemoContext context)
    {
        _toastService = toastService;
        _context = context;
    }

    [ReactiveCommand]
    private async Task ShowInfoToast(Window? owner)
    {
        await _toastService.ShowInformationAsync(_context.CustomMessage, CreateToastOptions(), owner);
        _context.SetLastResult("已显示：信息提示");
    }

    [ReactiveCommand]
    private async Task ShowSuccessToast(Window? owner)
    {
        await _toastService.ShowSuccessAsync(_context.CustomMessage, CreateToastOptions(), owner);
        _context.SetLastResult("已显示：成功提示");
    }

    [ReactiveCommand]
    private async Task ShowWarningToast(Window? owner)
    {
        await _toastService.ShowWarningAsync(_context.CustomMessage, CreateToastOptions(), owner);
        _context.SetLastResult("已显示：警告提示");
    }

    [ReactiveCommand]
    private async Task ShowErrorToast(Window? owner)
    {
        await _toastService.ShowErrorAsync(_context.CustomMessage, CreateToastOptions(), owner);
        _context.SetLastResult("已显示：错误提示");
    }

    [ReactiveCommand]
    private async Task CloseAllToasts(Window? owner)
    {
        await _toastService.CloseAllAsync(owner);
        _context.SetLastResult("已关闭：当前窗口上的全部轻提示");
    }

    private ToastOptions CreateToastOptions()
    {
        return new ToastOptions
        {
            Expiration = TimeSpan.FromSeconds(ToastTimeout),
            ShowIcon = ToastShowIcon,
            ShowClose = ToastShowClose,
            Classes = MessagesViewModelHelper.CreateStyleClasses(SelectedToastStyleMode),
            MaxItems = ToastMaxItems
        };
    }
}
