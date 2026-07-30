using Avalonia.Interactivity;
using Kokkoro.Core.Extensions;
using Kokkoro.ViewModels.Pages;

namespace Kokkoro.Views.Pages;

public partial class SimpleTreePageView : DocumentPageView<SimpleTreePageViewModel>
{
    public SimpleTreePageView()
    {
        InitializeComponent();
    }

    private void OnExpandAllClick(object? sender, RoutedEventArgs e)
        => NodeTreeView.ExpandAll();

    private void OnCollapseAllClick(object? sender, RoutedEventArgs e)
        => NodeTreeView.CollapseAll();
}
