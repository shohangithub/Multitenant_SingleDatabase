﻿namespace AuthPermissions.AdminCode
{
    /// <summary>
    /// This class contains the information on a user from your authentication provider - see <see cref="ISyncAuthenticationUsers"/>
    /// </summary>
    public class SyncAuthenticationUser
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        public SyncAuthenticationUser(string userId, string email, string userName)
        {
            UserId = userId;
            Email = email;
            UserName = userName;
        }

        /// <summary>
        /// The userId of the user
        /// </summary>
        public string UserId { get; private set; }
        /// <summary>
        /// The user's main email (used as one way to find the user) 
        /// </summary>
        public string Email { get; private set; }
        /// <summary>
        /// The user's name
        /// </summary>
        public string UserName { get; private set; }
    }
}
