using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Kokkoro.Core.MetaModels;
using Kokkoro.ViewModels.Core;
using Kokkoro.ViewModels.Main;

namespace Kokkoro.Services;

public interface IDockLayoutManager
{
    /// <summary>
    /// Dock 根布局，主窗口直接绑定它来显示工作区。
    /// </summary>
    IRootDock Layout { get; }

    /// <summary>
    /// 左侧导航菜单。
    /// </summary>
    //IReadOnlyList<MenuItemViewModel> NavigationItems { get; }
    IReadOnlyList<MenuItemMeta> NavigationItems { get; }

    /// <summary>
    /// 当前处于激活状态的文档页。
    /// </summary>
    IDockable? ActiveDocument { get; }

    /// <summary>
    /// 当前已经打开的文档页。
    /// </summary>
    IReadOnlyList<Document> OpenDocuments { get; }

    /// <summary>
    /// 当前是否至少还有一个打开的文档页。
    /// </summary>
    bool HasOpenDocuments { get; }

    /// <summary>
    /// 打开页面；如果已经存在则直接激活。
    /// </summary>
    void OpenOrActivate(PageMeta pageMeta);

    /// <summary>
    /// 关闭当前激活的文档页。
    /// </summary>
    void CloseActiveDocument();

    /// <summary>
    /// 关闭指定路由对应的文档页。
    /// </summary>
    void CloseDocument(string routeKey);

    /// <summary>
    /// 关闭当前激活文档以外的其他文档页。
    /// </summary>
    void CloseOtherDocuments();

    /// <summary>
    /// 关闭当前所有文档页。
    /// </summary>
    void CloseAllDocuments();

    /// <summary>
    /// 判断指定路由的文档页当前是否已经打开。
    /// </summary>
    bool IsDocumentOpen(string routeKey);

    /// <summary>
    /// 快速切回首页。
    /// </summary>
    void ShowHome();

    /// <summary>
    /// 将工作区 Dock 布局恢复为初始状态（默认文档页并激活 Home）。
    /// </summary>
    void ResetToDefault();
}
