namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : IRequest<UpdateProductResult>;

public record UpdateProductResult(Product Product);

public class UpdateProductCommandHandler(IDocumentSession documentSession, ILogger<UpdateProductCommandHandler> logger)
    : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle() called");

        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with ID {ProductId} not found", command.Id);
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        documentSession.Update(product);

        await documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}