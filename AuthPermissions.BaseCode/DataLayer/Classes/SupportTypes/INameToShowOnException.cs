using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes
{
    /// <summary>
    /// Add this to a AuthP's entity classes to define a name for show when a database exception happens
    /// </summary>
    public interface INameToShowOnException
    {
        /// <summary>
        /// The most useful name in an entity class to show when there is a database exception
        /// </summary>
        public string NameToUseForError { get; }
    }
}
