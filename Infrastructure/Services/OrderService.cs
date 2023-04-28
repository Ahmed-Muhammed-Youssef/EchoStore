using Core.Entities.OrderAggregate;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;

        public OrderService(ICartRepository cartRepository, IProductRepository productRepository, IOrderRepository orderRepository, IDeliveryMethodRepository deliveryMethodRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _deliveryMethodRepository = deliveryMethodRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, Address shippingAddress)
        {
            var cart = await _cartRepository.GetCart(cartId);
            var dm = await _deliveryMethodRepository.GetByIdAsync(deliveryMethodId);
            if(dm == null)
            {
                throw new Exception("404: Delivery method not found");
            }
            var items = new List<OrderedProductInfo>();
            decimal subtotal = 0;
            foreach (var item in cart.Items)
            {
                var productItemInfo = await _productRepository.GetProductInfoByIdAsync(item.Id);
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
            return await _orderRepository.CreateOrderAsync(order);
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(string cartId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
