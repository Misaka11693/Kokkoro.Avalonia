using Ursa.ReactiveUIExtension;

namespace Kokkoro.Core.Workbench;

/// <summary>
/// Kokkoro 工作台
/// </summary>
public partial class KokkoroWorkbench :  ReactiveUrsaWindow<KokkoroWorkbenchViewModel>
{
    public KokkoroWorkbench()
    {
        InitializeComponent();
    }
}