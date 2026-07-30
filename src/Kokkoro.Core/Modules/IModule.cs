using Kokkoro.Core.Apps;

namespace Kokkoro.Core.Modules;

public interface IModule
{
    /// <summary>
    /// 模块的启动级别
    /// </summary>
    int SetupLevel { get; }

    /// <summary>
    /// 模块的初始化方法
    /// </summary>
    /// <param name="app">应用程序对象</param>
    void Initialize(IApp app);
}
