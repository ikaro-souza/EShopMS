using BuildingBlocks.Pagination;

namespace Ordering.API.Endpoints;

public record GetOrdersRequest(PaginationRequest Pagination);

public record GetOrdersResponse(string OrderId);

public class GetOrders
{
}