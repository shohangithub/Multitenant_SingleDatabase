using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthPermissions.BaseCode.CommonCode
{
    /// <summary>
    /// This defines an Encrypt/Decrypt service used by AuthP
    /// </summary>
    public interface IEncryptDecryptService
    {
        /// <summary>
        /// This encrypts a string using the Aes encryption with the key provided
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string Encrypt(string text);

        /// <summary>
        /// This decrypts a string using the Aes encryption with the key provided
        /// </summary>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        string Decrypt(string encrypted);
    }
}
