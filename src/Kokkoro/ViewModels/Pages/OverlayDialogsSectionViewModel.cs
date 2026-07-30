using Kokkoro.Core.UI.OverlayDialogs;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;
using Ursa.Controls;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// OverlayDialog 演示区域 ViewModel。
/// </summary>
public partial class OverlayDialogsSectionViewModel : ViewModelBase
{
    private readonly IOverlayDialogService _overlayDialogService;
    private readonly IMessagesDemoContext _context;

    public OverlayDialogsSectionViewModel(
        IOverlayDialogService overlayDialogService,
        IMessagesDemoContext context)
    {
        _overlayDialogService = overlayDialogService;
        _context = context;
    }

    [Reactive]
    public partial DialogMode SelectedDialogMode { get; set; } = DialogMode.Info;

    [Reactive]
    public partial DialogButton SelectedDialogButton { get; set; } = DialogButton.OKCancel;

    [Reactive]
    public partial HorizontalPosition SelectedHorizontalAnchor { get; set; } = HorizontalPosition.Center;

    [Reactive]
    public partial VerticalPosition SelectedVerticalAnchor { get; set; } = VerticalPosition.Center;

    [Reactive]
    public partial double HorizontalOffset { get; set; } = 24;

    [Reactive]
    public partial double VerticalOffset { get; set; } = 24;

    [Reactive]
    public partial bool CanLightDismiss { get; set; }

    [Reactive]
    public partial bool CanDragMove { get; set; } = true;

    [Reactive]
    public partial bool CanResize { get; set; }

    [Reactive]
    public partial bool IsCloseButtonVisible { get; set; } = true;

    [Reactive]
    public partial bool FullScreen { get; set; }

    public IReadOnlyList<DialogMode> DialogModes { get; } =
    [
        DialogMode.Info,
        DialogMode.Success,
        DialogMode.Warning,
        DialogMode.Error,
        DialogMode.Question,
        DialogMode.None
    ];

    public IReadOnlyList<DialogButton> DialogButtons { get; } =
    [
        DialogButton.OK,
        DialogButton.OKCancel,
        DialogButton.YesNo,
        DialogButton.YesNoCancel
    ];

    public IReadOnlyList<HorizontalPosition> HorizontalAnchors { get; } =
    [
        HorizontalPosition.Left,
        HorizontalPosition.Center,
        HorizontalPosition.Right
    ];

    public IReadOnlyList<VerticalPosition> VerticalAnchors { get; } =
    [
        VerticalPosition.Top,
        VerticalPosition.Center,
        VerticalPosition.Bottom
    ];

    [ReactiveCommand]
    private void OpenStandardNonModal()
    {
        _overlayDialogService.ShowStandard(
            CreateStandardDemoViewModel(),
            options: CreateStandardOptions());
        _context.SetLastResult("已打开：标准 OverlayDialog（非模态）");
    }

    [ReactiveCommand]
    private async Task OpenStandardModal()
    {
        var result = await _overlayDialogService.ShowStandardAsync(
            CreateStandardDemoViewModel(),
            options: CreateStandardOptions());
        _context.SetLastResult($"标准 OverlayDialog（模态）结果：{MapDialogResult(result)}");
    }

    [ReactiveCommand]
    private void OpenCustomNonModal()
    {
        _overlayDialogService.ShowCustom(
            CreateCustomDemoViewModel(),
            options: CreateCustomOptions());
        _context.SetLastResult("已打开：自定义 OverlayDialog（非模态）");
    }

    [ReactiveCommand]
    private async Task OpenCustomModal()
    {
        var result = await _overlayDialogService.ShowCustomAsync<OverlayDialogCustomDemoViewModel, bool>(
            CreateCustomDemoViewModel(),
            options: CreateCustomOptions());
        _context.SetLastResult($"自定义 OverlayDialog（模态）结果：{MapCustomResult(result)}");
    }

    private OverlayDialogStandardDemoViewModel CreateStandardDemoViewModel()
    {
        return new OverlayDialogStandardDemoViewModel
        {
            Message = _context.CustomMessage,
            Description = "该内容区由新引入的 IOverlayDialogService 承载，用于演示标准 OverlayDialog 的展示效果。"
        };
    }

    private OverlayDialogCustomDemoViewModel CreateCustomDemoViewModel()
    {
        return new OverlayDialogCustomDemoViewModel
        {
            Message = _context.CustomMessage,
            Header = MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle) ?? "自定义 OverlayDialog",
            InputText = "这里可以继续填写额外内容，然后点击底部按钮返回结果。"
        };
    }

    private OverlayDialogOptions CreateStandardOptions()
    {
        var options = CreateCommonOptions(MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle) ?? "标准 OverlayDialog");
        options.Mode = SelectedDialogMode;
        options.Buttons = SelectedDialogButton;
        return options;
    }

    private OverlayDialogOptions CreateCustomOptions()
    {
        var options = CreateCommonOptions(MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle) ?? "自定义 OverlayDialog");
        options.Buttons = DialogButton.None;
        return options;
    }

    private OverlayDialogOptions CreateCommonOptions(string title)
    {
        return new OverlayDialogOptions
        {
            Title = title,
            FullScreen = FullScreen,
            CanLightDismiss = CanLightDismiss,
            CanDragMove = CanDragMove,
            CanResize = CanResize,
            IsCloseButtonVisible = IsCloseButtonVisible,
            HorizontalAnchor = SelectedHorizontalAnchor,
            VerticalAnchor = SelectedVerticalAnchor,
            HorizontalOffset = SelectedHorizontalAnchor == HorizontalPosition.Center ? null : HorizontalOffset,
            VerticalOffset = SelectedVerticalAnchor == VerticalPosition.Center ? null : VerticalOffset
        };
    }

    private static string MapDialogResult(DialogResult result)
    {
        return result switch
        {
            DialogResult.OK => "用户点击了「确定」",
            DialogResult.Yes => "用户点击了「是」",
            DialogResult.No => "用户点击了「否」",
            DialogResult.Cancel => "用户点击了「取消」",
            _ => "未返回结果"
        };
    }

    private static string MapCustomResult(bool? result)
    {
        return result switch
        {
            true => "用户点击了「提交并关闭」",
            false => "用户点击了「取消」",
            null => "未返回结果（可能通过右上角关闭）"
        };
    }
}
