using AuthPermissions.BaseCode;
using Microsoft.Extensions.DependencyInjection;

namespace AuthPermissions
{
    public class AuthSetupData
    {
        internal AuthSetupData(IServiceCollection services, AuthPermissionsOptions options) {
            Services = services;
            Options = options;
        }

        /// <summary>
        /// The DI ServiceCollection which AuthPermissions services, constants and policies are registered to
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// This holds the AuthPermissions options
        /// </summary>
        public AuthPermissionsOptions Options { get; }
    }
}
