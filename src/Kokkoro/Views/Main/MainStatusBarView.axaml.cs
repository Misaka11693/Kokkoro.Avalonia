using Kokkoro.ViewModels.Main;
using ReactiveUI.Avalonia;

namespace Kokkoro.Views.Main;

public partial class MainStatusBarView : ReactiveUserControl<MainStatusBarViewModel>
{
    public MainStatusBarView()
    {
        InitializeComponent();
    }
}
