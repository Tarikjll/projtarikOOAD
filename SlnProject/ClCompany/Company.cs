using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ClCompany;      // voor DbHelper & SessionContext
using System.Data;
using Microsoft.Data.SqlClient;



namespace ClCompany
{
    public class Company
    {
        // === 1) Properties ===
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   // NOT NULL
        public string? Contact { get; set; }                  // NULLABLE
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Btw { get; set; }
        public string Login { get; set; } = string.Empty;   // NOT NULL
        public string Password { get; set; } = string.Empty;   // NOT NULL
        public DateTime RegDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string Status { get; set; } = string.Empty;   // NOT NULL
        public string Language { get; set; } = string.Empty;   // NOT NULL
        public byte[]? Logo { get; set; }                  // NULLABLE
        public string NacecodeCode { get; set; } = string.Empty;   // NOT NULL

        // === 2) Parameterloze constructor ===
        public Company()
        {
        }

        // === 3) GetAll (read-only) ===
        public static List<Company> GetAll()
        {
            List<Company> lijst = new List<Company>();
            SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql =
                "SELECT id, name, contact, address, zip, city, country, phone, email, " +
                "btw, login, password, regdate, acceptdate, lastmodified, status, language, logo, nacecode_code " +
                "FROM dbo.Companies";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Company comp = new Company();
                comp.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                comp.Name = rdr.GetString(rdr.GetOrdinal("name"));
                comp.Contact = rdr.IsDBNull(rdr.GetOrdinal("contact"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("contact"));
                comp.Address = rdr.IsDBNull(rdr.GetOrdinal("address"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("address"));
                comp.Zip = rdr.IsDBNull(rdr.GetOrdinal("zip"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("zip"));
                comp.City = rdr.IsDBNull(rdr.GetOrdinal("city"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("city"));
                comp.Country = rdr.IsDBNull(rdr.GetOrdinal("country"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("country"));
                comp.Phone = rdr.IsDBNull(rdr.GetOrdinal("phone"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("phone"));
                comp.Email = rdr.IsDBNull(rdr.GetOrdinal("email"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("email"));
                comp.Btw = rdr.IsDBNull(rdr.GetOrdinal("btw"))
                                       ? null
                                       : rdr.GetString(rdr.GetOrdinal("btw"));
                comp.Login = rdr.GetString(rdr.GetOrdinal("login"));
                comp.Password = rdr.GetString(rdr.GetOrdinal("password"));
                comp.RegDate = rdr.GetDateTime(rdr.GetOrdinal("regdate"));
                comp.AcceptDate = rdr.IsDBNull(rdr.GetOrdinal("acceptdate"))
                                       ? (DateTime?)null
                                       : rdr.GetDateTime(rdr.GetOrdinal("acceptdate"));
                comp.LastModified = rdr.IsDBNull(rdr.GetOrdinal("lastmodified"))
                                       ? (DateTime?)null
                                       : rdr.GetDateTime(rdr.GetOrdinal("lastmodified"));
                comp.Status = rdr.GetString(rdr.GetOrdinal("status"));
                comp.Language = rdr.GetString(rdr.GetOrdinal("language"));
                int logoIndex = rdr.GetOrdinal("logo");
                comp.Logo = rdr.IsDBNull(logoIndex)
                                       ? null
                                       : (byte[])rdr.GetValue(logoIndex);
                comp.NacecodeCode = rdr.GetString(rdr.GetOrdinal("nacecode_code"));

                lijst.Add(comp);
            }

            rdr.Close();
            conn.Close();
            return lijst;
        }

        // === 4) GetById (read-only) ===
        public static Company? GetById(int id)
        {
            Company? resultaat = null;
            SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql =
                "SELECT id, name, contact, address, zip, city, country, phone, email, " +
                "btw, login, password, regdate, acceptdate, lastmodified, status, language, logo, nacecode_code " +
                "FROM dbo.Companies WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                resultaat = new Company();
                resultaat.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                resultaat.Name = rdr.GetString(rdr.GetOrdinal("name"));
                resultaat.Contact = rdr.IsDBNull(rdr.GetOrdinal("contact"))
                                           ? null
                                           : rdr.GetString(rdr.GetOrdinal("contact"));
                // … vul hier verder alle velden in zoals bij GetAll()
            }

            rdr.Close();
            conn.Close();
            return resultaat;
        }

        // === 5) Insert (admin-only) ===
        public void Insert()
        {
            if (!SessionContext.IsAdmin)
            {
                throw new UnauthorizedAccessException("Alleen admin mag nieuwe bedrijven toevoegen.");
            }

            SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql =
                "INSERT INTO dbo.Companies " +
                "(name, contact, address, zip, city, country, phone, email, btw, login, password, " +
                " regdate, acceptdate, lastmodified, status, language, logo, nacecode_code) " +
                "VALUES (@name, @contact, @address, @zip, @city, @country, @phone, @email, @btw, " +
                "@login, @password, @regdate, @acceptdate, @lastmodified, @status, @language, @logo, @nace)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@contact", (object?)Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@address", (object?)Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@zip", (object?)Zip ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city", (object?)City ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country", (object?)Country ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone", (object?)Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@btw", (object?)Btw ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@login", Login);
            cmd.Parameters.AddWithValue("@password", PasswordHelper.Hash(this.Password));
            cmd.Parameters.AddWithValue("@regdate", RegDate);
            cmd.Parameters.AddWithValue("@acceptdate", (object?)AcceptDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lastmodified", (object?)LastModified ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", Status);
            cmd.Parameters.AddWithValue("@language", Language);
            cmd.Parameters.AddWithValue("@logo", (object?)Logo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@nace", NacecodeCode);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // === 6) Update (admin-only) ===
        public void Update()
        {
            if (!SessionContext.IsAdmin)
            {
                throw new UnauthorizedAccessException("Alleen admin mag bedrijven bewerken.");
            }

            SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql =
                "UPDATE dbo.Companies SET " +
                "name=@name, contact=@contact, address=@address, zip=@zip, city=@city, country=@country, " +
                "phone=@phone, email=@email, btw=@btw, login=@login, password=@password, regdate=@regdate, " +
                "acceptdate=@acceptdate, lastmodified=@lastmodified, status=@status, language=@language, logo=@logo, " +
                "nacecode_code=@nace " +
                "WHERE id=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@contact", (object?)Contact ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@address", (object?)Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@zip", (object?)Zip ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@city", (object?)City ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@country", (object?)Country ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@phone", (object?)Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object?)Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@btw", (object?)Btw ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@login", Login);
            cmd.Parameters.AddWithValue("@password",PasswordHelper.Hash(this.Password));
            cmd.Parameters.AddWithValue("@regdate", RegDate);
            cmd.Parameters.AddWithValue("@acceptdate", (object?)AcceptDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@lastmodified", (object?)LastModified ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@status", Status);
            cmd.Parameters.AddWithValue("@language", Language);
            // zorg dat je System.Data gebruikt hebt:
            // using System.Data;

            // … ná alle andere AddWithValue calls …

            // in plaats van AddWithValue gebruiken we hier het juiste DbType
            SqlParameter pLogo = cmd.Parameters.Add("@logo", SqlDbType.Image);
            if (Logo != null)
            {
                pLogo.Value = Logo;            // Logo is byte[] of MemoryStream
            }
            else
            {
                pLogo.Value = DBNull.Value;    // laat ongewijzigd
            }

      


            cmd.Parameters.AddWithValue("@nace", NacecodeCode);
            cmd.Parameters.AddWithValue("@id", Id);



            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // === 7) Delete (admin-only) ===
        public void Delete()
        {
            if (!SessionContext.IsAdmin)
            {
                throw new UnauthorizedAccessException("Alleen admin mag bedrijven verwijderen.");
            }

            SqlConnection conn = DbHelper.GetOpenConnection();
            const string sql = "DELETE FROM dbo.Companies WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        // === 8) ToString() ===
        public override string ToString()
        {
            return Name;
        }
    }
}
