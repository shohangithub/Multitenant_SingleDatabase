﻿namespace AuthPermissions.BaseCode.SetupCode
{
    /// <summary>
    /// Used to check what form of authorization you are using.
    /// </summary>
    public enum AuthPAuthenticationTypes
    {
        /// <summary>
        /// This is the default - AuthPermissions will throw an exception to say you must define the Authentication Type
        /// </summary>
        NotSet,
        /// <summary>
        /// This says you are using IndividualAccount Authentication
        /// </summary>
        IndividualAccounts,
        /// <summary>
        /// This says you are using an authentication provider that uses OpenID
        /// </summary>
        OpenId,
        /// <summary>
        /// This means you have manually set up the Authentication code which adds the AuthP Roles and Tenant claims to the cookie or JWT Token
        /// </summary>
        UserProvidedAuthentication
    }
}
