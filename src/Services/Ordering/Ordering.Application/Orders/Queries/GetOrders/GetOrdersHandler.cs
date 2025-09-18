namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Take(query.Pagination.PageSize)
            .Skip(query.Pagination.PageIndex * query.Pagination.PageSize)
            .ToListAsync(cancellationToken);

        var totalCount = await dbContext.Orders.CountAsync(cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                query.Pagination.PageIndex,
                query.Pagination.PageSize,
                totalCount,
                orders.Select(OrderDto.From).ToList()
            )
        );
    }
}