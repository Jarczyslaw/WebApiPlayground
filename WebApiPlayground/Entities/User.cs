namespace WebApiPlayground.Entities
{
    public class User
    {
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}