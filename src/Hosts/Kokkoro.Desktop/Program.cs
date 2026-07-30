using Avalonia;
#if DEBUG
using AvaloniaUI.DiagnosticsSupport;
#endif
using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;
using ReactiveUI.Avalonia;
using System;

namespace Kokkoro.Desktop;

internal static class Program
{
    private const string ExecutablePath =
        @"D:\AvaloniaSys\avaloniaui.developertools.windows\2.2.2\avaloniaui.developertools.windows\2.2.2\tools\net6.0\any\tool-win-x64\Avalonia.DeveloperTools.exe";

    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
    private static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools(options =>
            {
#pragma warning disable CA1416
                options.Runner = DeveloperToolsRunner.CreateFromExecutable(ExecutablePath);
#pragma warning restore CA1416
            })
#endif
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI(_ => { });
}