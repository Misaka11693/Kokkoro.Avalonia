using Kokkoro.Services;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Main;

public sealed partial class MainTopBarViewModel
    (IDockLayoutManager dockLayoutManager) 
    : ViewModelBase
{
    [ReactiveCommand]
    private void CloseCurrentPage()
    {
        dockLayoutManager.CloseActiveDocument();
    }

    [ReactiveCommand]
    private void CloseOtherPages()
    {
        dockLayoutManager.CloseOtherDocuments();
    }

    [ReactiveCommand]
    private void CloseAllPages()
    {
        dockLayoutManager.CloseAllDocuments();
    }
}
