using Microsoft.AspNetCore.Authorization;

namespace AuthPermissions.AspNetCore.PolicyCode
{
    /// <summary>
    /// This is the policy requirement
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="permissionName"></param>
        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }

        /// <summary>
        /// The Permission name to look for
        /// </summary>
        public string PermissionName { get; }
    }
}
