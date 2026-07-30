namespace Kokkoro.Core.Services;

/// <summary>
/// 服务注册器。
/// 用于向容器注册服务类型、实现类型或实例对象。
/// </summary>
public interface IServiceRegistrar
{
    /// <summary>
    /// 注册服务自身。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <param name="lifeStyle">服务生命周期。</param>
    void Register<TService>(
        ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
        where TService : class;

    /// <summary>
    /// 注册服务及其实现类型。
    /// </summary>
    /// <typeparam name="TService">服务接口或基类。</typeparam>
    /// <typeparam name="TImplementation">实现类型。</typeparam>
    /// <param name="lifeStyle">服务生命周期。</param>
    void Register<TService, TImplementation>(
        ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton)
        where TService : class
        where TImplementation : class, TService;

    /// <summary>
    /// 注册服务实例。
    /// 注册后始终返回该实例。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <param name="instance">服务实例。</param>
    void Register<TService>(TService instance)
        where TService : class;

    /// <summary>
    /// 注册服务及其实现类型。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <param name="implementationType">实现类型。</param>
    /// <param name="lifeStyle">服务生命周期。</param>
    void Register(
        Type serviceType,
        Type implementationType,
        ServiceLifeStyle lifeStyle = ServiceLifeStyle.Singleton);

    /// <summary>
    /// 判断指定服务是否已注册。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>
    /// true：已注册；
    /// false：未注册。
    /// </returns>
    bool IsRegistered<TService>();

    /// <summary>
    /// 判断指定服务是否已注册。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <returns>
    /// true：已注册；
    /// false：未注册。
    /// </returns>
    bool IsRegistered(Type serviceType);
}