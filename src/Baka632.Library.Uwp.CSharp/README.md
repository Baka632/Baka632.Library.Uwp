![Library Icon](https://raw.githubusercontent.com/Baka632/Baka632.Library.Uwp/master/icon.png)

# Baka632.Library.Uwp.CSharp

为基于 C# 的 UWP 应用而设计的类库。

MIT 许可证。

## 常用类型

- XamlHelper
为函数绑定提供的工具类。
```xml
xmlns:helpers="using:Baka632.Library.Uwp.CSharp.Helpers"

<Button Visibility="{x:Bind helpers:XamlHelper.ReverseVisibility(ViewModel.IsLoading), Mode=OneWay}">
```
- LocalizationHelper
为应用本地化提供的工具类。
```csharp
public string AppDisplayName => "AppDisplayName".GetLocalized();
```
- EnvironmentHelper
为应用程序运行环境提供的工具类。
```csharp
if (EnvironmentHelper.IsWindowsMobile)
{
    IsShowTitlebar = false;
}
```
- AcrylicHelper & MicaHelper
为亚克力背景和云母背景提供的工具类。
```csharp
if (MicaHelper.IsSupported())
{
    MicaHelper.TrySetMica(this);
}
else if (AcrylicHelper.IsSupported())
{
    AcrylicHelper.TrySetAcrylicBrush(this);
}
```

## 支持版本

包版本限制为 Windows 10 Build 16299，但实际支持到 15063。

可以通过将项目最低版本设置为 16299 而将 ```Package.appxmanifest``` 中的 ```TargetDeviceFamily``` 设置为 15063 来支持较低版本的系统。

例如：
```xml
    <TargetDeviceFamily Name="Windows.Mobile" MinVersion="10.0.15063.0" MaxVersionTested="10.0.15254.0" />
```