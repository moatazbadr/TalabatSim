using StackExchange.Redis;
using System.Text.Json;
using Talabat.Domain.Entities;
using Talabat.Domain.Repositories;

namespace Talabat.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _redisDatabase;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _redisDatabase = redis.GetDatabase();
    }
    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        //impelement the delete functionality using the redis database
        var deleted = await _redisDatabase.KeyDeleteAsync(basketId);
        return deleted;
    }

    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var Basket = await _redisDatabase.StringGetAsync(basketId);
        if (!Basket.IsNull)
        {
            var basket = JsonSerializer.Deserialize<CustomerBasket>(Basket);
            return basket;
        }
        return null;
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _redisDatabase.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
        if (!created)
            return null;
        return await GetBasketAsync(basket.Id);

    }
}
