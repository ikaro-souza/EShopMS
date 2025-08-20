namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<DeleteProductResult>;

public record DeleteProductResult(bool Succeeded);

internal class DeleteProductCommandHandler(
    IDocumentSession documentSession,
    ILogger<DeleteProductCommandHandler> logger)
    : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommand handler called with {@Command}", command);

        var product = await documentSession.LoadAsync<Product>(command.ProductId, cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with ID {ProductId} not found", command.ProductId);
            throw new ProductNotFoundException();
        }

        documentSession.Delete(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}