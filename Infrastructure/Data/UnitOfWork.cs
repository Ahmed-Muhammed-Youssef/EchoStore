using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Data.Repositories;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private readonly IConnectionMultiplexer _redis;

        public ICartRepository CartRepository => new CartRepository(_redis);
        public IDeliveryMethodRepository DeliveryMethodRepository => new DeliveryMethodRepository(_storeContext);
        public IOrderRepository OrderRepository => new OrderRepository(_storeContext);
        public IProductRepository ProductRepository => new ProductRepository(_storeContext);
        public IBrandRepository BrandRepository => new BrandRepository(_storeContext);
        public IProductTypeRepository ProductTypeRepository => new ProductTypeRepository(_storeContext);


        public UnitOfWork(StoreContext storeContext, IConnectionMultiplexer redis)
        {
            _storeContext = storeContext;
            _redis = redis;
        }
        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _storeContext.Dispose();
        }
        public bool HasChanges()
        {
            return _storeContext.ChangeTracker.HasChanges();
        }
    }
}
