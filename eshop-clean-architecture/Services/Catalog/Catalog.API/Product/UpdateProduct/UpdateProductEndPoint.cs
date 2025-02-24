namespace Catalog.API.Product.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        string Price);

    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/update-product", async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();
                    var resutl = await sender.Send(command);
                    var res = resutl.Adapt<UpdateProductResponse>();
                    return Results.Ok(res);
                }).WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update a product")
                .WithDescription("Update product");
        }
    }
}
