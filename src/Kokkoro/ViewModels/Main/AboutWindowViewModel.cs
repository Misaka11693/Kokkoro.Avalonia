using System.Runtime.InteropServices;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;

namespace Kokkoro.ViewModels.Main;

public sealed partial class AboutWindowViewModel : ViewModelBase
{
    public AboutWindowViewModel()
    {
        ProductName = "Kokkoro";
        Version = "0.1.0";
        TechStack = "Avalonia + ReactiveUI + Ursa";
        Purpose = "桌面界面学习示例";
        Description = "这个关于窗口刻意保持简单，适合拿来练习产品信息、运行环境、鸣谢说明或许可证文本的布局方式。";
        OsDescription = $"操作系统：{RuntimeInformation.OSDescription}";
        DotNetVersion = $".NET 版本：{Environment.Version}";
        ProcessArchitecture = $"进程架构：{RuntimeInformation.ProcessArchitecture}";
        OpenedAt = $"打开时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }

    public event EventHandler? CloseRequested;

    public string ProductName { get; }

    public string Version { get; }

    public string TechStack { get; }

    public string Purpose { get; }

    public string Description { get; }

    public string OsDescription { get; }

    public string DotNetVersion { get; }

    public string ProcessArchitecture { get; }

    public string OpenedAt { get; }

    [ReactiveCommand]
    private void Confirm()
    {
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }
}
