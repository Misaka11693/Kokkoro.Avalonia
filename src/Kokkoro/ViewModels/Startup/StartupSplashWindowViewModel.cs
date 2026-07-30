using Irihi.Avalonia.Shared.Contracts;
using Kokkoro.Core.Apps;
using Kokkoro.ViewModels.Core;
using ReactiveUI.SourceGenerators;
using System.IO;
using System.Net.NetworkInformation;

namespace Kokkoro.ViewModels.Startup;

public partial class StartupSplashWindowViewModel : ViewModelBase, IDialogContext
{
    private static readonly TimeSpan ExitDelay = TimeSpan.FromMilliseconds(3000);

    [Reactive]
    private double _progress = 0;

    [Reactive]
    private string _statusText = "启动中...";

    public StartupSplashWindowViewModel()
    {
        _ = InitializeAsync();
    }

    public string Title => "Kokkoro";

    public string Subtitle => "业务工作台正在完成启动准备";

    public string SectionTitle => "启动检查";

    public string FooterText => "正在检查网络、工作目录、运行环境以及登录依赖。";

    public event EventHandler<object?>? RequestClose;

    public void Close()
    {
        RequestClose?.Invoke(this, false);
    }

    private async Task InitializeAsync()
    {
        var steps = new List<StartupStep>
        {
            new("正在检查网络连接...", 25, CheckNetworkAsync),
            new("正在检查运行环境...", 80, CheckRuntimeEnvironmentAsync),
            new("正在准备登录窗口...", 100, PrepareAuthWindowAsync)
        };

        foreach (var step in steps)
        {   
            await RunStepAsync(step);
        }

        await Task.Delay(ExitDelay);
        RequestClose?.Invoke(this, true);
    }

    private async Task RunStepAsync(StartupStep step)
    {
        StatusText = step.PendingText;
        StatusText = await step.ExecuteAsync();
        Progress = step.TargetProgress;
    }

    private static async Task<string> CheckNetworkAsync()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            return "网络检查完成：当前未检测到可用网络，将继续以本地模式启动。";
        }

        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync("223.5.5.5", 1500);
            return reply.Status == IPStatus.Success
                ? $"网络检查完成：连接正常，延迟 {reply.RoundtripTime} ms。"
                : $"网络检查完成：网络可用，但 Ping 状态为 {reply.Status}。";
        }
        catch (Exception ex)
        {
            return $"网络检查完成：检测过程中出现异常（{ex.GetType().Name}）。";
        }
    }

    private static Task<string> CheckRuntimeEnvironmentAsync()
    {
        return Task.FromResult($".NET 运行时检查完成：{Environment.Version}");
    }

    private static Task<string> PrepareAuthWindowAsync()
    {
        if (Application.Current is null)
        {
            return Task.FromResult("登录窗口准备失败：应用上下文不可用。");
        }

        _ = AppRuntime.Service.Resolve<ViewModels.Auth.AuthWindowViewModel>();
        return Task.FromResult("登录窗口准备完成。");
    }

    private sealed class StartupStep(string pendingText, double targetProgress, Func<Task<string>> executeAsync)
    {
        public string PendingText { get; } = pendingText;

        public double TargetProgress { get; } = targetProgress;

        public Func<Task<string>> ExecuteAsync { get; } = executeAsync;
    }
}
