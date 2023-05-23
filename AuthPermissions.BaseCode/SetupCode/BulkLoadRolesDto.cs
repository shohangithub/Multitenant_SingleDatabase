using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.SetupCode
{

    /// <summary>
    /// This class is used for bulk loading of AuthP's Roles
    /// </summary>
    public class BulkLoadRolesDto
    {
        /// <summary>
        /// Define a Role and the permissions in the Role: must be unique and not null
        /// </summary>
        /// <param name="roleName">Name of the Role: must be unique</param>
        /// <param name="description">Human-friendly description of what the Role provides. Can be null</param>
        /// <param name="permissionsCommaDelimited">A list of the names of the `Permissions` in this Role</param>
        /// <param name="roleType">Optional: Only used if the Role is linked to a tenant</param>
        public BulkLoadRolesDto(string roleName, string description, string permissionsCommaDelimited, RoleTypes? roleType = null)
        {
            RoleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
            Description = description;
            RoleType = roleType ?? RoleTypes.Normal;
            PermissionsCommaDelimited = permissionsCommaDelimited;
        }

        /// <summary>
        /// Name of the Role: must be unique and not null
        /// </summary>
        public string RoleName { get; }
        /// <summary>
        /// Human-friendly description of what the Role provides. Can be null
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// The Type of the Role. This is only used in multi-tenant applications 
        /// </summary>
        public RoleTypes RoleType { get; }

        /// <summary>
        /// A list of the names of the `Permissions` in this Role
        /// The names come from your Permissions enum members
        /// </summary>
        public string PermissionsCommaDelimited { get; }

        /// <summary>
        /// Useful for debugging
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{nameof(RoleName)}: {RoleName}, {nameof(Description)}: {Description}, {nameof(RoleType)}: {RoleType}, {nameof(PermissionsCommaDelimited)}: {PermissionsCommaDelimited}";
        }
    }
}
