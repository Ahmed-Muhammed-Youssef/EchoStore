using System;

namespace Core.Entities.OrderAggregate
{
    public class OrderedProductInfo
    {
        public OrderedProductInfo()
        {
        }

        public OrderedProductInfo(int quantity)
        {
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        // Foreign Keys
        public int ProductInfoId { get; set; }
        public int OrderId { get; set; }

        // Navigation Properties
        public ProductInfo ProductInfo { get; set; }
        public Order Order { get; set; }
    }
}
