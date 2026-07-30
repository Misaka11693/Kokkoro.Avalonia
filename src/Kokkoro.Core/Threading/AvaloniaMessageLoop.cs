﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using Avalonia.Threading;
using Kokkoro.Core.Threading;

namespace Kokkoro.Threading;

/// <summary>
/// Avalonia 消息循环实现。
/// </summary>
public sealed class AvaloniaMessageLoop : IMessageLoop
{
    private readonly Dispatcher _dispatcher;
    private readonly SynchronizationContext? _synchronizationContext;

    /// <summary>
    /// 初始化消息循环。
    /// </summary>
    /// <param name="dispatcher">
    /// Avalonia Dispatcher。
    /// </param>
    /// <param name="synchronizationContext">
    /// UI线程同步上下文。
    /// </param>
    public AvaloniaMessageLoop(
        Dispatcher dispatcher,
        SynchronizationContext? synchronizationContext)
    {
        _dispatcher = dispatcher
            ?? throw new ArgumentNullException(nameof(dispatcher));

        _synchronizationContext = synchronizationContext;
    }

    /// <inheritdoc />
    public bool CheckAccess()
    {
        return _dispatcher.CheckAccess();
    }

    /// <inheritdoc />
    public void VerifyAccess()
    {
        _dispatcher.VerifyAccess();
    }

    /// <inheritdoc />
    public void InvokeIfRequired(Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            callback();
            return;
        }

        _dispatcher.Invoke(callback);
    }

    /// <inheritdoc />
    public T InvokeIfRequired<T>(Func<T> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            return callback();
        }

        return _dispatcher.Invoke(callback);
    }

    /// <inheritdoc />
    public Task InvokeIfRequiredAsync(Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            callback();
            return Task.CompletedTask;
        }

        return _dispatcher
            .InvokeAsync(callback)
            .GetTask();
    }

    /// <inheritdoc />
    public async Task InvokeIfRequiredAsync(Func<Task> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            await callback();
            return;
        }

        await _dispatcher.InvokeAsync(callback);
    }

    /// <inheritdoc />
    public Task<T> InvokeIfRequiredAsync<T>(Func<T> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            return Task.FromResult(callback());
        }

        return _dispatcher
            .InvokeAsync(callback)
            .GetTask();
    }

    /// <inheritdoc />
    public async Task<T> InvokeIfRequiredAsync<T>(Func<Task<T>> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        if (_dispatcher.CheckAccess())
        {
            return await callback();
        }

        return await _dispatcher.InvokeAsync(callback);
    }

    /// <inheritdoc />
    public Task InvokeAsync(Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        return _dispatcher
            .InvokeAsync(callback)
            .GetTask();
    }

    /// <inheritdoc />
    public async Task InvokeAsync(Func<Task> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        await _dispatcher.InvokeAsync(callback);
    }

    /// <inheritdoc />
    public Task<T> InvokeAsync<T>(Func<T> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        return _dispatcher
            .InvokeAsync(callback)
            .GetTask();
    }

    /// <inheritdoc />
    public async Task<T> InvokeAsync<T>(Func<Task<T>> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        return await _dispatcher.InvokeAsync(callback);
    }

    /// <inheritdoc />
    public void Post(Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        _dispatcher.Post(callback);
    }

    /// <inheritdoc />
    public async void CallLater(
        TimeSpan delay,
        Action callback)
    {
        ArgumentNullException.ThrowIfNull(callback);

        await Task.Delay(delay)
            .ConfigureAwait(false);

        _dispatcher.Post(callback);
    }

    #region Internal

    /// <summary>
    /// 当前 Dispatcher。
    /// <para>
    /// 仅框架内部使用。
    /// </para>
    /// </summary>
    internal Dispatcher Dispatcher => _dispatcher;

    /// <summary>
    /// 当前同步上下文。
    /// <para>
    /// 仅框架内部使用。
    /// </para>
    /// </summary>
    internal SynchronizationContext? SynchronizationContext
        => _synchronizationContext;

    #endregion
}
