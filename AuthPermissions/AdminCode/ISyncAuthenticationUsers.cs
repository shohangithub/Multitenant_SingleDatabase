using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.AdminCode
{
    /// <summary>
    /// This interface defines the service that will return the UserId, Email and Name of all
    /// the active users in your authentication provider's.  This is used to synchronize the
    /// AuthUsers in the AuthPermissions database to the authentication provider's users
    /// </summary>
    public interface ISyncAuthenticationUsers
    {
        /// <summary>
        /// This should provide all the active users that need AuthPermissions role/permissions and/or multi-tenant features
        /// </summary>
        /// <returns>A list <see cref="SyncAuthenticationUser"/> classes containing the UserId, Email and Name for all users</returns>
        public Task<IEnumerable<SyncAuthenticationUser>> GetAllActiveUserInfoAsync();
    }
}
