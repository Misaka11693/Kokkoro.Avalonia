using Avalonia.Media;
using Kokkoro.Core.Extensions;
using Kokkoro.Core.Helpers;
using System.Windows.Input;

namespace Kokkoro.ViewModels.Core;

/// <summary>
/// 菜单项的元数据模型
/// </summary>
public sealed class MenuItemMeta
{
    public MenuItemMeta()
    {
    }

    public MenuItemMeta(string title) : this()
    {
        Title = title;
    }

    public MenuItemMeta(string title, string iconName) : this(title)
    {
        Icon = MenuItemUtilities.GetIcon(iconName);
    }

    public MenuItemMeta(string title, string iconName, Type entityType) : this(title, iconName)
    {
        EntityType = entityType;
    }

    /// <summary>
    /// 唯一标识符，为空时表示该菜单项作为父菜单
    /// </summary>
    public string? Key => EntityType?.GetQualifiedName();

    /// <summary>
    /// 表示其显示的标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 表示其显示的实体类型
    /// </summary>
    public Type? EntityType { get; set; }

    public Geometry? Icon { get; init; }

    public object? Content { get; init; }

    public IReadOnlyList<MenuItemMeta> Children { get; init; } = [];

    public bool IsSeparator { get; init; }

    public ICommand? ActivateCommand { get; set; }

}
