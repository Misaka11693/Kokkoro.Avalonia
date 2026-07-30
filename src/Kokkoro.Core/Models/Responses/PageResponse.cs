using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Models;

public class PageResponse<T>
{
    public int TotalCount { get; set; }

    public List<T> Items { get; set; } = [];
}
