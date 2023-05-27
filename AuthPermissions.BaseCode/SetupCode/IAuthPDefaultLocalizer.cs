using LocalizeMessagesAndErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.SetupCode
{
    /// <summary>
    /// This provides the correct <see cref="IDefaultLocalizer"/> service for
    /// any of AuthP's methods that support localizations.
    /// This should be registered to the DI as a singleton 
    /// </summary>
    public interface IAuthPDefaultLocalizer
    {
        /// <summary>
        /// Correct <see cref="IDefaultLocalizer"/> service for the AuthP to use on localized code.
        /// </summary>
        IDefaultLocalizer DefaultLocalizer { get; }
    }
}
