using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PaymantService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymantService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }
        public async Task<Order> CreatePaymentIntent(int orderId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var order = await _unitOfWork.OrderRepository.GetOrderAsync(orderId);

            // checks if the order exists
            if(order == null)
            {
                return null;
            }
            // checks if it has an intent
            if(!string.IsNullOrEmpty(order.PaymentIntentId))
            {
                return null;
            }

            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)(order.OrderedProductInfo
                    .Sum(p => p.Quantity * p.ProductInfo.Price)
                    + (order.DeliveryMethod.Price) * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string>() {"card"}
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);
            order.PaymentIntentId = intent.Id;
            order.ClientSecret = intent.ClientSecret;
            _unitOfWork.OrderRepository.UpdateOrder(order);
            var res = await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string intentId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByPaymentIntent(intentId);
            if (order == null) { return null; }
            order.Status = OrderStatus.PaymantFailed;
            _unitOfWork.OrderRepository.UpdateOrder(order);
            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string intentId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByPaymentIntent(intentId);
            if(order == null) { return null;}
            order.Status = OrderStatus.PaymentRecevied; 
            _unitOfWork.OrderRepository.UpdateOrder(order);
            await _unitOfWork.Complete();
            return order;
        }
    }
}
