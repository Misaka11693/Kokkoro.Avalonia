using Dock.Avalonia.Controls;

namespace Kokkoro.Core.Workbench.Docking;

public class AppHostWindow : HostWindow
{
    protected override Type StyleKeyOverride => typeof(AppHostWindow);
}
