using Core.Entities;
using Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private const double NumberOfDaysToExpire = 30;

        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCart(Cart cart)
        {
            return await _database.KeyDeleteAsync(cart.Id);
        }

        public async Task<Cart> GetCart(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data);
        }

        public async Task<bool> CreateUpdateCart(Cart cart)
        {
            return await _database.StringSetAsync(cart.Id,
                JsonSerializer.Serialize(cart),
                TimeSpan.FromDays(NumberOfDaysToExpire));
        }
    }
}
