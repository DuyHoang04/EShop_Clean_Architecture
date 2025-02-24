namespace Basket.API.Exceptions
{
    public class BasketNotFoundException: NotFoundException
    {
        public BasketNotFoundException(string Username) : base("Basket", Username)
        {

        }
    }
}
