namespace WebApiPlayground.Configuration
{
    public class AuthenticationSettings
    {
        public int JwtExpireDays { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtKey { get; set; }
    }
}