using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.CommonCode
{
    /// <summary>
    /// This is on entity classes where the DataKey set by setting the DataKey directly
    /// </summary>
    public interface IDataKeyFilterReadWrite
    {
        /// <summary>
        /// The DataKey to be used for multi-tenant applications
        /// </summary>
        public string DataKey { get; set; }
    }
}
