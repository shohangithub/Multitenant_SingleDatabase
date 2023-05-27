﻿using AuthPermissions.AdminCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Multitenant_SingleDatabase.InvoiceCode.EfCoreCode;
using RunMethodsSequentially;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitenant_SingleDatabase.InvoiceCode.AppStart
{
    /// <summary>
    /// If there are no RetailOutlets in the RetailDbContext it seeds the RetailDbContext with RetailOutlets and gives each of them some stock
    /// </summary>

    public class StartupServiceSeedInvoiceDbContext : IStartupServiceToRunSequentially
    {
        public int OrderNum { get; } //runs after migration of the InvoicesDbContext

        public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
        {
            var context = scopedServices.GetRequiredService<InvoicesDbContext>();
            var numInvoices = await context.Invoices.IgnoreQueryFilters().CountAsync();
            if (numInvoices == 0)
            {
                var authTenantAdmin = scopedServices.GetRequiredService<IAuthTenantAdminService>();

                var seeder = new SeedInvoiceDbContext(context);
                await seeder.SeedInvoicesForAllTenantsAsync(authTenantAdmin.QueryTenants().ToArray());
            }
        }

    }

}