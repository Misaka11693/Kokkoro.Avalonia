using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Kokkoro.Core.Helpers;
namespace Kokkoro.Core.UI.Dialogs;

public static class DialogHelper
{
    //private static readonly Stack<Window> _dialogStack = new();

    public static async Task<int> ShowDialogAsync(Control view, object? vm, Window? owner = null, Action<IDialogOptions>? configureOptions = null)
    {
        view.DataContext = vm;
        var window = new KokkoroDialogWindow();
        window.SetContent(view);
        var options = ConfigureDialogWindow(window, configureOptions);
        window.Icon = options.Icon ?? owner?.Icon;
        window.GenerateCommands();
        //owner ??= GetMainWindow();
        //if (owner is null)
        //{
        //    window.Show();
        //    return -1;
        //}
        //await window.ShowDialog<int>(owner); 
        //return window.Result;
        owner = GetCurrentOwner(owner);

        if (owner == null)
        {
            window.Show();
            return -1;
        }

        //Push(window);

        try
        {
            await window.ShowDialog<int>(owner);
            return window.Result;
        }
        finally
        {
            //Pop(window);
        }
    }

    private static Window? GetMainWindow()
    {
        //var lifetime = Application.Current?.ApplicationLifetime;
        //return lifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } w } ? w : null;
        return WindowHelper.GetActiveWindow();
    }

    private static DialogOptions ConfigureDialogWindow(KokkoroDialogWindow window, Action<IDialogOptions>? configureOptions)
    {
        var options = new DialogOptions();
        configureOptions?.Invoke(options);

        window.Title = string.IsNullOrWhiteSpace(options.Title) ? "对话框" : options.Title;
        window.Commands.AddRange(options.Commands);
        window.MinWidth = options.MinWidth;
        window.MinHeight = options.MinHeight;
        if (options.MaxWidth.HasValue)
        {
            window.MaxWidth = options.MaxWidth.Value;
        }
        if (options.MaxHeight.HasValue)
        {
            window.MaxHeight = options.MaxHeight.Value;
        }
        window.CanResize = options.CanResize;
        window.CanMinimize = options.CanMinimize;
        window.CanMaximize = options.CanMaximize;
        window.Topmost = options.Topmost;
        window.ShowInTaskbar = options.ShowInTaskbar;
        window.DefaultButton = options.DefaultButton;
        window.WindowStartupLocation = options.WindowStartupLocation;
        window.BeforeButtonCloseAsync = options.BeforeButtonCloseAsync;
        ScrollViewer.SetHorizontalScrollBarVisibility(window, options.HorizontalScrollBarVisibility);
        ScrollViewer.SetVerticalScrollBarVisibility(window, options.VerticalScrollBarVisibility);
        if (options.WindowStartupLocation == WindowStartupLocation.Manual)
        {
            if (options.Position is not null)
                window.Position = options.Position.Value;
            else
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
        if (options.SizeMode == DialogSizeMode.Content)
        {
            window.SizeToContent = SizeToContent.WidthAndHeight;
        }
        else if (options.Width.HasValue || options.Height.HasValue)
        {
            window.SizeToContent = SizeToContent.Manual;
            window.Width = options.Width ?? 800;
            window.Height = options.Height ?? 600;
        }
        else
        {
            window.SizeToContent = SizeToContent.Manual;
            var screen = window.Screens.Primary!.WorkingArea;
            window.Width = screen.Width * options.ScreenRatio;
            window.Height = screen.Height * options.ScreenRatio;
        }

        return options;
    }

    public static Window? GetCurrentOwner(Window? owner = null)
    {
        if (owner != null)
            return owner;

        //if (_dialogStack.Count > 0)
        //    return _dialogStack.Peek();

        return GetMainWindow();
    }

    //private static void Push(Window window)
    //{
    //    _dialogStack.Push(window);
    //}
    //private static void Pop(Window window)
    //{
    //    if (_dialogStack.TryPeek(out var top) && ReferenceEquals(top, window))
    //    {
    //        _dialogStack.Pop();
    //    }
    //    else
    //    {
    //        // 理论上不会发生，防止异常关闭导致栈错乱
    //        //var temp = new Stack<Window>();

    //        //while (_dialogStack.TryPop(out var item))
    //        //{
    //        //    if (ReferenceEquals(item, window))
    //        //        break;

    //        //    temp.Push(item);
    //        //}

    //        //while (temp.Count > 0)
    //        //    _dialogStack.Push(temp.Pop());

    //        throw new Exception("弹窗关闭异常");
    //    }
    //}

}
