using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;

namespace Asp.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderedProductInfoDto> OrderedProductInfo { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
