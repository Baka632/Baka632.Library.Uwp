using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;

namespace Baka632.Library.Uwp.CSharp.Helpers;

/// <summary>
/// 为导航操作提供帮助方法的类
/// </summary>
public sealed class NavigationHelper
{
    private readonly static SystemNavigationManager navigationManager = TitleBarHelper.NavigationManager;
    private readonly Frame currentFrame;

    /// <summary>
    /// 当向后导航完成时引发
    /// </summary>
    public event EventHandler GoBackComplete;
    /// <summary>
    /// 当向前导航完成时引发
    /// </summary>
    public event EventHandler GoForwardComplete;
    /// <summary>
    /// 当向特定页导航完成时引发
    /// </summary>
    public event EventHandler NavigationComplete;

    /// <summary>
    /// 指示是否能向后导航的值
    /// </summary>
    public bool CanGoBack => currentFrame.CanGoBack;

    /// <summary>
    /// 使用指定的参数构造 <see cref="NavigationHelper"/> 的新实例
    /// </summary>
    /// <param name="frame">要进行导航操作的 <see cref="Frame"/></param>
    /// <param name="reactToBackRequested">指示是否响应系统后退请求的值</param>
    /// <exception cref="ArgumentNullException"><paramref name="frame"/> 为 <see langword="null"/></exception>
    public NavigationHelper(Frame frame, bool reactToBackRequested = false)
    {
        currentFrame = frame ?? throw new ArgumentNullException(nameof(frame));

        if (reactToBackRequested)
        {
            navigationManager.BackRequested += OnBackRequested;
        }
    }

    /// <summary>
    /// 销毁此对象时调用的方法
    /// </summary>
    ~NavigationHelper()
    {
        navigationManager.BackRequested -= OnBackRequested;
    }

    /// <summary>
    /// 进行向后导航
    /// </summary>
    public void GoBack()
    {
        if (currentFrame.CanGoBack)
        {
            currentFrame.GoBack();
            GoBackComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 进行向后导航
    /// </summary>
    /// <param name="e">系统导航请求事件的事件参数，将使用此参数将系统导航请求事件标记为已处理</param>
    public void GoBack(BackRequestedEventArgs e)
    {
        if (currentFrame.CanGoBack)
        {
            e.Handled = true;
            currentFrame.GoBack();
            GoBackComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 进行向前导航
    /// </summary>
    public void GoForward()
    {
        if (currentFrame.CanGoForward)
        {
            currentFrame.GoForward();
            GoForwardComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 导航到指定的页
    /// </summary>
    /// <param name="sourcePageType">表示目标页的 <see cref="Type"/></param>
    /// <param name="parameter">向目标页传递的参数</param>
    /// <param name="transitionInfo">要在导航时应用的切换效果</param>
    public void Navigate(Type sourcePageType, object parameter = null, NavigationTransitionInfo transitionInfo = null)
    {
        if (transitionInfo is not null)
        {
            currentFrame.Navigate(sourcePageType, parameter, transitionInfo);
        }
        else
        {
            currentFrame.Navigate(sourcePageType, parameter);
        }
        NavigationComplete?.Invoke(this, EventArgs.Empty);
    }

    private void OnBackRequested(object sender, BackRequestedEventArgs e)
    {
        GoBack(e);
    }
}
