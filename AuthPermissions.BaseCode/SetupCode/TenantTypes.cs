﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.SetupCode
{
    /// <summary>
    /// This defines the types of tenant the AuthPermissions can handle, with optional sharding
    /// </summary>
    [Flags]
    public enum TenantTypes
    {
        /// <summary>
        /// Usage of tenants are turned off
        /// </summary>
        NotUsingTenants = 0,
        /// <summary>
        /// Multi-tenant with one level only, e.g. a company has different departments: sales, finance, HR etc.
        /// A User can only be in one of these levels
        /// </summary>
        SingleLevel = 1,
        /// <summary>
        /// Multi-tenant with one level only, e.g. a company has different departments: sales, finance, HR etc.
        /// A tenant can be mixed in with 
        /// </summary>
        /// <summary>
        /// Multi-tenant many levels, e.g. Holding company -> USA branch -> East Coast -> New York
        /// A User at the USA branch has read/write access to the USA branch data, read-only access to the East Coast and all its subsidiaries 
        /// </summary>
        HierarchicalTenant = 2,
        /// <summary>
        /// This turns on the sharding. Sharding allows tenants to be split across many databases, including placing a tenant's data in its own database.
        /// 
        /// </summary>
        AddSharding = 4

    }
}
