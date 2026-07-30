using Kokkoro.Core.Apps;

namespace Kokkoro.Core.Modules;

/// <summary>
/// 领域模块
/// </summary>
public abstract class DomainModule : IModule
{
    /// <summary>
    /// 模块的启动级别
    /// </summary>
    public virtual int SetupLevel => 100;

    /// <summary>
    /// 模块的初始化方法
    /// </summary>
    /// <param name="app">应用程序对象</param>
    public abstract void Initialize(IApp app);
}
