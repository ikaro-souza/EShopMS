namespace Ordering.API.Endpoints;

public record GetOrdersByNameRequest(string OrderId);

public record GetOrdersByNameResponse(string OrderId);

public class GetOrdersByName
{
}