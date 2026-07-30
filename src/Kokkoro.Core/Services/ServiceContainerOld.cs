using Microsoft.Extensions.DependencyInjection;

namespace Kokkoro.Core.Services;

/// <summary>
/// 默认服务容器
/// </summary>
public class ServiceContainerOld : IServiceContainer
{
    /// <summary>
    /// 服务集合
    /// </summary>
    private readonly IServiceCollection _services;

    /// <summary>
    /// 服务提供器
    /// </summary>
    private IServiceProvider? _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ServiceContainerOld()
    {
        _services = new ServiceCollection();
    }

    #region 注册服务

    /// <summary>
    /// 注册服务自身
    /// </summary>
    /// <typeparam name="TService">服务类型</typeparam>
    /// <param name="lifeStyle">服务生命周期</param>
    public void Register<TService>(ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton) where TService : class
    {
        EnsureNotBuilt();

        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                _services.AddSingleton<TService>();
                break;

            case ServiceLifeStyle.Transient:
                _services.AddTransient<TService>();
                break;

            case ServiceLifeStyle.Scoped:
                _services.AddScoped<TService>();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <summary>
    /// 注册服务及其实现类型
    /// </summary>
    /// <typeparam name="TService">服务类型</typeparam>
    /// <typeparam name="TImplementation">实现类型</typeparam>
    /// <param name="lifeStyle">服务生命周期</param>
    public void Register<TService, TImplementation>(
        ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
        where TService : class
        where TImplementation : class, TService
    {
        EnsureNotBuilt();

        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                _services.AddSingleton<TService, TImplementation>();
                break;

            case ServiceLifeStyle.Transient:
                _services.AddTransient<TService, TImplementation>();
                break;

            case ServiceLifeStyle.Scoped:
                _services.AddScoped<TService, TImplementation>();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <summary>
    /// 注册服务实例
    /// </summary>
    /// <typeparam name="TService">服务类型</typeparam>
    /// <param name="instance">服务实例</param>
    public void Register<TService>(TService instance)
        where TService : class
    {
        ArgumentNullException.ThrowIfNull(instance);

        EnsureNotBuilt();

        _services.AddSingleton(instance);
    }

    /// <summary>
    /// 注册服务及其实现类型
    /// </summary>
    /// <param name="serviceType">服务类型</param>
    /// <param name="implementationType">实现类型</param>
    /// <param name="lifeStyle">服务生命周期</param>
    public void Register(
        Type serviceType,
        Type implementationType,
        ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        EnsureNotBuilt();

        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                _services.AddSingleton(serviceType, implementationType);
                break;

            case ServiceLifeStyle.Transient:
                _services.AddTransient(serviceType, implementationType);
                break;

            case ServiceLifeStyle.Scoped:
                _services.AddScoped(serviceType, implementationType);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <summary>
    /// 判断服务是否已注册
    /// </summary>
    /// <typeparam name="TService">服务类型</typeparam>
    /// <returns>是否已注册</returns>
    public bool IsRegistered<TService>()
    {
        return IsRegistered(typeof(TService));
    }

    /// <summary>
    /// 判断服务是否已注册
    /// </summary>
    /// <param name="serviceType">服务类型</param>
    /// <returns>是否已注册</returns>
    public bool IsRegistered(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return _services.Any(x => x.ServiceType == serviceType);
    }

    #endregion

    #region 服务解析

    /// <summary>
    /// 解析服务。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>服务实例。</returns>
    public TService Resolve<TService>()
        where TService : class
    {
        return ServiceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    /// 解析服务。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <returns>服务实例。</returns>
    public object Resolve(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return ServiceProvider.GetRequiredService(serviceType);
    }

    /// <summary>
    /// 获取服务。
    /// 服务不存在时返回 null。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>服务实例。</returns>
    public TService? ResolveOrDefault<TService>()
        where TService : class
    {
        return ServiceProvider.GetService<TService>();
    }

    /// <summary>
    /// 获取所有服务实现。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>服务集合。</returns>
    public IEnumerable<TService> ResolveAll<TService>()
        where TService : class
    {
        return ServiceProvider.GetServices<TService>();
    }

    #endregion

    #region Private

    /// <summary>
    /// 获取服务提供器。
    /// 第一次解析服务时自动构建容器。
    /// </summary>
    private IServiceProvider ServiceProvider
    {
        get
        {
            _serviceProvider ??= _services.BuildServiceProvider();

            return _serviceProvider;
        }
    }

    /// <summary>
    /// 确保容器尚未构建。
    /// </summary>
    private void EnsureNotBuilt()
    {
        if (_serviceProvider != null)
        {
            throw new InvalidOperationException(
                "The service container has already been built and can no longer register services.");
        }
    }

    #endregion
}