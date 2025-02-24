
namespace Catalog.API.Product.DeleteProduct
{
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductHandler(IDocumentSession Session, ILogger<DeleteProductHandler> Logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await Session.LoadAsync<Models.Product>(request.Id, cancellationToken);
            if (Product is null) throw new ProductNotFoundException(Product.Id);
            Session.Delete<Models.Product>(Product.Id);
            await Session.SaveChangesAsync();
            return new DeleteProductResult(true);
        }
    }
}
