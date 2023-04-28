using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class Order
    {
        public Order()
        {
        }

        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderedProductInfo> orderItems, decimal subtotal)
        {
            BuyerEmail = buyerEmail ?? throw new ArgumentNullException(nameof(buyerEmail));
            ShipToAddress = shipToAddress ?? throw new ArgumentNullException(nameof(shipToAddress));
            DeliveryMethod = deliveryMethod ?? throw new ArgumentNullException(nameof(deliveryMethod));
            OrderedProductInfo = orderItems ?? throw new ArgumentNullException(nameof(orderItems));
            Subtotal = subtotal;
        }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        // Foreign Keys
        public int DeliveryMethodId { get; set; }

        // Navigation Properties
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderedProductInfo> OrderedProductInfo { get; set; }

        // Utility
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
