using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
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


        public async Task<DeliveryMethod> RemoveAsync(int deliveryMethodId)
        {
            var dm = await GetByIdAsync(deliveryMethodId);
            if (dm == null)
            {
                throw new Exception("Not Found");
            }
            var res = _storeContext.DeliveryMethods.Remove(dm);
            _storeContext.SaveChanges();
            return res.Entity;
        }
    }
}
