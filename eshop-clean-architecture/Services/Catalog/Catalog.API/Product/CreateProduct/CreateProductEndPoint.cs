namespace Catalog.API.Product.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        List<string> Category,
        string Description,
        string ImageFile,
        string Price);

    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/create-product", async (CreateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var resutl = await sender.Send(command);
                    var res = resutl.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{res.Id}", res);
                }).WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new product")
                .WithDescription("Create a new product");
        }
    }
}