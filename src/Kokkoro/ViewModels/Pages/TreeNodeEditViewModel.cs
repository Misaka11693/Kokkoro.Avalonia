using Irihi.Avalonia.Shared.Contracts;
using Kokkoro.Models;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 节点新增 / 编辑 OverlayDialog ViewModel。
/// </summary>
public partial class TreeNodeEditViewModel : ViewModelBase, IDialogContext
{
    [Reactive] public partial string NodeName { get; set; } = string.Empty;
    [Reactive] public partial string NodeType { get; set; } = string.Empty;
    [Reactive] public partial string? Description { get; set; }
    [Reactive] public partial int Order { get; set; } = 1;
    [Reactive] public partial bool IsEnabled { get; set; } = true;

    /// <summary>编辑模式标题（由外部设置）。</summary>
    public string DialogTitle { get; init; } = "节点信息";

    /// <summary>父节点名称提示（新增时显示）。</summary>
    public string? ParentNodeHint { get; init; }

    public event EventHandler<object?>? RequestClose;

    public void Close()
    {
        RequestClose?.Invoke(this, null);
    }

    [ReactiveCommand]
    private void Confirm()
    {
        if (string.IsNullOrWhiteSpace(NodeName))
            return;

        RequestClose?.Invoke(this, BuildResult());
    }

    [ReactiveCommand]
    private void Cancel() => RequestClose?.Invoke(this, null);

    /// <summary>从现有节点初始化（编辑模式）。</summary>
    public void LoadFrom(TreeNode node)
    {
        NodeName    = node.Name;
        NodeType    = node.Type;
        Description = node.Description;
        Order       = node.Order;
        IsEnabled   = node.IsEnabled;
    }

    /// <summary>构造编辑结果节点。</summary>
    public TreeNode BuildResult() => new()
    {
        Name        = NodeName.Trim(),
        Type        = NodeType.Trim(),
        Description = Description?.Trim(),
        Order       = Order,
        IsEnabled   = IsEnabled
    };
}
