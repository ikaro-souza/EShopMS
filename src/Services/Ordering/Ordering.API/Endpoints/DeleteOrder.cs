namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(string OrderId);

public record DeleteOrderResponse(string OrderId);

public class DeleteOrder
{
}