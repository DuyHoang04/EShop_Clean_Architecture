namespace Catalog.API.Product.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/delete-product/{id}", async (Guid Id, ISender sender) =>
                {
                    var resutl = await sender.Send(new DeleteProductCommand(Id));
                    var res = resutl.Adapt<DeleteProductResponse>();
                    return Results.Ok(res);
                }).WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete a product")
                .WithDescription("Delete product");
        }
    }
}
