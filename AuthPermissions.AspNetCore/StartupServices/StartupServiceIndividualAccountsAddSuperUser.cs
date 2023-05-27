﻿using Microsoft.AspNetCore.Identity;
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
    /// This is a complex method that can handle a individual account user with a 
    /// personalized IdentityUser type
    /// </summary>
    public class StartupServiceIndividualAccountsAddSuperUser<TIdentityUser> : IStartupServiceToRunSequentially
        where TIdentityUser : IdentityUser, new()
    {
        /// <summary>
        /// This must be after migrations, and after the adding demo users startup service.
        /// </summary>
        public int OrderNum { get; } = -1;

        /// <summary>
        /// This will ensure that a user who's email/password is held in the "SuperAdmin" section of 
        /// the appsettings.json file is in the individual users account authentication database
        /// </summary>
        /// <param name="scopedServices">This should be a scoped service</param>
        /// <returns></returns>
        public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
        {
            var userManager = scopedServices.GetRequiredService<UserManager<TIdentityUser>>();

            var (email, password) = scopedServices.GetSuperUserConfigData();
            if (!string.IsNullOrEmpty(email))
                await CheckAddNewUserAsync(userManager, email, password);
        }

        /// <summary>
        /// This will add a user with the given email if they don't all ready exist
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static async Task CheckAddNewUserAsync(UserManager<TIdentityUser> userManager, string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return;

            user = new TIdentityUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
                throw new InvalidOperationException(
                    $"Tried to add user {email}, but failed. Errors:\n {errorDescriptions}");
            }
        }
    }
}
