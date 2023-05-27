﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.Factories
{
    /// <summary>
    /// Generic factory method to handle services that are (optionally) registered by the developer
    /// </summary>
    /// <typeparam name="TServiceInterface"></typeparam>
    public interface IAuthPServiceFactory<out TServiceInterface> where TServiceInterface : class
    {
        /// <summary>
        /// This returns the service registered to the <see type="TServiceInterface"/> interface
        /// </summary>
        /// <param name="throwExceptionIfNull">If no service found and this is true, then throw an exception</param>
        /// <param name="callingMethod">This contains the name of the calling method</param>
        /// <returns></returns>
        TServiceInterface GetService(bool throwExceptionIfNull = true, [CallerMemberName] string callingMethod = "");
    }
}
