using Kokkoro.ViewModels.Core;

namespace Kokkoro.ViewModels.Main;

public sealed class MainContentViewModel : ViewModelBase
{

    public MainContentViewModel(MainSidebarViewModel sidebar, MainPageViewModel page)
    {
        Sidebar = sidebar;
        Page = page;
    }

    public MainSidebarViewModel Sidebar { get; }

    public MainPageViewModel Page { get; }
}
