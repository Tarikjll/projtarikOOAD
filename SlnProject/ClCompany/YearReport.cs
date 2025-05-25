using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ClCompany
{
    public class YearReport
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public double Fte { get; set; }
        public int CompanyId { get; set; }

        public YearReport() { }

        public static List<YearReport> GetByCompanyId(int companyId)
        {
            List<YearReport> lijst = new List<YearReport>();
            SqlConnection conn = DbHelper.GetOpenConnection();

            const string sql =
                "SELECT id, year, fte, company_id " +
                "FROM dbo.YearReports " +
                "WHERE company_id = @cid " +
                "ORDER BY year DESC";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@cid", companyId);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                YearReport yr = new YearReport();
                yr.Id = rdr.GetInt32(rdr.GetOrdinal("id"));
                yr.Year = rdr.GetInt32(rdr.GetOrdinal("year"));
                yr.Fte = rdr.GetDouble(rdr.GetOrdinal("fte"));
                yr.CompanyId = rdr.GetInt32(rdr.GetOrdinal("company_id"));
                lijst.Add(yr);
            }

            rdr.Close();
            conn.Close();
            return lijst;
        }

        public override string ToString()
        {
            return $"{Year} — FTE: {Fte}";
        }
    }
}
