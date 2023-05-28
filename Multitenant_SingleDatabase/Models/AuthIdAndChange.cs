using AuthPermissions.AdminCode;

namespace Multitenant_SingleDatabase.Models
{
    public class AuthIdAndChange
    {
        public SyncAuthUserChangeTypes FoundChangeType { get; set; }
        public string UserId { get; set; }
    }
}
