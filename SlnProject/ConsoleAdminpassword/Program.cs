using System;
using ClCompany;

namespace UserMigration
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using System;
    using System.Security.Cryptography;
    using System.Text;

    class Program
    {
        static void Main()
        {
            // 1) Kies hier het duidelijke admin-wachtwoord
            const string password = "SuperVeilig123!";

            // 2) Genereer een 16-byte salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            string saltB64 = Convert.ToBase64String(salt);
            Console.WriteLine("Salt (Base64): " + saltB64);

            // 3) Bereken PBKDF2-SHA256-hash (32 bytes)
            using var pbkdf2 = new Rfc2898DeriveBytes(
                Encoding.UTF8.GetBytes(password),
                salt,
                100_000,
                HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);
            string hashB64 = Convert.ToBase64String(hash);
            Console.WriteLine("Hash (Base64): " + hashB64);

            Console.WriteLine("\nCopy-en-plak díe twee waarden in AdminConfig.");
            Console.ReadKey();
        }
    }


}
