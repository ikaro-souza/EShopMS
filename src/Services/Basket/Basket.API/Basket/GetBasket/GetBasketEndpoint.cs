using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

// public record GetBasketRequest(string UserName);
public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .WithDescription("Get basket by user name")
            .WithSummary("Get basket by user name")
            .Produces<GetBasketResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}