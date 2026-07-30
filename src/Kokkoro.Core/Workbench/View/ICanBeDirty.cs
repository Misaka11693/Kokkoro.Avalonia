using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Workbench.View;

public interface ICanBeDirty
{
    bool IsDirty { get; }

    event EventHandler IsDirtyChanged;
}
