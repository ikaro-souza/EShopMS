namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);

        var orderDtos = orders.Select(OrderDto.From).ToList();
        return new GetOrdersByCustomerResult(orderDtos);
    }
}