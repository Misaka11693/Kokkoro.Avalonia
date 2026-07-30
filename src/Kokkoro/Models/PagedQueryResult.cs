namespace Kokkoro.Models;

/// <summary>模拟后端分页查询结果。</summary>
public sealed record PagedQueryResult<T>(IReadOnlyList<T> Items, int TotalCount, int CurrentPage);
