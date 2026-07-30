using Kokkoro.Helpers;
using Kokkoro.ViewModels.Pages;
using ReactiveUI.Avalonia;

namespace Kokkoro.Views.Pages;

public abstract class DocumentPageView<TViewModel> : ReactiveUserControl<TViewModel>
    where TViewModel : DocumentPageViewModel
{
    protected DocumentPageView()
    {
        //DataContextChanged += (_, _) => TryEnableZoom();
        //Loaded += (_, _) => TryEnableZoom();
    }

    private void TryEnableZoom()
    {
        if (DataContext is not DocumentPageViewModel document)
        {
            return;
        }

        ZoomHelper.EnableZoom(this, document.Zoom);
    }
}
