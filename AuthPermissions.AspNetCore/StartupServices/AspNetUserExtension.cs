﻿using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Test")]
namespace AuthPermissions.AspNetCore.StartupServices
{

    internal static class AspNetUserExtension
    {
        /// <summary>
        /// This will add a user with the given email if they don't all ready exist
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<IdentityUser> CheckAddNewUserAsync(this UserManager<IdentityUser> userManager,
            string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                return user;
            user = new IdentityUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
                throw new InvalidOperationException(
                    $"Tried to add user {email}, but failed. Errors:\n {errorDescriptions}");
            }

            return user;
        }
    }

}
