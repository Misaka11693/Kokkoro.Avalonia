using Dock.Model.Controls;
using Kokkoro.Services;
using Kokkoro.ViewModels.Core;

namespace Kokkoro.ViewModels.Main;

public partial class MainPageViewModel : ViewModelBase
{
    private readonly IDockLayoutManager _dockLayoutManager;

    public MainPageViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;
    }

    public IRootDock Layout => _dockLayoutManager.Layout;
}
