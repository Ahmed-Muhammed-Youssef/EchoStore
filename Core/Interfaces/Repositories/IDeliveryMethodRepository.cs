using Core.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IDeliveryMethodRepository
    {
        // POST
        public Task<DeliveryMethod> AddAsync(DeliveryMethod delivery);

        // GET
        public Task<DeliveryMethod> GetByIdAsync(int id);
        public Task<IReadOnlyList<DeliveryMethod>> GetAllAsync();

        // DELETE
        public void RemoveAsync(DeliveryMethod deliveryMethod);

        public Task<bool> AnyAsync();
    }
}
