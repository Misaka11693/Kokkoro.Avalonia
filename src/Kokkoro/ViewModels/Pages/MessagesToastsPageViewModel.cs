using Kokkoro.Core.UI.Toasts;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 轻提示演示页面。
/// </summary>
public sealed class MessagesToastsPageViewModel : MessagesDemoPageViewModelBase
{
    public MessagesToastsPageViewModel(IToastService toastService)
    {
        Toasts = new MessagesToastsSectionViewModel(toastService, DemoContext);
    }

    public MessagesToastsSectionViewModel Toasts { get; }
}
