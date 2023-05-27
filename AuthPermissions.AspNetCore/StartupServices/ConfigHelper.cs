using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthPermissions.AspNetCore.StartupServices
{
    internal static class ConfigHelper
    {
        public static (string email, string password) GetSuperUserConfigData(this IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var superSection = config.GetSection("SuperAdmin");
            if (superSection == null)
                return (null, null);

            return (superSection["Email"], superSection["Password"]);
        }
    }
}
