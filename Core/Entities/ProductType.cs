using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200), MinLength(1)]
        public string Name { get; set; }
    }
}