using Marten.Linq.QueryHandlers;

namespace Catalog.API.Product.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Catagory) : IQuery<GetProductByCategoryResponse>;
    public record GetProductByCategoryResponse(IEnumerable<Models.Product> Products);

    internal class GetProductByCategoryHandler(IDocumentSession Session, ILogger<GetProductByCategoryHandler> Logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
    {
        public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await Session.Query<Models.Product>().Where(x => x.Category.Contains(request.Catagory))
                .ToListAsync();
            return new GetProductByCategoryResponse(products);
        }
    }
}
