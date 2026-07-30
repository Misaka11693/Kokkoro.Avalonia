using Kokkoro.Admin.ViewModels;
using Kokkoro.Core.Apps;
using Kokkoro.Core.MetaModels;
using Kokkoro.Core.Modules;
using Kokkoro.ViewModels.Core;

namespace Kokkoro.Admin;

public class AdminModule : DomainModule
{
    public override void Initialize(IApp app)
    {
        app.ModuleOperations += OnModuleOperations;
    }

    private static void OnModuleOperations(object? sender, EventArgs e)
    {
        MenuManager.AddModules(
            new MenuItemMeta("管理界面", "SemiIconInheritStroked")
            {
                Children =
                [
                    new MenuItemMeta("数据列表", "SemiIconGridStroked", typeof(AdminListViewModel)),
                    new MenuItemMeta("树形结构", "SemiIconBookStroked", typeof(AdminTreeViewModel))
                ]
            });
    }
}
