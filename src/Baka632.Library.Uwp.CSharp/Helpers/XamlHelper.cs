﻿// Fix CS0419
using Color = Windows.UI.Color;

namespace Baka632.Library.Uwp.CSharp.Helpers;

/// <summary>
/// 为在 Xaml 中进行数值转换等操作提供方法的类
/// </summary>
public static class XamlHelper
{
    /// <summary>
    /// 将传入的布尔值反转
    /// </summary>
    /// <param name="value">要反转的布尔值</param>
    /// <returns>反转后的布尔值</returns>
    public static bool ReverseBoolean(bool value) => !value;

    /// <summary>
    /// 将传入的 <see cref="Visibility"/> 反转
    /// </summary>
    /// <param name="value">要反转的 <see cref="Visibility"/></param>
    /// <returns>反转后的 <see cref="Visibility"/></returns>
    public static Visibility ReverseVisibility(Visibility value)
    {
        return value switch
        {
            Visibility.Visible => Visibility.Collapsed,
            _ => Visibility.Visible,
        };
    }

    /// <summary>
    /// 将布尔值转换为 <see cref="Visibility"/>，并将得到的 <see cref="Visibility"/> 反转
    /// </summary>
    /// <param name="value">要反转的布尔值</param>
    /// <returns>完成反转操作的 <see cref="Visibility"/></returns>
    public static Visibility ReverseVisibility(bool value)
    {
        return value switch
        {
            true => Visibility.Collapsed,
            false => Visibility.Visible,
        };
    }

    /// <summary>
    /// 将布尔值转换为 <see cref="Visibility"/>
    /// </summary>
    /// <param name="value">要进行操作的布尔值</param>
    /// <returns>由布尔值转换而来的 <see cref="Visibility"/></returns>
    public static Visibility ToVisibility(bool value)
    {
        return value switch
        {
            true => Visibility.Visible,
            false => Visibility.Collapsed,
        };
    }

    /// <summary>
    /// 将可空布尔值转换为 <see cref="Visibility"/>
    /// </summary>
    /// <param name="value">要进行操作的可空布尔值</param>
    /// <param name="defaultVisibilityForNull">当 <paramref name="value"/> 为 <see langword="null"/> 时返回的值，默认为 <see cref="Visibility.Collapsed"/></param>
    /// <returns>由布尔值转换而来的 <see cref="Visibility"/></returns>
    public static Visibility ToVisibility(bool? value, Visibility defaultVisibilityForNull = Visibility.Collapsed)
    {
        return value switch
        {
            true => Visibility.Visible,
            false => Visibility.Collapsed,
            null => defaultVisibilityForNull
        };
    }

    /// <summary>
    /// 将可空布尔值转换为 <see cref="Visibility"/>
    /// </summary>
    /// <param name="value">要进行操作的可空布尔值</param>
    /// <returns>由可空布尔值转换而来的 <see cref="Visibility"/></returns>
    public static Visibility ToVisibility(bool? value)
    {
        return value switch
        {
            true => Visibility.Visible,
            _ => Visibility.Collapsed
        };
    }

    /// <summary>
    /// 按照格式，将 <see cref="DateTimeOffset"/> 转换为字符串
    /// </summary>
    /// <param name="value">要转换的 <see cref="DateTimeOffset"/> 实例</param>
    /// <param name="format">字符串的格式</param>
    /// <exception cref="FormatException">当格式不正确时抛出</exception>
    /// <returns>格式化的日期字符串</returns>
    public static string ToFormatedString(DateTimeOffset value, string format)
    {
        return value.ToString(format);
    }

    /// <summary>
    /// 确定对象是否为空
    /// </summary>
    /// <param name="value">要检查的对象</param>
    /// <returns>确定对象是否为空的值</returns>
    public static bool IsNull(object value) => value is null;

    /// <summary>
    /// 确定对象是否不为空
    /// </summary>
    /// <param name="value">要检查的对象</param>
    /// <returns>确定对象是否不为空的值</returns>
    public static bool IsNotNull(object value) => value is not null;

    /// <summary>
    /// 确定对象是否为空，并将其表示为 <see cref="Visibility"/>
    /// </summary>
    /// <param name="value">要检查的对象</param>
    /// <returns>由对象是否为空的值而转换得到的 <see cref="Visibility"/></returns>
    public static Visibility IsNullToVisibility(object value)
    {
        return ToVisibility(IsNull(value));
    }

    /// <summary>
    /// 确定对象是否为空，并将得到的 <see cref="Visibility"/> 反转
    /// </summary>
    /// <param name="value">要检查的对象</param>
    /// <returns>完成反转操作的 <see cref="Visibility"/></returns>
    public static Visibility IsNullReverseVisibility(object value)
    {
        return ReverseVisibility(IsNull(value));
    }

    /// <summary>
    /// 确定字符串是否为 <see langword="null"/> 或空字符串
    /// </summary>
    /// <param name="value">要检查的字符串</param>
    /// <returns>确定字符串是否为 <see langword="null"/> 或空字符串的值</returns>
    public static bool IsStringNullOrEmpty(string value) => string.IsNullOrEmpty(value);

    /// <summary>
    /// 确定字符串是否不为 <see langword="null"/> 或空字符串
    /// </summary>
    /// <param name="value">要检查的字符串</param>
    /// <returns>确定字符串是否不为 <see langword="null"/> 或空字符串的值</returns>
    public static bool IsStringNotNullOrEmpty(string value) => IsStringNullOrEmpty(value) != true;

    /// <summary>
    /// 确定字符串是否为 <see langword="null"/> 或空字符串，并将其表示为 <see cref="Visibility"/>
    /// </summary>
    /// <param name="value">要检查的字符串</param>
    /// <returns>由对象是否为字符串是否为 <see langword="null"/> 或空字符串的值而转换得到的 <see cref="Visibility"/></returns>
    public static Visibility IsStringNullOrEmptyToVisibility(string value)
    {
        return ToVisibility(IsStringNullOrEmpty(value));
    }

    /// <summary>
    /// 确定字符串是否为 <see langword="null"/> 或空字符串，并将得到的 <see cref="Visibility"/> 反转
    /// </summary>
    /// <param name="value">要检查的字符串</param>
    /// <returns>完成反转操作的 <see cref="Visibility"/></returns>
    public static Visibility IsStringNullOrEmptyReverseVisibility(string value)
    {
        return ReverseVisibility(IsStringNullOrEmpty(value));
    }

    /// <summary>
    /// 将一个 <see cref="Color"/> 转换为 <see cref="SolidColorBrush"/>
    /// </summary>
    /// <param name="color">一个 <see cref="Color"/> 实例</param>
    /// <returns>按 <paramref name="color"/> 构造而成的 <see cref="SolidColorBrush"/> 实例</returns>
    public static SolidColorBrush ToSolidColorBrush(Color color)
    {
        return new SolidColorBrush(color);
    }

    /// <summary>
    /// 将一个 <see cref="SolidColorBrush"/> 转换为 <see cref="Color"/>
    /// </summary>
    /// <param name="solidColorBrush">一个 <see cref="SolidColorBrush"/> 实例</param>
    /// <returns>从 <paramref name="solidColorBrush"/> 获得的的 <see cref="Color"/> 实例</returns>
    public static Color ToColor(SolidColorBrush solidColorBrush)
    {
        return solidColorBrush.Color;
    }
}
