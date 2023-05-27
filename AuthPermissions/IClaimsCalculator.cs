﻿using System.Security.Claims;

namespace AuthPermissions
{
    // <summary>
    /// Defines the interface to the code that calcs the AuthP claims
    /// </summary>
    public interface IClaimsCalculator
    {
        /// <summary>
        /// This will return the AuthP claims to be added to the Cookie or JWT token
        /// </summary>
        /// <param name="userId"></param>
        /// That's because the JWT Token uses a claim of type "nameid" to hold the ASP.NET Core user's ID</param>
        /// <returns></returns>
        Task<List<Claim>> GetClaimsForAuthUserAsync(string userId);
    }
}
