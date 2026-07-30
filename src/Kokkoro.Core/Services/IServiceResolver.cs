namespace Kokkoro.Core.Services;

/// <summary>
/// 服务解析器。
/// 用于从容器中获取已注册的服务实例。
/// </summary>
public interface IServiceResolver
{
    /// <summary>
    /// 解析指定类型的服务。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>服务实例。</returns>
    TService Resolve<TService>()
        where TService : class;

    /// <summary>
    /// 解析指定类型的服务。
    /// </summary>
    /// <param name="serviceType">服务类型。</param>
    /// <returns>服务实例。</returns>
    object Resolve(Type serviceType);

    /// <summary>
    /// 尝试解析服务。
    /// 服务不存在时返回 null。
    /// </summary>
    /// <typeparam name="TService">服务类型。</typeparam>
    /// <returns>服务实例。</returns>
    TService? ResolveOrDefault<TService>()
        where TService : class;
}
