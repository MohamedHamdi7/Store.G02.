using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;

namespace Persistance.Repositories
{

    // To Use InmemoryDatabase We Use Redis To Use it Install Package (StackExchange.Redis) and use Object From IconnectionMultiPlexer to Use Redis)
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
          var redisvalue= await  database.StringGetAsync(id);
            if (redisvalue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisvalue);
            if (basket is null) return null;
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timetolive)
        {
            var redisvalue = JsonSerializer.Serialize(basket);
            var flag = await database.StringSetAsync(basket.Id, redisvalue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(basket.Id) : null;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
          return await database.KeyDeleteAsync(id);
        }

      
    }
}
