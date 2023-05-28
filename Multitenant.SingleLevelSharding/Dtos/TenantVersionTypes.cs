using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitenant.SingleLevelSharding.Dtos
{
    public enum TenantVersionTypes
    {
        //Error
        NotSet,
        //Only allows one user per tenant
        Free,
        //Allows many users in one tenant
        Pro,
        //Have your own admin user
        Enterprise
    }
}
