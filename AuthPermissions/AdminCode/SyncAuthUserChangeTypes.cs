﻿namespace AuthPermissions.AdminCode
{
    /// <summary>
    /// The type of changes between the authentication provider's user and the AuthPermission's AuthUser
    /// Also used to confirm that the change should be made 
    /// </summary>
    public enum SyncAuthUserChangeTypes
    {
        /// <summary>
        /// Ignore this change - can be set by the user
        /// </summary>
        NoChange,
        /// <summary>
        /// A new authentication provider's user, need to add a AuthP user  
        /// </summary>
        Create,
        /// <summary>
        /// The authentication provider user's email and/or username has change, so update AuthP user
        /// </summary>
        Update,
        /// <summary>
        /// A user has been removed from authentication provider' database, so delete AuthP user too
        /// </summary>
        Delete
    }
}
