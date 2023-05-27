using AuthPermissions;
using AuthPermissions.AdminCode;
using System.Security.Claims;

namespace Multitenant_SingleDatabase.InvoiceCode.Services
{
    /// <summary>
    /// This adds the tenant name as a claim. This speeds up the showing of the tenant name in the display
    /// </summary>
    public class AddTenantNameClaim : IClaimsAdder
    {
        public const string TenantNameClaimType = "TenantName";

        private readonly IAuthUsersAdminService _userAdmin;

        public AddTenantNameClaim(IAuthUsersAdminService userAdmin)
        {
            _userAdmin = userAdmin;
        }

        public async Task<Claim> AddClaimToUserAsync(string userId)
        {
            var user = (await _userAdmin.FindAuthUserByUserIdAsync(userId)).Result;

            return user?.UserTenant?.TenantFullName == null
                ? null
                : new Claim(TenantNameClaimType, user.UserTenant.TenantFullName);
        }

        public static string GetTenantNameFromUser(ClaimsPrincipal user)
        {
            return user?.Claims.FirstOrDefault(x => x.Type == TenantNameClaimType)?.Value;
        }
    }
}
