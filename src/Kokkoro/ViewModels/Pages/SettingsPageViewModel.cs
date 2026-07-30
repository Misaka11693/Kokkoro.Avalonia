using Dock.Model.ReactiveUI.Controls;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// 设置页文档 ViewModel。
/// </summary>
public partial class SettingsPageViewModel : DocumentPageViewModel
{

    [Reactive]
    private string _environmentName = "开发环境";

    [Reactive]
    private bool _enableNotifications = true;
}
