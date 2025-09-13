namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("StoreBasket")
        .WithDescription("Stores basket for a user")
        .WithSummary("Stores basket for a user")
        .Produces<StoreBasketResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}