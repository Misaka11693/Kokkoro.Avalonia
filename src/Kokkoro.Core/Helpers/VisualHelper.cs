using Avalonia;
using Avalonia.VisualTree;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// 视觉树帮助类。
/// </summary>
public static class VisualHelper
{
    /// <summary>
    /// 查找指定类型的父级视觉对象。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="visual">起始视觉对象。</param>
    /// <returns>找到的父级对象，不存在时返回 <c>null</c>。</returns>
    public static T? FindParent<T>(Visual visual)
        where T : Visual
    {
        var parent = visual.GetVisualParent();

        while (parent != null)
        {
            if (parent is T result)
                return result;

            parent = parent.GetVisualParent();
        }

        return null;
    }

    /// <summary>
    /// 查找指定类型的子级视觉对象。
    /// </summary>
    /// <typeparam name="T">目标类型。</typeparam>
    /// <param name="visual">起始视觉对象。</param>
    /// <returns>找到的子级对象，不存在时返回 <c>null</c>。</returns>
    public static T? FindChild<T>(Visual visual)
        where T : Visual
    {
        foreach (var child in visual.GetVisualChildren())
        {
            if (child is T result)
                return result;

            var find = FindChild<T>(child);

            if (find != null)
                return find;
        }

        return null;
    }
}
