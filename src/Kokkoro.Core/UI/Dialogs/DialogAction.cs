using System.Windows.Input;

namespace Kokkoro.Core.UI.Dialogs;

public class DialogAction
{
    public string Text { get; set; }
    public ICommand Command { get; set; }
    public bool IsDefault { get; set; }
    public bool IsCancel { get; set; }
    public int Result { get; set; }
}