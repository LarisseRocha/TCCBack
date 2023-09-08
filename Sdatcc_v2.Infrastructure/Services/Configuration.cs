namespace Sdatcc_v2.Infrastructure.Services
{
    public static class Configuration
    {
        public static string JwtKey = "MTM1NzkwQG15and0a2V5";
        public static string ApiKeyName = "api_key";
        public static string ApiKey = "movie_review_MTM1NzkwQG15and0a2V5";
        public static SmtpConfiguration Smtp = new();

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
