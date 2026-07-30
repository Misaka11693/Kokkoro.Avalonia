using Dock.Model.ReactiveUI.Controls;
using Kokkoro.Core.Workbench.Docking;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 文档页基类。缩放随 Document 实例保留；关闭后重新打开为新实例。
/// </summary>
public abstract partial class DocumentPageViewModel : DocumentPage
{
    /// <summary>本页首次打开时的缩放（1.0 = 100%）。子类可 override 为其它默认值。</summary>
    protected virtual double InitialZoom => 1.0;

    protected DocumentPageViewModel()
    {
        Zoom = InitialZoom;
    }

    [Reactive]
    private double _zoom;
}
