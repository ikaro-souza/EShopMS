namespace BuildingBlocks.Pagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);

public record PaginatedResult<TEntity>(int PageIndex, int PageSize, int Count, IEnumerable<TEntity> Items)
    where TEntity : class;