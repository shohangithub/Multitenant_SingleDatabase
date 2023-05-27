﻿using System.Security.Claims;


namespace AuthPermissions.BaseCode.PermissionsCode
{
    /// <summary>
    /// Service to return permission names of the user
    /// </summary>
    public interface IUsersPermissionsService
    {
        /// <summary>
        /// This returns all the permissions in the provided ClaimsPrincipal (or null if no user or permission claim)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns list of permissions in current user, or null if claim not found</returns>
        List<string> PermissionsFromUser(ClaimsPrincipal user);
    }
}
