using Core.Entities.OrderAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, Address shippingAddress)
        {
            var cart = await _unitOfWork.CartRepository.GetCart(cartId);
            var dm = await _unitOfWork.DeliveryMethodRepository.GetByIdAsync(deliveryMethodId);
            decimal subtotal = 0;
            if(dm == null)
            {
                throw new Exception("404: Delivery method not found");
            }
            var items = new List<OrderedProductInfo>();
            foreach (var item in cart.Items)
            {
                var productItemInfo = await _unitOfWork.ProductRepository.GetProductInfoByIdAsync(item.Id);
                // check the quantity availability
                if (item.Quantity > productItemInfo.AvailableAmount)
                {
                    throw new Exception($"Not enough available amount. Only available {productItemInfo.AvailableAmount}");
                }
                
                var orderedItem = new OrderedProductInfo(item.Quantity);

                subtotal += productItemInfo.Price * item.Quantity;
                items.Add(orderedItem);
            }
            var order = new Order(buyerEmail, shippingAddress, dm, items, subtotal);

            // Save to db
            order = await _unitOfWork.OrderRepository.CreateOrderAsync(order);
            int result = 0;
            if (_unitOfWork.HasChanges())
            {
                result = await _unitOfWork.Complete();
            }
            if(result <= 0)
            {
                return null;
            }
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.DeliveryMethodRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(id);
            if(order.BuyerEmail == buyerEmail)
            {
                return order;
            }
            return null;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _unitOfWork.OrderRepository.GetCurrentUserOrdersAsync(buyerEmail);
            return orders;
        }
    }
}
