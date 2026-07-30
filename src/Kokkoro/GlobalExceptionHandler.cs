using Avalonia.Threading;
using Kokkoro.Core.Apps;
using Kokkoro.Core.UI.Messages;

namespace Kokkoro;

public static class GlobalExceptionHandler
{
    public static void Init()
    {
        // UI线程未捕获异常
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Handle(e.ExceptionObject as Exception);
        };

        // Task异常
        TaskScheduler.UnobservedTaskException += (s, e) =>
        {
            //e.SetObserved();
            //Handle(e.Exception);
        };

        // Avalonia UI线程异常
        Dispatcher.UIThread.UnhandledException += (s, e) =>
        {
            e.Handled = true;
            Handle(e.Exception);
        };
    }

    // ReSharper disable once AsyncVoidMethod
    public static async void Handle(Exception? ex)
    {
        if (ex == null) return;

        try
        {
            await AppRuntime.MessageService.ShowExceptionAsync(ex);
        }
        // ReSharper disable once EmptyGeneralCatchClause
        catch (Exception)
        {
        }
    }
}
