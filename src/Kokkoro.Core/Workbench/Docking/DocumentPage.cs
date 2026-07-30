using Avalonia.Media;
using Dock.Model.ReactiveUI.Controls;
using Kokkoro.Core.Workbench.View;

namespace Kokkoro.Core.Workbench.Docking;

/// <summary>
/// 文档页面
/// </summary>
public class DocumentPage : Document, ICanBeDirty
{
    public Geometry? Icon { get; set; }

    public IBrush IconForeground { get; set; } = Brushes.SkyBlue;

    /// <summary>
    /// 是否为脏
    /// </summary>
    public bool IsDirty
    {
        get
        {
            return field;
        }
        set
        {
            if (value != field)
            {
                field = value;
                OnIsDirtyChanged();
            }
        }
    }

    /// <summary>
    /// 是否脏数据变更事件
    /// </summary>
    public event EventHandler? IsDirtyChanged;

    /// <summary>
    /// 是否脏数据变更
    /// </summary>
    protected virtual void OnIsDirtyChanged()
    {
        this.IsDirtyChanged?.Invoke(this, EventArgs.Empty);
    }
}
