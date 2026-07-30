namespace Kokkoro.ViewModels.Pages;

/// <summary>
/// Semi 常用色 token 参考页。
/// 分组与命名对齐 Semi.Avalonia.Demo <c>ColorTokens</c>（完整版）。
/// </summary>
public class ColorsPageViewModel : DocumentPageViewModel
{
    public IReadOnlyList<ColorTokenSection> Sections { get; } =
    [
        new("基础色阶",
        [
            new("背景 Background",
                "SemiColorBackground0",
                "SemiColorBackground1",
                "SemiColorBackground2",
                "SemiColorBackground3",
                "SemiColorBackground4"),
            new("填充 Fill",
                "SemiColorFill0",
                "SemiColorFill1",
                "SemiColorFill2"),
            new("文字 Text",
                "SemiColorText0",
                "SemiColorText1",
                "SemiColorText2",
                "SemiColorText3"),
            new("边框 Border",
                "SemiColorBorder",
                "SemiColorFocusBorder"),
        ]),
        new("主题色",
        [
            new("主题色 Primary",
                "SemiColorPrimary",
                "SemiColorPrimaryPointerover",
                "SemiColorPrimaryActive",
                "SemiColorPrimaryDisabled",
                "SemiColorPrimaryLight",
                "SemiColorPrimaryLightPointerover",
                "SemiColorPrimaryLightActive"),
            new("次要色 Secondary",
                "SemiColorSecondary",
                "SemiColorSecondaryPointerover",
                "SemiColorSecondaryActive",
                "SemiColorSecondaryDisabled",
                "SemiColorSecondaryLight",
                "SemiColorSecondaryLightPointerover",
                "SemiColorSecondaryLightActive"),
            new("第三色 Tertiary",
                "SemiColorTertiary",
                "SemiColorTertiaryPointerover",
                "SemiColorTertiaryActive",
                "SemiColorTertiaryLight",
                "SemiColorTertiaryLightPointerover",
                "SemiColorTertiaryLightActive"),
        ]),
        new("语义色",
        [
            new("信息 Information",
                "SemiColorInformation",
                "SemiColorInformationPointerover",
                "SemiColorInformationActive",
                "SemiColorInformationDisabled",
                "SemiColorInformationLight",
                "SemiColorInformationLightPointerover",
                "SemiColorInformationLightActive"),
            new("成功 Success",
                "SemiColorSuccess",
                "SemiColorSuccessPointerover",
                "SemiColorSuccessActive",
                "SemiColorSuccessDisabled",
                "SemiColorSuccessLight",
                "SemiColorSuccessLightPointerover",
                "SemiColorSuccessLightActive"),
            new("警告 Warning",
                "SemiColorWarning",
                "SemiColorWarningPointerover",
                "SemiColorWarningActive",
                "SemiColorWarningLight",
                "SemiColorWarningLightPointerover",
                "SemiColorWarningLightActive"),
            new("危险 Danger",
                "SemiColorDanger",
                "SemiColorDangerPointerover",
                "SemiColorDangerActive",
                "SemiColorDangerLight",
                "SemiColorDangerLightPointerover",
                "SemiColorDangerLightActive"),
        ]),
        new("链接",
        [
            new("链接 Link",
                "SemiColorLink",
                "SemiColorLinkPointerover",
                "SemiColorLinkActive",
                "SemiColorLinkVisited"),
        ]),
        new("禁用态",
        [
            new("禁用 Disabled",
                "SemiColorDisabledText",
                "SemiColorDisabledBackground",
                "SemiColorDisabledBorder",
                "SemiColorDisabledFill"),
        ]),
        new("AI 相关",
        [
            new("AI 通用",
                "SemiColorAIGeneral",
                "SemiColorAIGeneralPointerover",
                "SemiColorAIGeneralActive",
                "SemiColorAIGeneralDisabled"),
            new("AI 紫色",
                "SemiColorAIPurple",
                "SemiColorAIPurplePointerover",
                "SemiColorAIPurpleActive",
                "SemiColorAIPurpleDisabled"),
            new("AI 背景",
                "SemiColorAIBackgroundBottom",
                "SemiColorAIBackgroundBottomPointerover",
                "SemiColorAIBackgroundBottomActive",
                "SemiColorAIBackgroundTop",
                "SemiColorAIBackgroundTopPointerover",
                "SemiColorAIBackgroundTopActive"),
        ]),
        new("调色板",
        [
            // ========== 红色系 ==========
            new("红 Red",
                "SemiRed0", "SemiRed1", "SemiRed2", "SemiRed3", "SemiRed4",
                "SemiRed5", "SemiRed6", "SemiRed7", "SemiRed8", "SemiRed9"),

            // ========== 粉色系 ==========
            new("粉 Pink",
                "SemiPink0", "SemiPink1", "SemiPink2", "SemiPink3", "SemiPink4",
                "SemiPink5", "SemiPink6", "SemiPink7", "SemiPink8", "SemiPink9"),

            // ========== 紫色系 ==========
            new("紫 Purple",
                "SemiPurple0", "SemiPurple1", "SemiPurple2", "SemiPurple3", "SemiPurple4",
                "SemiPurple5", "SemiPurple6", "SemiPurple7", "SemiPurple8", "SemiPurple9"),

            new("紫罗兰 Violet",
                "SemiViolet0", "SemiViolet1", "SemiViolet2", "SemiViolet3", "SemiViolet4",
                "SemiViolet5", "SemiViolet6", "SemiViolet7", "SemiViolet8", "SemiViolet9"),

            new("靛蓝 Indigo",
                "SemiIndigo0", "SemiIndigo1", "SemiIndigo2", "SemiIndigo3", "SemiIndigo4",
                "SemiIndigo5", "SemiIndigo6", "SemiIndigo7", "SemiIndigo8", "SemiIndigo9"),

            // ========== 蓝色系 ==========
            new("蓝 Blue",
                "SemiBlue0", "SemiBlue1", "SemiBlue2", "SemiBlue3", "SemiBlue4",
                "SemiBlue5", "SemiBlue6", "SemiBlue7", "SemiBlue8", "SemiBlue9"),

            new("浅蓝 Light Blue",
                "SemiLightBlue0", "SemiLightBlue1", "SemiLightBlue2", "SemiLightBlue3", "SemiLightBlue4",
                "SemiLightBlue5", "SemiLightBlue6", "SemiLightBlue7", "SemiLightBlue8", "SemiLightBlue9"),

            new("青 Cyan",
                "SemiCyan0", "SemiCyan1", "SemiCyan2", "SemiCyan3", "SemiCyan4",
                "SemiCyan5", "SemiCyan6", "SemiCyan7", "SemiCyan8", "SemiCyan9"),

            new("鸭绿 Teal",
                "SemiTeal0", "SemiTeal1", "SemiTeal2", "SemiTeal3", "SemiTeal4",
                "SemiTeal5", "SemiTeal6", "SemiTeal7", "SemiTeal8", "SemiTeal9"),

            // ========== 绿色系 ==========
            new("绿 Green",
                "SemiGreen0", "SemiGreen1", "SemiGreen2", "SemiGreen3", "SemiGreen4",
                "SemiGreen5", "SemiGreen6", "SemiGreen7", "SemiGreen8", "SemiGreen9"),

            new("浅绿 Light Green",
                "SemiLightGreen0", "SemiLightGreen1", "SemiLightGreen2", "SemiLightGreen3", "SemiLightGreen4",
                "SemiLightGreen5", "SemiLightGreen6", "SemiLightGreen7", "SemiLightGreen8", "SemiLightGreen9"),

            new("青柠 Lime",
                "SemiLime0", "SemiLime1", "SemiLime2", "SemiLime3", "SemiLime4",
                "SemiLime5", "SemiLime6", "SemiLime7", "SemiLime8", "SemiLime9"),

            // ========== 黄色系 ==========
            new("黄 Yellow",
                "SemiYellow0", "SemiYellow1", "SemiYellow2", "SemiYellow3", "SemiYellow4",
                "SemiYellow5", "SemiYellow6", "SemiYellow7", "SemiYellow8", "SemiYellow9"),

            new("琥珀 Amber",
                "SemiAmber0", "SemiAmber1", "SemiAmber2", "SemiAmber3", "SemiAmber4",
                "SemiAmber5", "SemiAmber6", "SemiAmber7", "SemiAmber8", "SemiAmber9"),

            // ========== 橙色系 ==========
            new("橙 Orange",
                "SemiOrange0", "SemiOrange1", "SemiOrange2", "SemiOrange3", "SemiOrange4",
                "SemiOrange5", "SemiOrange6", "SemiOrange7", "SemiOrange8", "SemiOrange9"),

            // ========== 灰色系 ==========
            new("灰 Grey",
                "SemiGrey0", "SemiGrey1", "SemiGrey2", "SemiGrey3", "SemiGrey4",
                "SemiGrey5", "SemiGrey6", "SemiGrey7", "SemiGrey8", "SemiGrey9"),

            // ========== AI 扩展 ==========
            new("AI 紫 AIPurple",
                "SemiAIPurple0", "SemiAIPurple1", "SemiAIPurple2", "SemiAIPurple3", "SemiAIPurple4",
                "SemiAIPurple5", "SemiAIPurple6", "SemiAIPurple7", "SemiAIPurple8", "SemiAIPurple9"),

            new("AI 通用 AIGeneral",
                "SemiAIGeneral0", "SemiAIGeneral1", "SemiAIGeneral2", "SemiAIGeneral3", "SemiAIGeneral4",
                "SemiAIGeneral5", "SemiAIGeneral6", "SemiAIGeneral7", "SemiAIGeneral8", "SemiAIGeneral9"),
        ]),
        new("其他",
        [
            new("其他 Other",
                "SemiColorWhite",
                "SemiColorBlack",
                "SemiColorNavBackground",
                "SemiColorOverlayBackground",
                "SemiColorHighlightBackground",
                "SemiColorHighlight"),
            new("阴影 Shadow",
                "SemiColorShadow",
                "SemiShadowElevated"),
        ]),
    ];
}

public sealed record ColorTokenSection(string Title, IReadOnlyList<ColorTokenGroup> Groups);

public sealed record ColorTokenGroup(string Title, params string[] Tokens);
