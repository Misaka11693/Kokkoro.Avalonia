using Avalonia.Controls;
using Kokkoro.Core.Apps;
using ReactiveUI;

namespace Kokkoro.Core.UI.Dialogs;

public class KokkoroDialogService : IKokkoroDialogService
{
    public Task<int> ShowKokkoroDialogAsync(
        Control view,
        object? viewModel = null,
        Window? owner = null,
        Action<IDialogOptions>? configureOptions = null)
    {
        return DialogHelper.ShowDialogAsync(
            view,
            viewModel,
            owner,
            configureOptions);
    }

    public Task<int> ShowKokkoroDialogAsync<TViewModel>(
        TViewModel? viewModel = null,
        Window? owner = null,
        Action<IDialogOptions>? configureOptions = null)
        where TViewModel : class
    {
        var vm = viewModel ?? AppRuntime.Service.Resolve<TViewModel>();

        var view = AppRuntime.Service.Resolve<IViewFor<TViewModel>>() as Control;

        if (view is null)
        {
            throw new InvalidOperationException(
                $"无法将 IViewFor<{typeof(TViewModel).Name}> 转换为 Control。");
        }

        return DialogHelper.ShowDialogAsync(
            view,
            vm,
            owner,
            configureOptions);
    }
}