using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.AspNetCore.Services
{
    /// <summary>
    /// This contains the data in the sharding settings file
    /// </summary>
    public class ShardingSettingsOption
    {
        /// <summary>
        /// This holds the list of <see cref="DatabaseInformation"/>. Can be null if no file found
        /// </summary>
        public List<DatabaseInformation> ShardingDatabases { get; set; }
    }
}
