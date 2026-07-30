using Kokkoro.Core.Apps;
using Kokkoro.Core.Services;
using Kokkoro.Core.Workbench;
using Kokkoro.Docking;
using Kokkoro.Services;
using Kokkoro.Services.Roles;
using Kokkoro.Services.Users;
using Kokkoro.ViewModels.Auth;
using Kokkoro.ViewModels.Main;
using Kokkoro.ViewModels.Session;
using Kokkoro.ViewModels.Startup;
using Kokkoro.Views.Auth;
using Kokkoro.Views.Main;
using Kokkoro.Views.Startup;
using System.Reflection;

namespace Kokkoro;

public static class ServiceCollectionExtensions
{
    public static void RegisterAppRuntimeServices()
    {
        var services = AppRuntime.Service;

        var assemblies = GetApplicationAssemblies();

        var assemblyTypes = assemblies
            .SelectMany(GetLoadableTypes)
            .ToArray();

        services.RegisterSingleton<AppDockFactory>();
        services.RegisterSingleton<Kokkoro.Core.Workbench.Docking.AppDockFactory>();
        services.RegisterSingleton<IViewLocator, ViewLocator>();
        services.RegisterSingleton<IDockLayoutManager, DockLayoutManager>();
        services.RegisterSingleton<Kokkoro.Core.Workbench.Managers.DockLayoutManagers.IDockLayoutManager, Kokkoro.Core.Workbench.Managers.DockLayoutManagers.DockLayoutManager>();
        services.RegisterSingleton<IThemeService, ThemeService>();
        services.RegisterSingleton<CurrentUserViewModel>();
        services.RegisterSingleton<IRoleService, RoleService>();
        services.RegisterSingleton<IUserService, UserService>();

        services.RegisterTransient<StartupSplashWindowViewModel>();
        services.RegisterTransient<StartupSplashWindow>();

        services.RegisterTransient<AboutWindowViewModel>();
        services.RegisterTransient<IViewFor<AboutWindowViewModel>, AboutWindow>();

        services.RegisterTransient<AuthWindowViewModel>();
        services.RegisterTransient<IViewFor<AuthWindowViewModel>, AuthWindow>();

        services.RegisterTransient<MainWindowViewModel>();
        services.RegisterTransient<IViewFor<MainWindowViewModel>, MainWindow>();

        services.RegisterTransient<KokkoroWorkbenchViewModel>();
        services.RegisterTransient<IViewFor<KokkoroWorkbenchViewModel>, KokkoroWorkbench>();

        services.RegisterTransient<SettingsWindowViewModel>();
        services.RegisterTransient<IViewFor<SettingsWindowViewModel>, SettingsWindow>();

        RegisterViews(services, assemblyTypes);
    }

    /// <summary>
    /// 获取所有已加载的 Kokkoro 程序集
    /// </summary>
    private static Assembly[] GetApplicationAssemblies()
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a =>
                !a.IsDynamic &&
                a.GetName().Name?.StartsWith("Kokkoro", StringComparison.Ordinal) == true)
            .ToArray();
    }

    /// <summary>
    /// 获取程序集中的所有可加载类型
    /// </summary>
    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t is not null)!;
        }
    }

    private static void RegisterViews(
        IServiceContainer services,
        IEnumerable<Type> assemblyTypes)
    {
        foreach (var viewType in assemblyTypes.Where(IsViewType))
        {
            foreach (var interfaceType in viewType.GetInterfaces().Where(IsReactiveViewInterface))
            {
                // 注册 IViewFor<T>
                services.RegisterTransient(interfaceType, viewType);

                // 注册 ViewModel
                var viewModelType = interfaceType.GenericTypeArguments[0];
                services.RegisterTransient(viewModelType, viewModelType);
            }
        }
    }

    private static bool IsViewType(Type type)
    {
        return !type.IsAbstract
            && typeof(Control).IsAssignableFrom(type)
            && (type.Name.EndsWith("View", StringComparison.Ordinal)
                || type.Name.EndsWith("Window", StringComparison.Ordinal));
    }

    private static bool IsReactiveViewInterface(Type type)
    {
        return type.IsGenericType
            && type.GetGenericTypeDefinition() == typeof(IViewFor<>);
    }
}