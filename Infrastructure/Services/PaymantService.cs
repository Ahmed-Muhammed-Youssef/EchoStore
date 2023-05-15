using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if(!string.IsNullOrEmpty(order.PaymentIntentId))
            {
                throw new ArgumentException("the order does already have stripe intent");

            }
            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)(order.OrderedProductInfo
                    .Sum(p => p.Quantity * p.ProductInfo.Price)
                    + (order.DeliveryMethod.Price) * 100),
                Currency = "usd",
                PaymentMethodTypes = new List<string>() {"card"}
            };
            intent = await service.CreateAsync(options);
            order.PaymentIntentId = intent.Id;
            order.ClientSecret = intent.ClientSecret;
            _unitOfWork.OrderRepository.UpdateOrder(order);
            var res = await _unitOfWork.Complete();
            return order;
        }
    }
}
