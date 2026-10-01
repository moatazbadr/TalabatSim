using Talabat.Domain.Entities;

namespace Talabat.Domain.Repositories;

public interface IBasketRepository
{

    //functions :
    //1-Update the Basket [Create]
    //2-Delete the Basket
    //3-Get the Basket

    Task<CustomerBasket ?> GetBasketAsync(string basketId);

    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

    Task<bool> DeleteBasketAsync(string basketId);

}
