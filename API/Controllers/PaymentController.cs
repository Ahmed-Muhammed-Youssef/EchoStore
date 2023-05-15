using API.DTOs;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Stripe;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly Mapper _mapper;
        private readonly ILogger<IPaymentService> _logger;
        private const string WebhookSecret = "  ";

        public PaymentController(IPaymentService paymentService, Mapper mapper, ILogger<IPaymentService> logger)
        {
            this._paymentService = paymentService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [Authorize]
        // POST api/payment/{orderId}
        [HttpPost("{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> CreatePaymentIntent(int orderId)
        {
            var order = await _paymentService.CreatePaymentIntent(orderId);
            if(order == null)
            {
                return BadRequest("Invalid order Id");
            }
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        // POST api/payment/webhook
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripEvent = EventUtility
                .ConstructEvent(json, Request.Headers["Stripe-Signature"], WebhookSecret);
            PaymentIntent intent;
            Order order;
            switch (stripEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent) stripEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order Updated:", order);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripEvent.Data.Object;
                    _logger.LogInformation("Payment Failed:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order Updated:", order);
                    break;
               
            }
            return new EmptyResult();
        }
       
    }
}
