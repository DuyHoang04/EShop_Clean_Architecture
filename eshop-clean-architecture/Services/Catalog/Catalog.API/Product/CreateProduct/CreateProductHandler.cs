namespace Catalog.API.Product.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    string Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is require");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is require");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is require");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is require");
    }
}

public class CreateProductHandler(IDocumentSession Session, ILogger<CreateProductHandler> logger)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Models.Product? product = new()
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        };
        Session.Store(product);
        await Session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}