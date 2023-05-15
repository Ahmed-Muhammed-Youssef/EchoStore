using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public uint AvailableAmount { get; set; }
        public float Rate { get; set; } = 0;
        public string PictureUrl { get; set; }

        // Forigen Keys
        public int ProductTypeId { get; set; } // many to one relationship 
        public int ProductBrandId { get; set; } // many to one relationship 

        // Navigation Properties
        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}