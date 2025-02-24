namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSucess);
    public class DeleteBasketEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/delet-basket/{UserName}", async (string UserName, ISender Sender) =>
                {
                    var result = await Sender.Send(new DeleteBasketCommand(UserName));
                    var response = result.Adapt<DeleteBasketResponse>();
                    return Results.Ok(response);

                }).WithName("DeleteStoreBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Store Basket")
                .WithDescription("Delete Store Basket");
        }
    }
}
