using Avalonia.Controls;
using Avalonia.Interactivity;
using Kokkoro.Models;
using Kokkoro.ViewModels.Pages;
using Ursa.Controls;

namespace Kokkoro.Views.Pages;

public partial class UserEditWindow : UrsaWindow
{
    public UserEditWindow()
    {
        InitializeComponent();
    }

    private void OnConfirmClick(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not UserEditViewModel viewModel)
        {
            return;
        }

        if (viewModel.TryConfirm(out var user))
        {
            Close(user);
        }
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close(null);
    }
}
