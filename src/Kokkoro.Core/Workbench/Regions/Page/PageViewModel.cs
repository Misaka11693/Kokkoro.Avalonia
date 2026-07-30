using Dock.Model.Controls;
using Kokkoro.Core.ViewModels;
using Kokkoro.Core.Workbench.Managers.DockLayoutManagers;

namespace Kokkoro.Core.Workbench.Regions.Page;

public partial class PageViewModel : ViewModelBase
{
    private readonly IDockLayoutManager _dockLayoutManager;

    public PageViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;
    }

    public IRootDock Layout => _dockLayoutManager.Layout;
}