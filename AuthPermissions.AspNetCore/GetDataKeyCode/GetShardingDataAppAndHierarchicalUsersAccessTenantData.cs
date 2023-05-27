﻿using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.BaseCode.CommonCode;
using Microsoft.AspNetCore.Http;

namespace AuthPermissions.AspNetCore.GetDataKeyCode
{
    /// <summary>
    /// This service is registered if a multi-tenant setup with sharding on
    /// NOTE: There are other versions if the "Access the data of other tenant" is turned on
    /// </summary>
    public class GetShardingDataAppAndHierarchicalUsersAccessTenantData : IGetShardingDataFromUser
    {
        /// <summary>
        /// This will return the AuthP's DataKey and the connection string via the ConnectionName claim.
        /// This version works with tenant users, but is little bit slower than the version that only works with app users
        /// If no user, or no claim then both parameters will be null
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="connectionService">Service to get the current connection string for the  </param>
        /// <param name="linkService"></param>
        public GetShardingDataAppAndHierarchicalUsersAccessTenantData(IHttpContextAccessor accessor,
            IShardingConnections connectionService,
            ILinkToTenantDataService linkService)
        {
            var overrideLink = linkService.GetShardingDataOfLinkedTenant();

            DataKey = overrideLink.dataKey ?? accessor.HttpContext?.User.GetAuthDataKeyFromUser();

            var databaseDataName = overrideLink.connectionName
                                   ?? accessor.HttpContext?.User.GetDatabaseInfoNameFromUser();

            if (databaseDataName != null)
                ConnectionString = connectionService.FormConnectionString(databaseDataName);
        }

        /// <summary>
        /// The AuthP' DataKey, can be null.
        /// </summary>
        public string DataKey { get; }

        /// <summary>
        /// This contains the connection string to the database to use
        /// If null, then use the default connection string as defined at the time when your application's DbContext was registered
        /// </summary>
        public string ConnectionString { get; }
    }
}
