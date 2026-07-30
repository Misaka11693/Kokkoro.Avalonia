using Kokkoro.ViewModels.Core;

namespace Kokkoro.ViewModels.Main.Settings;

public sealed class AboutSettingsSectionViewModel : ViewModelBase
{
    public AboutSettingsSectionViewModel()
    {
        ProductName = "Kokkoro";
        Version = "0.1.0";
        TechStack = "Avalonia + ReactiveUI + Ursa";
        Runtime = $".NET {Environment.Version}";
    }

    public string ProductName { get; }

    public string Version { get; }

    public string TechStack { get; }

    public string Runtime { get; }
}
