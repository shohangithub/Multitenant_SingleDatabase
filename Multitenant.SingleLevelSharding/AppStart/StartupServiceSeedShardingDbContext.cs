using AuthPermissions.AdminCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.SingleLevelSharding.EfCoreCode;
using RunMethodsSequentially;

namespace Multitenant.SingleLevelSharding.AppStart
{
    /// <summary>
    /// If there are no RetailOutlets in the RetailDbContext it seeds the RetailDbContext with RetailOutlets and gives each of them some stock
    /// </summary>

    public class StartupServiceSeedShardingDbContext : IStartupServiceToRunSequentially
    {
        public int OrderNum { get; } //runs after migration of the ShardingSingleDbContext

        public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
        {
            var context = scopedServices.GetRequiredService<ShardingSingleDbContext>();
            var numInvoices = await context.Invoices.IgnoreQueryFilters().CountAsync();
            if (numInvoices == 0)
            {
                var authTenantAdmin = scopedServices.GetRequiredService<IAuthTenantAdminService>();

                var seeder = new SeedShardingDbContext(context);
                await seeder.SeedInvoicesForAllTenantsAsync(authTenantAdmin.QueryTenants().ToArray());
            }
        }
    }


}
