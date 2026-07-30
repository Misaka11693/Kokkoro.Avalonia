using Avalonia.Controls;
using Kokkoro.Core.UI.Messages;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 对话框演示区域 ViewModel。
/// </summary>
public partial class MessagesDialogsSectionViewModel : ViewModelBase
{
    private readonly IMessageService _messageService;
    private readonly IMessagesDemoContext _context;

    public MessagesDialogsSectionViewModel(
        IMessageService messageService,
        IMessagesDemoContext context)
    {
        _messageService = messageService;
        _context = context;
    }

    [ReactiveCommand]
    private async Task ShowInfo(Window? owner)
    {
        await _messageService.ShowInformationAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已关闭：信息消息（模态）");
    }

    [ReactiveCommand]
    private async Task ShowSuccess(Window? owner)
    {
        await _messageService.ShowSuccessAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已关闭：成功消息（模态）");
    }

    [ReactiveCommand]
    private async Task ShowWarning(Window? owner)
    {
        await _messageService.ShowWarningAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已关闭：警告消息（模态）");
    }

    [ReactiveCommand]
    private async Task ShowError(Window? owner)
    {
        await _messageService.ShowErrorAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已关闭：错误消息（模态）");
    }

    [ReactiveCommand]
    private async Task ShowException(Window? owner)
    {
        await _messageService.ShowExceptionAsync(MessagesViewModelHelper.CreateDemoException());
        _context.SetLastResult("已关闭：异常消息（模态）");
    }

    [ReactiveCommand]
    private async Task AskQuestion(Window? owner)
    {
        var result = await _messageService.AskQuestionAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult(result ? "询问结果（模态）：用户点击了「是」" : "询问结果（模态）：用户点击了「否」");
    }

    [ReactiveCommand]
    private async Task ShowOverlayInfo()
    {
        await _messageService.ShowOverlayInfoAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已显示：信息消息（覆盖层）");
    }

    [ReactiveCommand]
    private async Task ShowOverlaySuccess()
    {
        await _messageService.ShowOverlaySuccessAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已显示：成功消息（覆盖层）");
    }

    [ReactiveCommand]
    private async Task ShowOverlayWarning()
    {
        await _messageService.ShowOverlayWarningAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已显示：警告消息（覆盖层）");
    }

    [ReactiveCommand]
    private async Task ShowOverlayError()
    {
        await _messageService.ShowOverlayErrorAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult("已显示：错误消息（覆盖层）");
    }

    [ReactiveCommand]
    private async Task ShowOverlayException()
    {
        await _messageService.ShowOverlayExceptionAsync(MessagesViewModelHelper.CreateDemoException());
        _context.SetLastResult("已显示：异常消息（覆盖层）");
    }

    [ReactiveCommand]
    private async Task AskOverlayQuestion()
    {
        var result = await _messageService.AskOverlayQuestionAsync(
            _context.CustomMessage,
            MessagesViewModelHelper.NullIfEmpty(_context.CustomTitle));
        _context.SetLastResult(result ? "询问结果（覆盖层）：用户点击了「是」" : "询问结果（覆盖层）：用户点击了「否」");
    }
}
