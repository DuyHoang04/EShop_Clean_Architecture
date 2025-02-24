namespace Catalog.API.Product.GetProductById
{
    public  record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResponse>;

    public record GetProductByIdResponse(Models.Product Product);

    public class GetProductByIdHandler(IDocumentSession Session, ILogger<GetProductByIdHandler> Logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await Session.LoadAsync<Models.Product>(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(product.Id);
            }

            return new GetProductByIdResponse(product);
        }
    }
}
