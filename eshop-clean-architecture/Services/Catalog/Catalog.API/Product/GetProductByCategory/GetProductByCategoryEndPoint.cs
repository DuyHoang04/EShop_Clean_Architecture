
namespace Catalog.API.Product.GetProductByCategory
{
    public record getProductByCategoryResponse(IEnumerable<Models.Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender Sender) =>
                {
                    var result = await Sender.Send(new GetProductByCategoryQuery(category));
                    var response = result.Adapt<getProductByCategoryResponse>();
                    return Results.Ok(response);
                }).WithName("GetProductByCategory")
                .Produces<getProductByCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("get product by Category")
                .WithDescription("get product by Category");
        }
    }
}
