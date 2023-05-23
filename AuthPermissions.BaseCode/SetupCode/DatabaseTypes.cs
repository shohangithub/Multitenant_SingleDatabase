using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.SetupCode
{
    /// <summary>
    /// The different database types that AuthPermissions supports
    /// </summary>
    public enum AuthPDatabaseTypes
    {
        /// <summary>
        /// This is the default - AuthPermissions will throw an exception to say you must define the database type
        /// </summary>
        NotSet,
        /// <summary>
        /// This is a in-memory database - useful for unit testing
        /// </summary>
        SqliteInMemory,
        /// <summary>
        /// SQL Server database is used
        /// </summary>
        SqlServer,
        /// <summary>
        /// Postgres database is used
        /// </summary>
        Postgres
    }
}
