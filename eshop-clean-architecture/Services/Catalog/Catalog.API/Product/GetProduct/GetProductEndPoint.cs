
namespace Catalog.API.Product.GetProduct
{
    public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductResponse(IEnumerable<Models.Product> Products);
    public class GetProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async([AsParameters] GetProductRequest request, ISender Sender) =>
                {
                    var query = request.Adapt<GetProductsQuery>();
                var result = await Sender.Send(query); 
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            }).WithName("GetProduct")
            .Produces<GetProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("get product")
            .WithDescription("get product");
        }
    }
}
