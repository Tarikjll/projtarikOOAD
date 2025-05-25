using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;

namespace ClCompany
{
    public static class AdminConfig
    {
        // 1) Hardcoded admin-username
        public const string UserName = "admin";
        // 1) De salt voor PBKDF2 (60 bit is minimum, hier Base64)
        private static readonly byte[] _salt =
            Convert.FromBase64String("VsixGeRQxbee+p8L2Oxt7w=="); // “mijeneikelmeting” als voorbeeld

        // 2) Het aantal iteraties (hoe hoger, hoe trager om hash te kraken)
        private const int _iterations = 100_000;

        // 3) De opgeslagen hash (Base64), berekend met dezelfde salt+iteraties
        private const string _storedHash =
            "apzEYOsP3VYlW6DnUPUeJ+rGrF4CgAFE1iEkPbhLVM4="; // voorbeeld — vervang door jouw hash

        /// <summary>
        /// Valideert het opgegeven wachtwoord tegen de opgeslagen hash.
        /// </summary>
        public static bool Validate(string password)
        {
            // 4) Gebruik PBKDF2 (SHA-256)
            using (Rfc2898DeriveBytes pbkdf2 =
                       new Rfc2898DeriveBytes(
                           password,
                           _salt,
                           _iterations,
                           HashAlgorithmName.SHA256))
            {
                byte[] computed = pbkdf2.GetBytes(32);
                byte[] expected = Convert.FromBase64String(_storedHash);

                // 5) Constant-time vergelijking
                return CryptographicOperations.FixedTimeEquals(computed, expected);
            }
        }
    }
}
