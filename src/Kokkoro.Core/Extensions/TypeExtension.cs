using System;

namespace Kokkoro.Core.Extensions;

/// <summary>
/// Type 类型的扩展方法
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// 获取 System.Type 的程序集限定名，其中包括从中加载 System.Type 的程序集的名称(不带版本)
    /// </summary>
    public static string GetQualifiedName(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        return type.FullName + "," + type.Assembly.GetName().Name;
    }
}