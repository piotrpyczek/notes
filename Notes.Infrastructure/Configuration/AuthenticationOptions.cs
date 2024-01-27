namespace Notes.Infrastructure.Configuration
{
    public class AuthenticationOptions
    {
        public static string Section { get; } = "Authentication";

        public string JwtKey { get; set; }
    }
}
