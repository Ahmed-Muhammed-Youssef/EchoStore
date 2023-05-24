using Core.Entities.OrderAggregate;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly StoreContext _storeContext;

        public DeliveryMethodRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<DeliveryMethod> AddAsync(DeliveryMethod delivery)
        {
            var res = await _storeContext.DeliveryMethods.AddAsync(delivery);
            return res.Entity;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllAsync()
        {
            return await _storeContext
               .DeliveryMethods
               .AsNoTracking()
               .ToListAsync();
        }

        public async Task<DeliveryMethod> GetByIdAsync(int id)
        {
            return await _storeContext.DeliveryMethods.FindAsync(id);
        }

        public void RemoveAsync(DeliveryMethod deliveryMethod)
        {
            if (deliveryMethod is null)
            {
                throw new ArgumentNullException(nameof(deliveryMethod));
            }

            var res = _storeContext.DeliveryMethods.Remove(deliveryMethod);
        }

        public async Task<bool> AnyAsync()
        {
            return await _storeContext.DeliveryMethods.AnyAsync();
        }
    }
}
