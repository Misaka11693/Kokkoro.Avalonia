using Avalonia.Media;
using Kokkoro.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.MetaModels;

public class PageMeta
{
    public string? Key { get; set; }

    public string? Title { get; set; }

    public Type? EntityType { get; set; }

    public Geometry? Icon { get; init; }
}
