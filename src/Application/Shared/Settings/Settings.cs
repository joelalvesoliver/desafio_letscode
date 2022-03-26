
using Microsoft.Extensions.Configuration;

namespace Lets.Code.Application.Shared
{
    public static class Settings
    {
        public static string GetLogin(this IConfiguration config) { return config.GetSection($"Login").Value; }
        public static string GetPassword(this IConfiguration config) { return config.GetSection($"Password").Value; }
        public static string GetSecret(this IConfiguration config) { return config.GetSection($"SecretJWT").Value; }
    }
}
