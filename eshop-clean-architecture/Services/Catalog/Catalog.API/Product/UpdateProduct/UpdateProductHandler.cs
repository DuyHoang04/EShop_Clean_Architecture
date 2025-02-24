
namespace Catalog.API.Product.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, 
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        string Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductHandler(IDocumentSession Session, ILogger<UpdateProductHandler> Logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await Session.LoadAsync<Models.Product>(request.Id, cancellationToken);
            if (Product == null)
            {
                throw new ProductNotFoundException(Product.Id);
            }

            Product.Name = request.Name;
            Product.Category  = request.Category;
            Product.Description = request.Description;
            Product.ImageFile = request.ImageFile;
            Product.Price = request.Price;
            Session.Update(Product);
            await Session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
