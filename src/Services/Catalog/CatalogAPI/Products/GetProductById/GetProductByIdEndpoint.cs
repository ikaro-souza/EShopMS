namespace CatalogAPI.Products.GetProductById;

// public record GetProductByIdRequest();
public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{productId:guid}", async (Guid productId, ISender sender) =>
            {
                var query = new GetProductByIdQuery(productId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .WithSummary("Get a product by its ID")
            .WithDescription("Get a product by its ID")
            .Produces<GetProductByIdResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}