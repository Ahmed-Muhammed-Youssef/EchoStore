using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductBrand
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100), MinLength(1)]
        public string Name { get; set; }
    }
}