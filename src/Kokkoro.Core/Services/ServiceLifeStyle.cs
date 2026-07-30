using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Services;

public enum ServiceLifeStyle
{
    /// <summary>
    /// 单例对象
    /// </summary>
    Singleton = 0,

    /// <summary>
    /// 瞬态对象
    /// </summary>
    Transient = 1,

    /// <summary>
    /// 作用域对象
    /// </summary>
    Scoped = 2,
}
