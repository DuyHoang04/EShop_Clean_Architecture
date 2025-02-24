namespace Catalog.API.Product.GetProductById
{
    public record GteProductByIdResponse(Models.Product Product);
    public class GetProductByIdEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/{id}", async (Guid Id, ISender Sender) =>
               {
                   var result = await Sender.Send(new GetProductByIdQuery(Id));
                   var response = result.Adapt<GetProductByIdResponse>();
                   return Results.Ok(response);
               }).WithName("GetProductById")
                .Produces<GteProductByIdResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("get product by Id")
                .WithDescription("get product by Id");
        }
    }
}
