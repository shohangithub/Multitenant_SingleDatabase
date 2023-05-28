using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.SingleLevelSharding.EfCoreCode;
using NetCore.AutoRegisterDi;

namespace Multitenant.SingleLevelSharding.AppStart
{
    public static class StartupExtensions
    {
        public const string InvoicesDbContextHistoryName = "SingleDb-InvoicesDbContext";

        public static void RegisterSingleDbInvoices(this IServiceCollection services, IConfiguration configuration)
        {
            //Register any services in this project
            services.RegisterAssemblyPublicNonGenericClasses()
                .Where(c => c.Name.EndsWith("Service"))  //optional
                .AsPublicImplementedInterfaces();

            //Register the retail database to the same database used for individual accounts and AuthP database
            services.AddDbContext<InvoicesDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"), dbOptions =>
                dbOptions.MigrationsHistoryTable(InvoicesDbContextHistoryName)));
        }
    }
}
