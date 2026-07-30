using Avalonia;
using Avalonia.Controls;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// 应用资源帮助类。
/// </summary>
public static class ResourceHelper
{
    /// <summary>
    /// 根据资源键获取资源对象。
    /// </summary>
    /// <typeparam name="T">资源类型。</typeparam>
    /// <param name="key">资源键。</param>
    /// <returns>资源对象，不存在时返回默认值。</returns>
    public static T? Get<T>(object key)
    {
        if (Application.Current == null)
            return default;

        if (Application.Current.TryFindResource(key, null, out var value))
            return (T?)value;

        return default;
    }
}
