using Avalonia.Media;
using Kokkoro.Core.Extensions;
using System.Windows.Input;

namespace Kokkoro.ViewModels.Core;

public sealed class MenuItemViewModel
{
    private string? _key;

    public MenuItemViewModel(string key, string title)
    {
        Key = key;
        Title = title;
    }

    public MenuItemViewModel(string key, string title, string iconName)
    {
        Key = key;
        Title = title;
        Icon = MenuItemUtilities.GetIcon(iconName);
    }

    /// <summary>
    /// 唯一标识符，为空时表示该菜单项作为父菜单
    /// </summary>
    public string? Key
    {
        get => _key ?? EntityType?.GetQualifiedName();
        init => _key = value;
    }

    /// <summary>
    /// 表示其显示的标题
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// 表示其显示的实体类型
    /// </summary>
    public Type? EntityType { get; set; }

    public Geometry? Icon { get; init; }

    public object? Content { get; init; }

    public IReadOnlyList<MenuItemViewModel> Children { get; init; } = [];

    public bool IsSeparator { get; init; }

    public ICommand? ActivateCommand { get; init; }

}
