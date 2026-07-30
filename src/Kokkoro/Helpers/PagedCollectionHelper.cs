using System.Collections.ObjectModel;

namespace Kokkoro.Helpers;

/// <summary>
/// 分页列表辅助。
/// </summary>
public static class PagedCollectionHelper
{
    public static int GetPageCount(int totalCount, int pageSize)
        => pageSize <= 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

    public static int ClampPage(int currentPage, int pageCount)
        => pageCount <= 0 ? 1 : Math.Clamp(currentPage, 1, pageCount);

    public static void PopulatePageRows<TItem, TRow>(
        ObservableCollection<TRow> target,
        IReadOnlyList<TItem> source,
        int currentPage,
        int pageSize,
        Func<TItem, TRow> createRow)
        where TItem : class
    {
        target.Clear();

        if (source.Count == 0 || pageSize <= 0)
        {
            return;
        }

        var pageCount = GetPageCount(source.Count, pageSize);
        var page = ClampPage(currentPage, pageCount);
        var skip = (page - 1) * pageSize;

        foreach (var item in source.Skip(skip).Take(pageSize))
        {
            target.Add(createRow(item));
        }
    }
}
