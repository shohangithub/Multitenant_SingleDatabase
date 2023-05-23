﻿namespace AuthPermissions.BaseCode.CommonCode
{
    /// <summary>
    /// A AuthPermissions for bad data errors
    /// </summary>
    public class AuthPermissionsBadDataException : ArgumentException
    {
        /// <summary>
        /// Just send a message
        /// </summary>
        /// <param name="message"></param>
        public AuthPermissionsBadDataException(string message)
            : base(message) { }

        /// <summary>
        /// Message and parameter name
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramName"></param>
        public AuthPermissionsBadDataException(string message, string paramName)
            : base(message, paramName) { }
    }
}
