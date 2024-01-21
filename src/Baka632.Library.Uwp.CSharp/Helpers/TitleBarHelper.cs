using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using Microsoft.Toolkit.Uwp.UI.Helpers;

namespace Baka632.Library.Uwp.CSharp.Helpers;

/// <summary>
/// 为标题栏相关操作提供帮助的类
/// </summary>
public static class TitleBarHelper
{
    private static Frame currentFrame;
    private static readonly ThemeListener themeListener = new();

    /// <summary>
    /// 当系统默认的后退按钮可视性发生改变时引发
    /// </summary>
    public static event EventHandler<AppViewBackButtonVisibility> BackButtonVisibilityChanged;
    /// <summary>
    /// 当标题栏的可视性发生改变时引发
    /// </summary>
    public static event EventHandler<CoreApplicationViewTitleBar> TitleBarVisibilityChanged;
    /// <summary>
    /// 系统导航管理器
    /// </summary>
    public static SystemNavigationManager NavigationManager { get; } = SystemNavigationManager.GetForCurrentView();

    /// <summary>
    /// 设置标题栏的默认外观
    /// </summary>
    public static void SetTitleBarAppearance()
    {
        if (EnvironmentHelper.IsWindowsMobile != true)
        {
            themeListener.ThemeChanged += OnThemeChanged;

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.IsVisibleChanged += OnTitleBarVisibilityChanged;

            ApplicationViewTitleBar presentationTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            presentationTitleBar.ButtonBackgroundColor = Colors.Transparent;
            Color foregroundColor = themeListener.CurrentTheme switch
            {
                ApplicationTheme.Light => Colors.Black,
                _ => Colors.White
            };
            presentationTitleBar.ButtonForegroundColor = foregroundColor;
        }
    }

    /// <summary>
    /// 侦听指定 <see cref="Frame"/> 的导航事件，以确定标题栏后退按钮的可见性
    /// </summary>
    /// <param name="frame">一个 <see cref="Frame"/> </param>
    public static void Hook(Frame frame)
    {
        if (EnvironmentHelper.IsWindowsMobile != true)
        {
            if (frame is null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            Dehook();
            frame.Navigated += OnCurrentFrameNavigated;
            currentFrame = frame;
        }
    }

    /// <summary>
    /// 不再侦听来自 <see cref="Frame"/> 的导航事件
    /// </summary>
    public static void Dehook()
    {
        if (currentFrame is not null)
        {
            currentFrame.Navigated -= OnCurrentFrameNavigated;
        }
    }

    private static void OnTitleBarVisibilityChanged(CoreApplicationViewTitleBar sender, object args)
    {
        TitleBarVisibilityChanged?.Invoke(null, sender);
    }

    private static void OnCurrentFrameNavigated(object sender, NavigationEventArgs e)
    {
        NavigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibilityToBoolean((sender as Frame).CanGoBack);
        BackButtonVisibilityChanged?.Invoke(null, NavigationManager.AppViewBackButtonVisibility);
    }

    private static AppViewBackButtonVisibility AppViewBackButtonVisibilityToBoolean(bool canGoBack)
    {
        return canGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
    }

    private static void OnThemeChanged(ThemeListener sender)
    {
        ApplicationViewTitleBar presentationTitleBar = ApplicationView.GetForCurrentView().TitleBar;

        presentationTitleBar.ButtonForegroundColor = themeListener.CurrentTheme switch
        {
            ApplicationTheme.Light => Colors.Black,
            _ => Colors.White
        };
    }
}

/// <summary>
/// 为“后退按钮的可视性发生改变”事件提供数据
/// </summary>
public sealed class BackButtonVisibilityChangedEventArgs(AppViewBackButtonVisibility visibility) : EventArgs
{
    /// <summary>
    /// 当前后退按钮的可视性
    /// </summary>
    public AppViewBackButtonVisibility BackButtonVisibility { get; } = visibility;
}
