using Avalonia.Controls.Templates;
using Kokkoro.Core.Apps;
using System.Diagnostics.CodeAnalysis;

namespace Kokkoro;

/// <summary>
/// 统一负责 ViewModel 到 View 的解析，优先走 ReactiveUI 的 IViewFor 注册。
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
public sealed class ViewLocator : IDataTemplate, IViewLocator
{
    public Control? Build(object? param)
    {
        return param is null
            ? null
            : ResolveView(param.GetType(), param) as Control ?? new TextBlock { Text = "View not found: " + param.GetType().Name };
    }

    /// <summary>
    /// 仅判断能否解析，不创建 View（避免 Match + Build 重复实例化）。
    /// </summary>
    public bool Match(object? param)
    {
        return param is not null && CanResolveView(param.GetType());
    }

    IViewFor<TViewModel>? IViewLocator.ResolveView<TViewModel>(string? contract)
    {
        return ResolveView(typeof(TViewModel), null, contract) as IViewFor<TViewModel>;
    }

    IViewFor? IViewLocator.ResolveView(object? viewModel, string? contract)
    {
        return viewModel is null ? null : ResolveView(viewModel.GetType(), viewModel, contract);
    }

    private IViewFor? ResolveView(Type viewModelType, object? viewModel, string? contract = null)
    {
        if (!CanResolveView(viewModelType))
        {
            return null;
        }

        var view = ResolveRegisteredView(viewModelType, contract);

        if (viewModel is not null && view is not null)
        {
            view.ViewModel = viewModel;
        }

        return view;
    }

    private static bool CanResolveView(Type viewModelType)
    {
        if (!IsViewModel(viewModelType))
            return false;

        var serviceType = typeof(IViewFor<>).MakeGenericType(viewModelType);

        return AppRuntime.Service.IsRegistered(serviceType);
    }

    //private static bool CanResolveView(Type viewModelType)
    //    => TryResolveViewType(viewModelType) is not null;

    private static Type? TryResolveViewType(Type viewModelType)
    {
        if (viewModelType.FullName?.Contains(".ViewModels.", StringComparison.Ordinal) != true)
        {
            return null;
        }

        var viewTypeName = viewModelType.FullName
            .Replace(".ViewModels.", ".Views.", StringComparison.Ordinal)
            .Replace("ViewModel", "View", StringComparison.Ordinal);

        if (string.IsNullOrWhiteSpace(viewTypeName))
        {
            return null;
        }

        var viewType = viewModelType.Assembly.GetType(viewTypeName);
        return viewType is not null
            && typeof(Control).IsAssignableFrom(viewType)
            && typeof(IViewFor).IsAssignableFrom(viewType)
            ? viewType
            : null;
    }

    private IViewFor? ResolveRegisteredView(Type viewModelType, string? contract)
    {
        var serviceType = typeof(IViewFor<>).MakeGenericType(viewModelType);

        if (!string.IsNullOrWhiteSpace(contract))
        {
            return null;
        }

        return AppRuntime.Service.IsRegistered(serviceType)
            ? AppRuntime.Service.Resolve(serviceType) as IViewFor
            : null;
    }

    private static bool IsViewModel(Type type)
    {
        return type.IsClass
            && !type.IsAbstract
            && !typeof(Control).IsAssignableFrom(type);
    }

    public IViewFor<TViewModel>? ResolveView<TViewModel>() where TViewModel : class
    {
        throw new NotImplementedException();
    }

    public IViewFor? ResolveView(object? instance)
    {
        throw new NotImplementedException();
    }
}
