namespace WebApiPlayground.Models.Dtos
{
    public class RegisterUserDto
    {
        public string ConfirmPassword { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Nationality { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; } = 1;
    }
}