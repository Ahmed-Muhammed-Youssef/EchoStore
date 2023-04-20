using System;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string pictureUrl, string productName)
        {
            ProductItemId = productItemId;
            PictureUrl = pictureUrl ?? throw new ArgumentNullException(nameof(pictureUrl));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
        }

        public int ProductItemId { get; set; }
        public string PictureUrl { get; set; }
        public string ProductName { get; set; }
    }
}
