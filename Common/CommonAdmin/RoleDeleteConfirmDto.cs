using AuthPermissions.AdminCode;
using Microsoft.EntityFrameworkCore;

namespace Common.CommonAdmin
{
    /// <summary>
    /// This is the standard delete confirm where you need to display what uses are using a Role
    /// </summary>
    public class RoleDeleteConfirmDto
    {
        public string RoleName { get; set; }
        public string ConfirmDelete { get; set; }
        public List<EmailAndUserNameDto> AuthUsers { get; set; }

        public static async Task<RoleDeleteConfirmDto> FormRoleDeleteConfirmDtoAsync(string roleName, IAuthRolesAdminService rolesAdminService)
        {
            var result = new RoleDeleteConfirmDto
            {
                RoleName = roleName,
                AuthUsers = await rolesAdminService.QueryUsersUsingThisRole(roleName)
                    .Select(x => new EmailAndUserNameDto(x.Email, x.UserName))
                    .ToListAsync()
            };

            return result;
        }

    }
}
