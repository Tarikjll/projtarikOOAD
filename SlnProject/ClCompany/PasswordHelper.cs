using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ClCompany
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Hash een clear‐text wachtwoord met SHA-256 en Base64‐encode het resultaat.
        /// </summary>
        public static string Hash(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
