using System.Collections.ObjectModel;

namespace Kokkoro.Models;

/// <summary>
/// 树节点模型
/// </summary>
public class TreeNode
{
    /// <summary>
    /// 节点 ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 节点描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 父节点 ID
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// 子节点集合
    /// </summary>
    public ObservableCollection<TreeNode> Children { get; set; } = new();

    /// <summary>
    /// 是否展开（用于双向绑定控制展开状态）
    /// </summary>
    public bool IsExpanded { get; set; } = true;

    /// <summary>
    /// 是否有子节点
    /// </summary>
    public bool HasChildren => Children.Count > 0;

    /// <summary>
    /// 状态文本（用于显示）
    /// </summary>
    public string StatusText => IsEnabled ? "启用" : "禁用";
}
