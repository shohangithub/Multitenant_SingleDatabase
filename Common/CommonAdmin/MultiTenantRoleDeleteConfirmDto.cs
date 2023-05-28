using AuthPermissions.AdminCode;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonAdmin
{
    public class MultiTenantRoleDeleteConfirmDto
    {
        public string RoleName { get; set; }
        public string ConfirmDelete { get; set; }
        public List<UserOrTenantDto> UsedBy { get; set; }

        public static async Task<MultiTenantRoleDeleteConfirmDto> FormRoleDeleteConfirmDtoAsync(string roleName, IAuthRolesAdminService rolesAdminService)
        {
            var result = new MultiTenantRoleDeleteConfirmDto
            {
                RoleName = roleName
            };
            result.UsedBy = (await rolesAdminService.QueryUsersUsingThisRole(roleName)
                    .Select(x => new { x.Email, x.UserName })
                    .ToListAsync())
                .Select(x => new UserOrTenantDto(true, x.UserName ?? x.Email))
                .ToList();
            result.UsedBy.AddRange(await rolesAdminService.QueryTenantsUsingThisRole(roleName)
                .Select(x => new UserOrTenantDto(false, x.TenantFullName))
                .ToListAsync());

            return result;
        }

    }
}
