using Kokkoro.Core.UI.Messages;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 对话框演示页面。
/// </summary>
public sealed class MessagesDialogsPageViewModel : MessagesDemoPageViewModelBase
{
    public MessagesDialogsPageViewModel(IMessageService messageService)
    {
        Dialogs = new MessagesDialogsSectionViewModel(messageService, DemoContext);
    }

    public MessagesDialogsSectionViewModel Dialogs { get; }
}
