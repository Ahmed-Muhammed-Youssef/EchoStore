using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        public Task<IReadOnlyList<Order>> GetCurrentUserOrdersAsync(string userEmail);
        public Task<Order> GetOrderAsync(int orderId);
        Task<Order> GetOrderByPaymentIntent(string paymentIntentId);
        public Task<Order> CreateOrderAsync(Order order);
        public void UpdateOrder(Order order);
        public void DeleteOrder(Order order);
    }
}
