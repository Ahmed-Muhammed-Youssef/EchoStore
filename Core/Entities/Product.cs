using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductInfoId { get; set; }
        [Required]
        public ProductInfo ProductInfo { get; set; }
    }
}
