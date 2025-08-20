namespace CatalogAPI.Products.DeleteProduct;

// public record DeleteProductRequest();
public record DeleteProductResponse(bool Succeeded);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId:guid}", async (Guid productId, ISender sender) =>
        {
            var command = new DeleteProductCommand(productId);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteProductResponse>();
            return response.Succeeded ? Results.Ok(response) : Results.BadRequest(response);
        });
    }
}