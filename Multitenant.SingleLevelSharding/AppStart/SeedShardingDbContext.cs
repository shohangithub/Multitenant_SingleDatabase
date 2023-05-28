using AuthPermissions.BaseCode.DataLayer.Classes;
using Multitenant.SingleLevelSharding.EfCoreClasses;
using Multitenant.SingleLevelSharding.EfCoreCode;
using AuthPermissions.BaseCode.CommonCode;

namespace Multitenant.SingleLevelSharding.AppStart
{
    public class SeedShardingDbContext
    {
        private readonly ShardingSingleDbContext _context;

        public SeedShardingDbContext(ShardingSingleDbContext context)
        {
            _context = context;
        }

        public async Task SeedInvoicesForAllTenantsAsync(IEnumerable<Tenant> authTenants)
        {
            foreach (var authTenant in authTenants)
            {

                var company = new CompanyTenant
                {
                    AuthPTenantId = authTenant.TenantId,
                    CompanyName = authTenant.TenantFullName,
                    DataKey = authTenant.GetTenantDataKey(),
                };
                _context.Add(company);
                var invoiceBuilder = new ExampleInvoiceBuilder(authTenant.GetTenantDataKey());

                for (int i = 0; i < 5; i++)
                {
                    var invoice = invoiceBuilder.CreateRandomInvoice(authTenant.TenantFullName);
                    _context.Add(invoice);
                }
            }

            await _context.SaveChangesAsync();
        }
    }

}
