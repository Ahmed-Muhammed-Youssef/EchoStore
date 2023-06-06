namespace Asp.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }
}
