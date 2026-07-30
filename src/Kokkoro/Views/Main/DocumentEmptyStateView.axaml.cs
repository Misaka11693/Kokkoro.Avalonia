using Avalonia.Controls.Presenters;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Dock.Avalonia.Controls;
using Kokkoro.ViewModels.Main;
using ReactiveUI.Avalonia;

namespace Kokkoro.Views.Main;

public partial class DocumentEmptyStateView : ReactiveUserControl<DocumentEmptyStateViewModel>
{
    public DocumentEmptyStateView()
    {
        InitializeComponent();
    }
}
