namespace Kokkoro.Core.Apps;

/// <summary>
/// 应用程序生命周期定义。
/// </summary>
public interface IApp
{
    /// <summary>
    /// 模块操作阶段。
    /// 所有模块初始化完成后触发，可对模块进行统一配置。
    /// </summary>
    event EventHandler ModuleOperations;

    /// <summary>
    /// 应用启动完成。
    /// </summary>
    event EventHandler StartupCompleted;

    /// <summary>
    /// 应用退出。
    /// </summary>
    event EventHandler Exiting;
}
