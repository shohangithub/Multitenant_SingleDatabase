using AuthPermissions;
using System.Security.Claims;

namespace Common.IdentityCookieCode
{
    public class AddRefreshEveryMinuteClaim : IClaimsAdder
    {
        public Task<Claim> AddClaimToUserAsync(string userId)
        {
            var claim = PeriodicCookieEvent.TimeToRefreshUserClaimType
                .CreateClaimDateTimeTicks(new TimeSpan(0, 0, 1, 0));
            return Task.FromResult(claim);
        }
    }
}
