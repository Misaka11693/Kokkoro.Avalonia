using Kokkoro.Core.ViewModels;
using Kokkoro.Core.Workbench.Managers.DockLayoutManagers;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.Core.Workbench.Regions.Header;

public partial class HeaderBarViewModel : ViewModelBase
{
    private readonly IDockLayoutManager _dockLayoutManager;

    public HeaderBarViewModel(IDockLayoutManager dockLayoutManager)
    {
        _dockLayoutManager = dockLayoutManager;
    }

    [ReactiveCommand]
    private void CloseCurrentPage()
    {
        _dockLayoutManager.CloseActiveDocument();
    }

    [ReactiveCommand]
    private void CloseOtherPages()
    {
        _dockLayoutManager.CloseOtherDocuments();
    }

    [ReactiveCommand]
    private void CloseAllPages()
    {
        _dockLayoutManager.CloseAllDocuments();
    }
}