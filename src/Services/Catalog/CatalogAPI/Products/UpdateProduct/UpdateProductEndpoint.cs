namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

public record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{productId:guid}",
                async (Guid productId, UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();
                    command = command with { Id = productId };

                    var result = await sender.Send(command);
                    var response = result.Adapt<UpdateProductResponse>();
                    return Results.Ok(response);
                })
            .WithName("UpdateProduct")
            .WithSummary("Updates an existing product")
            .WithDescription("Updates an existing product")
            .Produces<UpdateProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}