
using Basket.API.Basket.StoreBasket;
using Basket.API.Data;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketHandler(IBasketRepository Repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await Repository.DeleteBasket(request.UserName);
            return new DeleteBasketResult(true);
        }
    }
}
