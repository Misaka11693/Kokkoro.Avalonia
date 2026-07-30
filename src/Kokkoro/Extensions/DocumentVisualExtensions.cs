using Avalonia;
using Avalonia.VisualTree;
using Dock.Model.ReactiveUI.Controls;

namespace Kokkoro.Extensions;

internal static class DocumentVisualExtensions
{
    public static Document? FindDocument(this Visual visual)
    {
        if (visual.DataContext is Document document)
        {
            return document;
        }

        foreach (var ancestor in visual.GetVisualAncestors())
        {
            if (ancestor is IViewFor { ViewModel: Document viewModel })
            {
                return viewModel;
            }

            if (ancestor.DataContext is Document dataContextDocument)
            {
                return dataContextDocument;
            }
        }

        return null;
    }
}
