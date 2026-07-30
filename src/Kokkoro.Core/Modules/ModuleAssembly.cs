using System.Diagnostics;
using System.Reflection;

namespace Kokkoro.Core.Modules;

/// <summary>
/// 模块程序集对象，包含程序集和模块实例。
/// </summary>
[DebuggerDisplay("{Assembly.FullName}")]
public class ModuleAssembly
{
    /// <summary>
    /// 程序集当中的模块对象。 如果模块中没有定义，则此属性为 null。
    /// </summary>
    public required IModule Instance { get; set; }

    /// <summary>
    /// 程序集本身
    /// </summary>
    public required Assembly Assembly { get; set; }

    /// <summary>
    /// 模块的启动级别
    /// </summary>
    public int SetupIndex { get; internal set; }
}
