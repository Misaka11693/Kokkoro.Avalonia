namespace Kokkoro.Core.Services;

/// <summary>
/// <see cref="IServiceRegistrar"/> 扩展方法，提供显式生命周期语义的注册方式。
/// 所有方法均在服务未注册时才执行注册（幂等）。
/// </summary>
public static class ServiceRegistrarExtensions
{
    #region Singleton

    /// <summary>
    /// 注册单例服务自身。
    /// </summary>
    public static void RegisterSingleton<TService>(this IServiceRegistrar registrar)
        where TService : class
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService>(ServiceLifeStyle.Singleton);
    }

    /// <summary>
    /// 注册单例服务及其实现类型。
    /// </summary>
    public static void RegisterSingleton<TService, TImplementation>(this IServiceRegistrar registrar)
        where TService : class
        where TImplementation : class, TService
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService, TImplementation>(ServiceLifeStyle.Singleton);
    }

    /// <summary>
    /// 注册单例服务实例。
    /// </summary>
    public static void RegisterSingleton<TService>(this IServiceRegistrar registrar, TService instance)
        where TService : class
    {
        ArgumentNullException.ThrowIfNull(instance);

        if (!registrar.IsRegistered<TService>())
            registrar.Register(instance);
    }

    /// <summary>
    /// 注册单例服务及其实现类型（非泛型）。
    /// </summary>
    public static void RegisterSingleton(this IServiceRegistrar registrar, Type serviceType, Type implementationType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        if (!registrar.IsRegistered(serviceType))
            registrar.Register(serviceType, implementationType, ServiceLifeStyle.Singleton);
    }

    #endregion

    #region Transient

    /// <summary>
    /// 注册瞬态服务自身。
    /// </summary>
    public static void RegisterTransient<TService>(this IServiceRegistrar registrar)
        where TService : class
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService>(ServiceLifeStyle.Transient);
    }

    /// <summary>
    /// 注册瞬态服务及其实现类型。
    /// </summary>
    public static void RegisterTransient<TService, TImplementation>(this IServiceRegistrar registrar)
        where TService : class
        where TImplementation : class, TService
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService, TImplementation>(ServiceLifeStyle.Transient);
    }

    /// <summary>
    /// 注册瞬态服务及其实现类型（非泛型）。
    /// </summary>
    public static void RegisterTransient(this IServiceRegistrar registrar, Type serviceType, Type implementationType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        if (!registrar.IsRegistered(serviceType))
            registrar.Register(serviceType, implementationType, ServiceLifeStyle.Transient);
    }

    #endregion

    #region Scoped

    /// <summary>
    /// 注册局部作用域服务自身。
    /// </summary>
    public static void RegisterScoped<TService>(this IServiceRegistrar registrar)
        where TService : class
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService>(ServiceLifeStyle.Scoped);
    }

    /// <summary>
    /// 注册局部作用域服务及其实现类型。
    /// </summary>
    public static void RegisterScoped<TService, TImplementation>(this IServiceRegistrar registrar)
        where TService : class
        where TImplementation : class, TService
    {
        if (!registrar.IsRegistered<TService>())
            registrar.Register<TService, TImplementation>(ServiceLifeStyle.Scoped);
    }

    /// <summary>
    /// 注册局部作用域服务及其实现类型（非泛型）。
    /// </summary>
    public static void RegisterScoped(this IServiceRegistrar registrar, Type serviceType, Type implementationType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        if (!registrar.IsRegistered(serviceType))
            registrar.Register(serviceType, implementationType, ServiceLifeStyle.Scoped);
    }

    #endregion
}
