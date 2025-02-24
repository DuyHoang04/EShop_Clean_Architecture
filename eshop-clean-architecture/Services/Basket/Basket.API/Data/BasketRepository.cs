namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession Session)
        : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
        {
            Session.Delete<ShoppingCart>(Username);
            await Session.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
        {
            var basket = await Session.LoadAsync<ShoppingCart>(Username, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(Username): basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            Session.Store(basket);
            await Session.SaveChangesAsync(cancellationToken);
            return basket;
        }
    }
}
