using Avalonia.Controls;
using Avalonia.Input;
using Ursa.Controls;

namespace Kokkoro.Core.UI.Dialogs;

/// <summary>
/// Kokkoro 对话框窗口
/// </summary>
public partial class KokkoroDialogWindow : UrsaWindow
{
    public Func<KokkoroDialogWindow, int, Task<bool>>? BeforeButtonCloseAsync { get; set; }

    /// <summary>
    /// 对话框结果
    /// -1：关闭按钮 / ESC
    /// 0~N：命令按钮索引
    /// </summary>
    public int Result { get; private set; } = -1;

    /// <summary>
    /// 默认按钮索引
    /// </summary>
    public int DefaultButton { get; set; } = -1;

    /// <summary>
    /// 命令按钮集合
    /// </summary>
    public IList<string> Commands { get; } = [];

    /// <summary>
    /// 构造函数
    /// </summary>
    public KokkoroDialogWindow()
    {
        InitializeComponent();
        ExtendClientAreaToDecorationsHint = true;
    }

    /// <summary>
    /// 设置内容
    /// </summary>
    public void SetContent(Control content)
    {
        PART_Content.Content = content;
    }

    /// <summary>
    /// 生成命令按钮
    /// </summary>
    public void GenerateCommands()
    {
        var duplicate = Commands
            .GroupBy(x => x)
            .FirstOrDefault(x => x.Count() > 1);

        if (duplicate is not null)
        {
            throw new InvalidOperationException($"按钮名称“{duplicate.Key}”重复定义。");
        }

        PART_CommandPanel.Children.Clear();

        if (Commands.Count == 0)
        {
            PART_CommandBar.IsVisible = false;
            return;
        }

        PART_CommandBar.IsVisible = true;

        for (var i = 0; i < Commands.Count; i++)
        {
            var index = i;

            var button = new Button
            {
                Content = Commands[i],
                IsDefault = DefaultButton == index
            };
            
            if (!button.IsDefault)
            {
                button.Classes.Add("Tertiary");
            }

            button.Click += async (_, _) =>
            {
                if (BeforeButtonCloseAsync is not null)
                {
                    var canClose = await BeforeButtonCloseAsync(this, index);
                    if (!canClose)
                    {
                        return;
                    }
                }

                Close(index);
            };

            PART_CommandPanel.Children.Add(button);
        }
    }

    /// <summary>
    /// ESC关闭
    /// </summary>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == Key.Escape)
        {
            Close(-1);
        }
    }

    /// <summary>
    /// 弹窗关闭
    /// </summary>
    private void Close(int result)
    {
        Result = result;
        base.Close();
    }
}
