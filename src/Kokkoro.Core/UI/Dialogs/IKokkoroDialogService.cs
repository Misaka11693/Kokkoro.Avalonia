using Avalonia.Controls;

namespace Kokkoro.Core.UI.Dialogs;

public interface IKokkoroDialogService
{
    Task<int> ShowKokkoroDialogAsync(
        Control view,
        object? viewModel = null,
        Window? owner = null,
        Action<IDialogOptions>? configureOptions = null);

    Task<int> ShowKokkoroDialogAsync<TViewModel>(
        TViewModel? viewModel = null,
        Window? owner = null,
        Action<IDialogOptions>? configureOptions = null)
        where TViewModel : class;
}
