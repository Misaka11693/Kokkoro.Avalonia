﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Threading.Tasks;

namespace Kokkoro.Core.Threading;

/// <summary>
/// UI消息循环抽象。
/// <para>
/// 用于统一封装主线程调度能力。
/// </para>
/// <para>
/// 业务层不应直接依赖 Dispatcher，
/// 而应通过 IMessageLoop 完成线程切换。
/// </para>
/// </summary>
public interface IMessageLoop
{
    /// <summary>
    /// 判断当前线程是否为 UI 线程。
    /// </summary>
    bool CheckAccess();

    /// <summary>
    /// 验证当前线程是否为 UI 线程。
    /// <para>
    /// 如果当前线程不是 UI 线程则抛出异常。
    /// </para>
    /// </summary>
    void VerifyAccess();

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则同步切换到 UI 线程执行。
    /// </summary>
    /// <param name="callback">执行委托。</param>
    void InvokeIfRequired(Action callback);

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则同步切换到 UI 线程执行。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="callback">执行委托。</param>
    /// <returns>执行结果。</returns>
    T InvokeIfRequired<T>(Func<T> callback);

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则异步切换到 UI 线程执行。
    /// </summary>
    /// <param name="callback">执行委托。</param>
    Task InvokeIfRequiredAsync(Action callback);

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则异步切换到 UI 线程执行。
    /// </summary>
    /// <param name="callback">异步执行委托。</param>
    Task InvokeIfRequiredAsync(Func<Task> callback);

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则异步切换到 UI 线程执行。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="callback">执行委托。</param>
    /// <returns>执行结果。</returns>
    Task<T> InvokeIfRequiredAsync<T>(Func<T> callback);

    /// <summary>
    /// 如果当前线程不是 UI 线程，
    /// 则异步切换到 UI 线程执行。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="callback">异步执行委托。</param>
    /// <returns>执行结果。</returns>
    Task<T> InvokeIfRequiredAsync<T>(Func<Task<T>> callback);

    /// <summary>
    /// 异步切换到 UI 线程执行。
    /// </summary>
    /// <param name="callback">执行委托。</param>
    Task InvokeAsync(Action callback);

    /// <summary>
    /// 异步切换到 UI 线程执行。
    /// </summary>
    /// <param name="callback">异步执行委托。</param>
    Task InvokeAsync(Func<Task> callback);

    /// <summary>
    /// 异步切换到 UI 线程执行。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="callback">执行委托。</param>
    /// <returns>执行结果。</returns>
    Task<T> InvokeAsync<T>(Func<T> callback);

    /// <summary>
    /// 异步切换到 UI 线程执行。
    /// </summary>
    /// <typeparam name="T">返回值类型。</typeparam>
    /// <param name="callback">异步执行委托。</param>
    /// <returns>执行结果。</returns>
    Task<T> InvokeAsync<T>(Func<Task<T>> callback);

    /// <summary>
    /// 将任务投递到 UI 消息队列。
    /// <para>
    /// 不等待执行完成。
    /// </para>
    /// </summary>
    /// <param name="callback">执行委托。</param>
    void Post(Action callback);

    /// <summary>
    /// 延迟执行指定方法。
    /// </summary>
    /// <param name="delay">延迟时间。</param>
    /// <param name="callback">执行委托。</param>
    void CallLater(TimeSpan delay, Action callback);
}
