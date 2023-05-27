﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RunMethodsSequentially;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.AspNetCore.StartupServices
{
    /// <summary>
    /// This will run EF Core's Migrate method on the given DbContext
    /// Note that if the database is an in-memory, then it will simply create it
    /// </summary>
    public class StartupServiceMigrateAnyDbContext<TContext> : IStartupServiceToRunSequentially
        where TContext : DbContext
    {
        /// <summary>
        /// Set to -10 so that it is run before any other startup services
        /// </summary>
        public int OrderNum { get; } = -10; //These must be run before any other startup services

        /// <summary>
        /// This migrates the given <typeparamref name="TContext"/> DbContext
        /// </summary>
        /// <param name="scopedServices">This should be a scoped service</param>
        /// <returns></returns>
        public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
        {
            var context = scopedServices.GetRequiredService<TContext>();

            if (context.Database.IsInMemory())
                await context.Database.EnsureCreatedAsync();
            else
                await context.Database.MigrateAsync();
        }
    }
}
