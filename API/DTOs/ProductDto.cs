using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required, MaxLength(4000), MinLength(1)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(10000), MinLength(10)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public uint Amount { get; set; }
        [Required]
        public float Rate { get; set; } = 0;
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public string ProductBrand { get; set; }
    }
}
