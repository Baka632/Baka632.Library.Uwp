using Windows.UI.Core;

namespace Baka632.Library.Uwp.CSharp.Helpers;

/// <summary>
/// 为应用程序访问 UI 线程提供方法的类
/// </summary>
public static class UIThreadHelper
{
    private static CoreDispatcher _dispatcher;

    /// <summary>
    /// 使用指定的 <see cref="CoreDispatcher"/> 初始化此类 
    /// </summary>
    /// <param name="dispatcher">一个 <see cref="CoreDispatcher"/></param>
    /// <exception cref="ArgumentException"><paramref name="dispatcher"/> 为 <see langword="null"/></exception>
    public static void Initialize(CoreDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    /// <summary>
    /// 使用正常优先级在 UI 线程上调用指定的方法
    /// </summary>
    /// <param name="dispatchedHandler">要执行的方法</param>
    /// <exception cref="InvalidOperationException">未调用 <see cref="Initialize(CoreDispatcher)"/> 方法</exception>
    public static async Task RunOnUIThread(DispatchedHandler dispatchedHandler)
    {
        if (_dispatcher == null)
        {
            throw new InvalidOperationException($"请先调用 {nameof(Initialize)} 方法");
        }

        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, dispatchedHandler);
    }

    /// <summary>
    /// 使用正常优先级在 UI 线程上调用指定的方法，并返回一个值
    /// </summary>
    /// <typeparam name="T">指定方法的返回值</typeparam>
    /// <param name="func">要执行的有返回值方法</param>
    /// <returns>指定方法的返回值</returns>
    /// <exception cref="InvalidOperationException">未调用 <see cref="Initialize(CoreDispatcher)"/> 方法</exception>
    public static async Task<T> RunOnUIThread<T>(Func<T> func)
    {
        if (_dispatcher == null)
        {
            throw new InvalidOperationException($"请先调用 {nameof(Initialize)} 方法");
        }

        TaskCompletionSource<T> source = new();

        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            try
            {
                T result = func();
                source.TrySetResult(result);
            }
            catch (Exception e)
            {
                source.TrySetException(e);
            }
        });

        return await source.Task;
    }

    /// <summary>
    /// 使用正常优先级在 UI 线程上调用指定的方法，并返回一个值
    /// </summary>
    /// <typeparam name="T">指定方法的返回值</typeparam>
    /// <param name="func">要执行的有返回值方法</param>
    /// <returns>指定方法的返回值</returns>
    /// <exception cref="InvalidOperationException">未调用 <see cref="Initialize(CoreDispatcher)"/> 方法</exception>
    public static async Task<T> RunOnUIThread<T>(Func<Task<T>> func)
    {
        if (_dispatcher == null)
        {
            throw new InvalidOperationException($"请先调用 {nameof(Initialize)} 方法");
        }

        TaskCompletionSource<T> source = new();

        await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
        {
            try
            {
                T result = await func();
                source.TrySetResult(result);
            }
            catch (Exception e)
            {
                source.TrySetException(e);
            }
        });

        return await source.Task;
    }
}
