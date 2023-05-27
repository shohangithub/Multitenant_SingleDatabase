using AuthPermissions.BaseCode.SetupCode;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BulkLoadServices
{
    /// <summary>
    /// Bulk load many Roles with their permissions
    /// </summary>
    public interface IBulkLoadRolesService
    {
        /// <summary>
        /// This allows you to add Roles with their permissions via the <see cref="BulkLoadRolesDto"/> class
        /// </summary>
        /// <param name="roleSetupData">A list of definitions containing the information for each Role</param>
        /// <returns>status</returns>
        Task<IStatusGeneric> AddRolesToDatabaseAsync(List<BulkLoadRolesDto> roleSetupData);
    }
}
