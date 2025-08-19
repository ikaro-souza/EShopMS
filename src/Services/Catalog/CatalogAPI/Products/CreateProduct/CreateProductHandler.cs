namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
) : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler(IDocumentSession documentSession)
    : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };

        // save to database
        documentSession.Store(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}