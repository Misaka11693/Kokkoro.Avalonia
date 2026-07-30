using Avalonia.Controls;
using Kokkoro.Admin.ViewModels;
using ReactiveUI.Avalonia;

namespace Kokkoro.Admin.Views;

public partial class AdminListView : ReactiveUserControl<AdminListViewModel>
{
    public AdminListView()
    {
        InitializeComponent();
    }

    private void OnItemsGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is AdminListViewModel viewModel)
        {
            viewModel.SelectedCount = ItemsGrid.SelectedItems?.Count ?? 0;
        }
    }
}
