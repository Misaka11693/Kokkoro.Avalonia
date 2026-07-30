using Kokkoro.Sample.ViewModels;
using ReactiveUI.Avalonia;

namespace Kokkoro.Sample.Views;

public partial class NotificationDemoView : ReactiveUserControl<NotificationDemoViewModel>
{
    public NotificationDemoView()
    {
        InitializeComponent();
    }
}