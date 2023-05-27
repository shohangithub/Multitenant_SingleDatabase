﻿using AuthPermissions.BaseCode.CommonCode;
using Microsoft.AspNetCore.Http;

namespace AuthPermissions.AspNetCore.GetDataKeyCode
{
    /// <summary>
    /// This service is registered if a multi-tenant setup without sharding
    /// NOTE: There are other version if the "Access the data of other tenant" is turned on
    /// </summary>
    public class GetDataKeyFromUserNormal : IGetDataKeyFromUser
    {
        /// <summary>
        /// This will return the AuthP' DataKey claim. If no user, or no claim then returns null
        /// </summary>
        /// <param name="accessor"></param>
        public GetDataKeyFromUserNormal(IHttpContextAccessor accessor)
        {
            DataKey = accessor.HttpContext?.User.GetAuthDataKeyFromUser();
        }

        /// <summary>
        /// The AuthP' DataKey, can be null.
        /// </summary>
        public string DataKey { get; }
    }
}
