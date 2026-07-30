using Avalonia.Controls;
using Kokkoro.ViewModels.Pages;
using ReactiveUI.Avalonia;

namespace Kokkoro.Views.Pages;

public partial class TreeNodeEditView : ReactiveUserControl<TreeNodeEditViewModel>
{
    public TreeNodeEditView()
    {
        InitializeComponent();
    }
}
