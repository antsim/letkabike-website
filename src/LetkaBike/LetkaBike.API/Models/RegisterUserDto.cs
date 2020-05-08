using System.ComponentModel.DataAnnotations;

namespace LetkaBike.API.Models
{
    public class RegisterUserDto
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Email { get; set; }
    }
}