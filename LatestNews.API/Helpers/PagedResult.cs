using Microsoft.EntityFrameworkCore;

namespace LatestNews.API.Helpers
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int page, int pageSize, int totalCount)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPerviousPage => Page * PageSize > 1;
        public IEnumerable<T> Items { get; set; } = [];

        public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new(items, page, pageSize, totalCount);
        }
    }
}
