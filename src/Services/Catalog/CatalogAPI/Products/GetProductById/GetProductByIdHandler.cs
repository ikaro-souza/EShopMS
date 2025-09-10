namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession documentSession, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQuery handler called with @{Query}", request);

        var product = await documentSession.LoadAsync<Product>(request.ProductId, cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with ID {ProductId} not found", request.ProductId);
            throw new ProductNotFoundException(request.ProductId);
        }

        return new GetProductByIdResult(product);
    }
}