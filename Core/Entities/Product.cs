namespace Core.Entities
{
    public class Product
    {
        // PK
        public int Id { get; set; }

        // FKs
        public int ProductInfoId { get; set; }

        // Navigation Properties
        public ProductInfo ProductInfo { get; set; }
    }
}
