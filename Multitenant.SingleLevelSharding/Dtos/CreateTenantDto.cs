using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitenant.SingleLevelSharding.Dtos
{
    public class CreateTenantDto
    {
        public string TenantName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }

        public TenantVersionTypes GetTenantVersionType()
        {
            return string.IsNullOrWhiteSpace(Version)
                ? TenantVersionTypes.NotSet
                : Enum.Parse<TenantVersionTypes>(Version);
        }


        public override string ToString()
        {
            return $"{nameof(TenantName)}: {TenantName}, {nameof(Email)}: {Email}, {nameof(Password)}: {Password}, {nameof(Version)}: {Version}";
        }
    }
}
