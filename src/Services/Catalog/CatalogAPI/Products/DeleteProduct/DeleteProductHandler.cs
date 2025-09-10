namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest<DeleteProductResult>;

public record DeleteProductResult(bool Succeeded);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
    }
}

internal class DeleteProductCommandHandler(
    IDocumentSession documentSession,
    ILogger<DeleteProductCommandHandler> logger,
    IValidator<DeleteProductCommand> validator)
    : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommand handler called with {@Command}", command);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
        if (errors.Count != 0) throw new ValidationException(errors.FirstOrDefault());

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