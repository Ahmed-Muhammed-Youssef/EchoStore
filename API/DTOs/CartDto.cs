using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp.DTOs
{
    public class CartDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
