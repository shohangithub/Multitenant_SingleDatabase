using System.ComponentModel.DataAnnotations;


namespace AuthPermissions.AspNetCore.JwtTokenCode
{
    /// <summary>
    /// This is used for input and output of the JWT Token and the RefreshToken
    /// </summary>
    public class TokenAndRefreshToken
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string RefreshToken { get; set; }
    }
}
