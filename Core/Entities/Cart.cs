using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Cart
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
