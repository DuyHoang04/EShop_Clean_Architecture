using Marten.Pagination;

namespace Catalog.API.Product.GetProduct
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;

    public record GetProductResult(IEnumerable<Models.Product> Products);

    internal class GetProductQueryHandler : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        private readonly IDocumentSession _session;
        private readonly ILogger<GetProductQueryHandler> _logger;

        public GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _session.Query<Models.Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
                return new GetProductResult(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products.");
                throw;
            }
        }
    }
}