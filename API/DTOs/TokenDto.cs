using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class TokenDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
