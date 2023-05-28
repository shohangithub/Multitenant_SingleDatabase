﻿using AuthPermissions.BaseCode.CommonCode;
using Microsoft.EntityFrameworkCore;

namespace Multitenant.SingleLevelSharding.EfCoreCode
{
    public static class MarkDataKeyExtension
    {
        public static void MarkWithDataKeyIfNeeded(this DbContext context, string accessKey)
        {
            foreach (var entityEntry in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                var hasDataKey = entityEntry.Entity as IDataKeyFilterReadWrite;
                if (hasDataKey != null && hasDataKey.DataKey == null)
                    // If the entity has a DataKey it will only update it if its null
                    // This allow for the code to define the DataKey on creation
                    hasDataKey.DataKey = accessKey;
            }
        }
    }
}
