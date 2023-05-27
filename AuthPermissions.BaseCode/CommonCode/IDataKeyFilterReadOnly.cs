using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.CommonCode
{
    /// <summary>
    /// This is used on entity classes where the DataKey isn't set by setting the DataKey directly
    /// </summary>
    public interface IDataKeyFilterReadOnly
    {
        /// <summary>
        /// The DataKey to be used for multi-tenant applications
        /// </summary>
        string DataKey { get; }
    }
}
