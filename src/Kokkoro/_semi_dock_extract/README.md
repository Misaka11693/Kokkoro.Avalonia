# Semi Theme for Dock.Avalonia

This is a theme for [Dock.Avalonia](https://github.com/wieslawsoltes/Dock)

## Before you start

This package is delivered via nuget for free, but not open source. Please read the license and agree to continue use this package.

If you need source code, please contact me via email: contact@irihi.tech

## Installation

Semi.Avalonia.Dock is used in conjunction with Semi.Avalonia and Dock.Avalonia. You need to install them all. 

```bash
dotnet add package Semi.Avalonia
dotnet add package Semi.Avalonia.Dock
```

## Usage

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourApplicationName.App"
             xmlns:semi="https://irihi.tech/semi">
    <Application.Styles>
        <semi:SemiTheme />
        <semi:DockSemiTheme />
    </Application.Styles>
</Application>
```
