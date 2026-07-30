using Avalonia.Interactivity;
using Kokkoro.Core.Extensions;
using Kokkoro.Admin.ViewModels;
using ReactiveUI.Avalonia;

namespace Kokkoro.Admin.Views;

public partial class AdminTreeView : ReactiveUserControl<AdminTreeViewModel>
{
    public AdminTreeView()
    {
        InitializeComponent();
    }

    private void OnExpandAllClick(object? sender, RoutedEventArgs e)
    {
        NodeTreeView.ExpandAll();
    }

    private void OnCollapseAllClick(object? sender, RoutedEventArgs e)
    {
        NodeTreeView.CollapseAll();
    }
}
