using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IReadOnlyList<Order>> GetCurrentUserOrdersAsync (string userEmail);
        public Task<Order> GetOrderAsync (int orderId);
        public Task<Order> CreateOrderAsync(Order order);
        public Task<Order> UpdateOrderAsync(Order order);
        public Task<Order> DeleteOrderAsync(Order order);
    }
}
