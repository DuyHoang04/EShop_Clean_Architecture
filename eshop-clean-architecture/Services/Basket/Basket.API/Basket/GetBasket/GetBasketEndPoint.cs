using Carter;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart Cart);


    public class GetBasketEndPoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{UserName}", async (string UserName, ISender Sender) =>
                {
                    var result = await Sender.Send(new GetBasketQuery(UserName));
                    var response = result.Adapt<GetBasketResponse>();
                    return Results.Ok(response);

                }).WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("get basket")
                .WithDescription("Get basket");
        }
    }
}
