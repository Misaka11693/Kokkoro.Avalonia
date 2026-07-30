using System.ComponentModel;

namespace Kokkoro.Enums;

public enum AppThemeMode
{
    [Description("跟随系统")]
    System,

    [Description("Semi 浅色")]
    Light,

    [Description("Semi 深色")]
    Dark,

    [Description("Kokkoro 浅色")]
    KokkoroLight,

    [Description("Kokkoro 深色")]
    KokkoroDark,

    [Description("海洋浅色")]
    OceanLight,

    [Description("海洋深色")]
    OceanDark,

    [Description("森林浅色")]
    ForestLight,

    [Description("森林深色")]
    ForestDark,

    [Description("海洋")]
    Aquatic,

    [Description("沙漠")]
    Desert,

    [Description("黄昏")]
    Dusk,

    [Description("夜空")]
    NightSky
}
