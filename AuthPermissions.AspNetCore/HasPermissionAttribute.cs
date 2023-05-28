﻿using AuthPermissions.BaseCode.CommonCode;
using Microsoft.AspNetCore.Authorization;

namespace AuthPermissions.AspNetCore
{
    /// <summary>
    /// This attribute can be applied in the same places as the [Authorize] would go
    /// This will only allow users which has a role containing the enum Permission passed in 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// This creates an HasPermission attribute with a Permission enum member
        /// </summary>
        /// <param name="permission"></param>
        public HasPermissionAttribute(object permission) : base(permission?.ToString()!)
        {
            if (permission == null) throw new ArgumentNullException(nameof(permission));

            permission.GetType().ThrowExceptionIfEnumIsNotCorrect();
        }
    }


}
