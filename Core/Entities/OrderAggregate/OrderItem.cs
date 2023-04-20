using System;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(int id, decimal price, ProductItemOrdered itemOrdered, int quantity)
        {
            Id = id;
            Price = price;
            ItemOrdered = itemOrdered ?? throw new ArgumentNullException(nameof(itemOrdered));
            Quantity = quantity;
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Quantity { get; set; }
    }
}
