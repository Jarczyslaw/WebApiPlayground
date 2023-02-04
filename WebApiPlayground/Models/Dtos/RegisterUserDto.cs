using System.ComponentModel.DataAnnotations;

namespace WebApiPlayground.Models.Dtos
{
    public class RegisterUserDto
    {
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        public string Nationality { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public int RoleId { get; set; } = 1;
    }
}