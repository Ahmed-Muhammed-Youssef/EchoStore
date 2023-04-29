using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OrderedProductInfoDto
    {
        public int Id { get; set; }
        public int ProductInfoId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
    }
}