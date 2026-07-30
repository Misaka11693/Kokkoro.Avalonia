using Microsoft.Extensions.DependencyInjection;

namespace Kokkoro.Core.Services;

/// <summary>
/// 默认服务容器。
/// 继承 <see cref="ServiceCollection"/>，同时实现注册与解析能力。
/// 第一次解析服务时自动构建 <see cref="IServiceProvider"/> 并锁定集合，构建后任何修改操作均由
/// <see cref="ServiceCollection.MakeReadOnly"/> 机制拦截并抛出 <see cref="InvalidOperationException"/>。
/// </summary>
public sealed class ServiceContainer : ServiceCollection, IServiceContainer
{
    private IServiceProvider? _serviceProvider;

    #region 注册服务

    /// <inheritdoc />
    public void Register<TService>(ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
        where TService : class
    {
        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                this.AddSingleton<TService>();
                break;
            case ServiceLifeStyle.Transient:
                this.AddTransient<TService>();
                break;
            case ServiceLifeStyle.Scoped:
                this.AddScoped<TService>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <inheritdoc />
    public void Register<TService, TImplementation>(ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
        where TService : class
        where TImplementation : class, TService
    {
        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                this.AddSingleton<TService, TImplementation>();
                break;
            case ServiceLifeStyle.Transient:
                this.AddTransient<TService, TImplementation>();
                break;
            case ServiceLifeStyle.Scoped:
                this.AddScoped<TService, TImplementation>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <inheritdoc />
    public void Register<TService>(TService instance)
        where TService : class
    {
        ArgumentNullException.ThrowIfNull(instance);

        this.AddSingleton(instance);
    }

    /// <inheritdoc />
    public void Register(Type serviceType, Type implementationType, ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
    {
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(implementationType);

        switch (lifeStyle)
        {
            case ServiceLifeStyle.Singleton:
                this.AddSingleton(serviceType, implementationType);
                break;
            case ServiceLifeStyle.Transient:
                this.AddTransient(serviceType, implementationType);
                break;
            case ServiceLifeStyle.Scoped:
                this.AddScoped(serviceType, implementationType);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifeStyle));
        }
    }

    /// <inheritdoc />
    public bool IsRegistered<TService>()
    {
        return IsRegistered(typeof(TService));
    }

    /// <inheritdoc />
    public bool IsRegistered(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return this.Any(x => x.ServiceType == serviceType);
    }

    #endregion

    #region 服务解析

    /// <inheritdoc />
    public TService Resolve<TService>()
        where TService : class
    {
        return ServiceProvider.GetRequiredService<TService>();
    }

    /// <inheritdoc />
    public object Resolve(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return ServiceProvider.GetRequiredService(serviceType);
    }

    /// <inheritdoc />
    public TService? ResolveOrDefault<TService>()
        where TService : class
    {
        return ServiceProvider.GetService<TService>();
    }

    #endregion

    #region Private

    /// <summary>
    /// 获取服务提供器。
    /// 首次访问时构建容器并调用 <see cref="ServiceCollection.MakeReadOnly"/> 锁定集合，
    /// 后续任何注册操作都会由基类直接拦截。
    /// </summary>
    private IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider is null)
            {
                _serviceProvider = this.BuildServiceProvider();
                MakeReadOnly();
            }

            return _serviceProvider;
        }
    }

    #endregion
}
