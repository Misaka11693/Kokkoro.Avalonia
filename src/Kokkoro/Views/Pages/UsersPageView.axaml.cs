using Avalonia.Controls;
using Avalonia.Interactivity;
using Kokkoro.Models;
using Kokkoro.ViewModels.Pages;
using Ursa.Controls;

namespace Kokkoro.Views.Pages;

public partial class UsersPageView : DocumentPageView<UsersPageViewModel>
{
    private bool _suppressSelectionSync;

    public UsersPageView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnSelectAllCurrentPageClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not UsersPageViewModel viewModel)
        {
            return;
        }

        _suppressSelectionSync = true;
        try
        {
            UsersGrid.SelectedItems.Clear();
            foreach (var row in viewModel.Users)
            {
                UsersGrid.SelectedItems.Add(row);
            }
        }
        finally
        {
            _suppressSelectionSync = false;
            SyncSelectionToViewModel();
        }
    }

    private void OnUsersGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (_suppressSelectionSync)
        {
            return;
        }

        SyncSelectionToViewModel();
    }

    private void SyncSelectionToViewModel()
    {
        if (DataContext is not UsersPageViewModel viewModel)
        {
            return;
        }

        var selected = UsersGrid.SelectedItems;
        if (selected is null || selected.Count == 0)
        {
            viewModel.SelectedUsers = new List<object>();
            viewModel.SelectedCount = 0;
            return;
        }

        var snapshot = new List<object>(selected.Count);
        foreach (var item in selected)
        {
            snapshot.Add(item);
        }

        viewModel.SelectedUsers = snapshot;
        viewModel.SelectedCount = snapshot.Count;
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is not UsersPageViewModel viewModel)
        {
            return;
        }

        viewModel.RequestUserEditAsync = ShowUserEditDialogAsync;
        viewModel.RequestConfirmAsync = ShowConfirmAsync;
        viewModel.RequestNotifyAsync = ShowNotifyAsync;
    }

    private async Task<User?> ShowUserEditDialogAsync(UserEditRequest request)
    {
        var owner = TopLevel.GetTopLevel(this) as Window;
        var dialog = new UserEditWindow
        {
            DataContext = new UserEditViewModel(request),
        };

        if (owner is null)
        {
            return null;
        }

        return await dialog.ShowDialog<User?>(owner);
    }

    private static async Task<bool> ShowConfirmAsync(string message, string title)
    {
        var result = await OverlayMessageBox.ShowAsync(
            message,
            title,
            icon: MessageBoxIcon.Question,
            button: MessageBoxButton.OKCancel);

        return result == MessageBoxResult.OK;
    }

    private static async Task ShowNotifyAsync(string message, string title)
    {
        await OverlayMessageBox.ShowAsync(
            message,
            title,
            icon: MessageBoxIcon.Information,
            button: MessageBoxButton.OK);
    }
}
