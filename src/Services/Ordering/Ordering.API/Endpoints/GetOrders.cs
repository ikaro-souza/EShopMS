using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

// public record GetOrdersRequest(PaginationRequest Pagination);

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));
            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        });
    }
}