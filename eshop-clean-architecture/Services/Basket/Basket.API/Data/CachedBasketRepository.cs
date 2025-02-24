namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string Username, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(Username, cancellationToken);
            await cache.RemoveAsync(Username, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string Username, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(Username, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await repository.GetBasket(Username, cancellationToken);
            await cache.SetStringAsync(Username, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
