namespace Logbook.Services.Pagination;

public interface IOffsetPagination<T>
{
    public Task<IEnumerable<T>> GetAsync(OffsetPaginationSegment segment);
}