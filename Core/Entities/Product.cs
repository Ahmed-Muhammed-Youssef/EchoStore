using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(4000), MinLength(1)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(6000), MinLength(1)]
        public string DescriptionSummary { get; set; } = string.Empty;
        [Required, MaxLength(10000), MinLength(1)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public uint Amount { get; set; }
        [Required]
        public float Rate { get; set; }

    }
}