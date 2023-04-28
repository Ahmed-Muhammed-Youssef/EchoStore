using Core.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        // POST
        public Task<DeliveryMethod> AddAsync(DeliveryMethod delivery);

        // GET
        public Task<DeliveryMethod> GetByIdAsync(int id);
        public Task<IReadOnlyList<DeliveryMethod>> GetAllAsync();

        // DELETE
        public Task<DeliveryMethod> RemoveAsync(int deliveryMethodId);

    }
}
