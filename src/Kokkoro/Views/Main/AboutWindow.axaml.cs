using Kokkoro.ViewModels.Main;
using Ursa.ReactiveUIExtension;

namespace Kokkoro.Views.Main;

public partial class AboutWindow : ReactiveUrsaWindow<AboutWindowViewModel>
{
    public AboutWindow()
    {
        InitializeComponent();
        Opened += OnOpened;
        Closed += OnClosed;
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        if (ViewModel is null)
        {
            return;
        }

        ViewModel.CloseRequested += OnCloseRequested;
    }

    private void OnClosed(object? sender, EventArgs e)
    {
        if (ViewModel is not null)
        {
            ViewModel.CloseRequested -= OnCloseRequested;
        }
    }

    private void OnCloseRequested(object? sender, EventArgs e)
    {
        Close();
    }
}
