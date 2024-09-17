
using Microsoft.EntityFrameworkCore;

namespace Netforemost_Todo_Api.Shared;
public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
public static class QueryableExtensions
{
    private static readonly int defaultPageSize=10;
    private static readonly int defaultPageNumber = 1;
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int? pageNumber,
        int? pageSize)
    {
        int Size = pageSize ?? defaultPageSize;
        int Number = pageNumber ?? defaultPageNumber;

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((Number - 1) * Size)
            .Take(Size)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = Size,
            PageSize = Number
        };
    }
}