namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerRequest(string OrderId);

public record GetOrdersByCustomerResponse(string OrderId);

public class GetOrdersByCustomer
{
}