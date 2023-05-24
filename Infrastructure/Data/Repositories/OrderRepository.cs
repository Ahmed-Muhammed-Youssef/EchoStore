using Core.Entities.OrderAggregate;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _storeContext;

        public OrderRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _storeContext.Orders.AddAsync(order);
            foreach (var item in order.OrderedProductInfo)
            {
                item.OrderId = order.Id;
                item.Order = order;
                await OrderProduct(order, item);
            }
            return order;
        }

        private async Task<OrderedProductInfo> OrderProduct(Order order, OrderedProductInfo orderedProductInfo)
        {
            if (orderedProductInfo.ProductInfo.AvailableAmount < orderedProductInfo.Quantity)
            {
                throw new Exception($"the available quantity of this product is less than {orderedProductInfo.Quantity}");
            }

            await _storeContext.OrderedProductInfo.AddAsync(orderedProductInfo);
            return orderedProductInfo;
        }

        public void DeleteOrder(Order order)
        {
            var r = _storeContext.Orders.Remove(order);
        }

        public async Task<IReadOnlyList<Order>> GetCurrentUserOrdersAsync(string userEmail)
        {
            return await _storeContext.Orders
                .AsNoTracking()
                .Where(o => o.BuyerEmail == userEmail)
                .Include(o => o.OrderedProductInfo)
                .Include(o => o.DeliveryMethod)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _storeContext.Orders
                .AsNoTracking()
                .Include(o => o.OrderedProductInfo)
                .Include(o => o.DeliveryMethod)
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public void UpdateOrder(Order order)
        {
            var r = _storeContext.Entry(order).State = EntityState.Modified; ;
        }

        public async Task<Order> GetOrderByPaymentIntent(string paymentIntentId)
        {
            return await _storeContext.Orders.FirstOrDefaultAsync(o => o.PaymentIntentId == paymentIntentId);
        }
    }
}
