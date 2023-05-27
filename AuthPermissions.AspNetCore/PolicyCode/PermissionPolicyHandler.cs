using AuthPermissions.BaseCode.PermissionsCode;
using AuthPermissions.BaseCode;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.AspNetCore.PolicyCode
{
    /// <summary>
    /// This defines the policy handler for the <see cref="PermissionRequirement"/> which the AuthP defined
    /// </summary>
    public class PermissionPolicyHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly Type _enumPermissionType;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public PermissionPolicyHandler(AuthPermissionsOptions options)
        {
            _enumPermissionType = options.InternalData.EnumPermissionsType;
        }

        /// <summary>
        /// This allows a user to access a method with a HasPermission attribute if that have the correct Permission 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var permissionsClaim =
                context.User.Claims.SingleOrDefault(c => c.Type == PermissionConstants.PackedPermissionClaimType);
            // If user does not have the scope claim, get out of here
            if (permissionsClaim == null)
                return Task.CompletedTask;

            if (_enumPermissionType.ThisPermissionIsAllowed(permissionsClaim.Value, requirement.PermissionName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
