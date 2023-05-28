using AuthPermissions.SupportCode.AddUsersServices;

namespace Multitenant_SingleDatabase.PermissionsCode
{
    public class SingleDbCreateTenantVersions
    {

        public static readonly MultiTenantVersionData TenantSetupData = new()
        {
            TenantRolesForEachVersion = new Dictionary<string, List<string>>()
        {
            { "Free", null },
            { "Pro", new List<string> { "Tenant Admin" } },
            { "Enterprise", new List<string> { "Tenant Admin", "Enterprise" } },
        },
            TenantAdminRoles = new Dictionary<string, List<string>>()
        {
            { "Free", new List<string> { "Invoice Reader", "Invoice Creator" } },
            { "Pro", new List<string> { "Invoice Reader", "Invoice Creator", "Tenant Admin" } },
            { "Enterprise", new List<string> { "Invoice Reader", "Invoice Creator", "Tenant Admin" } }
        }
            //No settings for HasOwnDbForEachVersion as this isn't a sharding 
        };

    }
}
