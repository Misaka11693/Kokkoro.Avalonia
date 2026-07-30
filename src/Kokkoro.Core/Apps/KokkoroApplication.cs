using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace Kokkoro.Core.Apps;

public class KokkoroApplication : KokkoroApplicationBase
{
    public override void Initialize()
    {
        base.Initialize();

        OnInitialized();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// 应用初始化完成。
    /// 可用于注册应用级服务、加载资源等。
    /// </summary>
    public virtual void OnInitialized()
    {
    }

    /// <summary>
    /// 应用初始化完成。
    /// 可用于注册应用级服务、加载资源等。
    /// </summary>
    public virtual void BeforeOnInitialized()
    {
    }

    /// <summary>
    /// 应用启动完成。
    /// 可用于创建窗口、恢复布局、启动后台服务等。
    /// </summary>
    protected virtual void OnStarted()
    {
    }

    /// <summary>
    /// 根据 ViewModel 创建窗口。
    /// </summary>
    public Window CreateWindow<TViewModel>()
        where TViewModel : class
    {
        var viewModel = AppRuntime.Service.Resolve<TViewModel>();
        var view = AppRuntime.Service.Resolve<IViewFor<TViewModel>>();

        view.ViewModel = viewModel;

        if (view is not Window window)
        {
            throw new InvalidOperationException(
                $"The registered view '{view.GetType().Name}' must inherit from Window.");
        }

        return window;
    }

    /// <summary>
    /// 切换到指定 ViewModel 对应的窗口，并关闭当前窗口。
    /// </summary>
    /// <typeparam name="TViewModel">目标 ViewModel 类型</typeparam>
    /// <param name="currentWindow">当前要关闭的窗口</param>
    /// <param name="setAsMainWindow">是否将新窗口设为应用程序主窗口</param>
    public void SwitchToWindow<TViewModel>(Window currentWindow, bool setAsMainWindow = false)
        where TViewModel : class
    {
        var newWindow = CreateWindow<TViewModel>();

        if (setAsMainWindow && ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = newWindow;
        }

        newWindow.Show();
        currentWindow.Close();
    }
}
