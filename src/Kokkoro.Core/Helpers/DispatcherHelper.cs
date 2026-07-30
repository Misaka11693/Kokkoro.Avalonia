using Avalonia.Threading;

namespace Kokkoro.Core.Helpers;

/// <summary>
/// UI 线程调度帮助类。
/// </summary>
public static class DispatcherHelper
{
    /// <summary>
    /// 在 UI 线程同步执行指定操作。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    public static void Invoke(Action action)
    {
        Dispatcher.UIThread.Invoke(action);
    }

    /// <summary>
    /// 在 UI 线程异步执行指定操作。
    /// </summary>
    /// <param name="action">要执行的操作。</param>
    /// <returns>表示异步操作的任务。</returns>
    public static Task InvokeAsync(Action action)
    {
        return Dispatcher.UIThread.InvokeAsync(action).GetTask();
    }
}