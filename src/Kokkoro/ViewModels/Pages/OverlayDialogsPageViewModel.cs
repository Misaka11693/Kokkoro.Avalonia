using Kokkoro.Core.UI.OverlayDialogs;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// OverlayDialog 演示页面。
/// </summary>
public sealed class OverlayDialogsPageViewModel : DocumentPageViewModel
{
    public OverlayDialogsPageViewModel(IOverlayDialogService overlayDialogService)
    {
        Editor = new MessagesEditorViewModel();
        Result = new MessagesResultViewModel();

        var demoContext = new MessagesDemoContext(Editor, Result);
        OverlayDialogs = new OverlayDialogsSectionViewModel(overlayDialogService, demoContext);
    }

    public MessagesEditorViewModel Editor { get; }

    public MessagesResultViewModel Result { get; }

    public OverlayDialogsSectionViewModel OverlayDialogs { get; }
}
