using Avalonia.Controls;
using Avalonia.Interactivity;
using Kokkoro.ViewModels.Pages;

namespace Kokkoro.Views.Pages;

public partial class RolePageView : DocumentPageView<RolePageViewModel>
{
    private bool _suppressSelectionSync;

    public RolePageView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnRolesGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_suppressSelectionSync)
        {
            return;
        }

        SyncSelectionToViewModel();
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        SyncSelectionToViewModel();
    }

    private void SyncSelectionToViewModel()
    {
        if (DataContext is not RolePageViewModel viewModel)
        {
            return;
        }

        var selected = RolesGrid.SelectedItems;
        if (selected is null || selected.Count == 0)
        {
            viewModel.SelectedRoles = new List<object>();
            viewModel.SelectedCount = 0;
            return;
        }

        _suppressSelectionSync = true;
        try
        {
            var snapshot = new List<object>(selected.Count);
            foreach (var item in selected)
            {
                snapshot.Add(item);
            }

            viewModel.SelectedRoles = snapshot;
            viewModel.SelectedCount = snapshot.Count;
        }
        finally
        {
            _suppressSelectionSync = false;
        }
    }
}
