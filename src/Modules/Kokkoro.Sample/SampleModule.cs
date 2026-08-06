    using Kokkoro.Core.Apps;
using Kokkoro.Core.MetaModels;
using Kokkoro.Core.Modules;
using Kokkoro.Sample.ViewModels;
using Kokkoro.ViewModels.Core;

namespace Kokkoro.Sample;

public class SampleModule : DomainModule
{
    public override void Initialize(IApp app)
    {
        app.ModuleOperations += App_ModuleOperations;
        app.StartupCompleted += App_StartupCompleted;
        app.Exiting += App_Exiting;
    }

    private void App_ModuleOperations(object? sender, EventArgs e)
    {
        MenuManager.AddModules(
            new MenuItemMeta(" 示例模块 ", "SemiIconSetting")
            {
                Children = new List<MenuItemMeta>
                {
                    new MenuItemMeta("通知提醒","SemiIconBell",typeof(NotificationDemoViewModel)),
                    new MenuItemMeta("标准对话框", "SemiIconSetting", typeof(DialogServiceDemoViewModel)),
                    new MenuItemMeta("消息对话框", "SemiIconComment", typeof(MessageServiceDemoViewModel)),
                    new MenuItemMeta("Overlay 对话框", "SemiIconLayers", typeof(OverlayDialogServiceDemoViewModel)),
                    new MenuItemMeta("轻提示", "SemiIconBell", typeof(ToastServiceDemoViewModel)),
                    new MenuItemMeta("Semi颜色卡","SemiIconContrast",typeof(ColorsPageViewModel))
                }
            }
        );
    }

    private void App_StartupCompleted(object? sender, EventArgs e)
    {
        Console.WriteLine("程序启动完成");
    }

    private void App_Exiting(object? sender, EventArgs e)
    {
        Console.WriteLine("程序退出中");
    }
}
