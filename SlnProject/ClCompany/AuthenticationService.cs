using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// File: AuthenticationService.cs in project ClCompany
using System;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace ClCompany
{
    /// <summary>
    /// Resultaat van een login-poging.
    /// </summary>
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public bool IsAdmin { get; set; }
        public Company Company { get; set; }  // bij admin = null
    }

    /// <summary>
    /// Zorgt voor inloggen van admin of company-gebruiker.
    /// </summary>
    public static class AuthenticationService
    {
        public static AuthResult Login(string login, string passwordPlain)
        {
            string user = login.Trim();

            // admin‐check blijft hetzelfde…
            if (user.Equals(AdminConfig.UserName, StringComparison.OrdinalIgnoreCase))
            {
                bool validAdmin = AdminConfig.Validate(passwordPlain);
                if (validAdmin)
                    return new AuthResult { IsSuccess = true, IsAdmin = true, Company = null };
                else
                    return new AuthResult { IsSuccess = false };
            }

            // --- Company login (via DB) ---
            using SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql =
                "SELECT id, name, contact, address, zip, city, country, phone, email, " +
                "btw, login, password, regdate, acceptdate, lastmodified, status, language, logo, nacecode_code " +
                "FROM dbo.Companies " +
                "WHERE login = @login";                // géén AND password

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@login", user);

            using SqlDataReader rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                return new AuthResult { IsSuccess = false };

            // 1) haal de hash uit de DB
            string storedHash = rdr.GetString(rdr.GetOrdinal("password"));

            // 2) bereken de hash van wat de gebruiker intypte
            string enteredHash = PasswordHelper.Hash(passwordPlain);

            // 3) vergelijk
            if (!CryptographicOperations.FixedTimeEquals(
                    Convert.FromBase64String(enteredHash),
                    Convert.FromBase64String(storedHash)))
            {
                return new AuthResult { IsSuccess = false };
            }

            // 4) check status = "active"
            string status = rdr.GetString(rdr.GetOrdinal("status"));
            if (!status.Equals("active", StringComparison.OrdinalIgnoreCase))
                return new AuthResult { IsSuccess = false };

            // 5) vul je Company‐object en return success…
            Company comp = new Company { /* … alle velden … */ };
            return new AuthResult
            {
                IsSuccess = true,
                IsAdmin = false,
                Company = comp
            };
        }
    }
}
