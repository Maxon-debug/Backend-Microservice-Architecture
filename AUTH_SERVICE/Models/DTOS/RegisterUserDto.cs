using System.ComponentModel.DataAnnotations;

namespace AUTH_SERVICE.Models.DTOS
{
    public class RegisterUserDto
    {

        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Role { get; set; } = "User";
    }
}
