namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "{GetProductsQueryHandlerName}.{HandleName} called with {GetProductsQuery}.", nameof(
                GetProductsQueryHandler), nameof(Handle), query);

        var products = await documentSession.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}