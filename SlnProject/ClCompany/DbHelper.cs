using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace ClCompany
{
    public static class DbHelper
    {
        private static readonly string _connString =
            ConfigurationManager
               .ConnectionStrings["BenchmarkDb"]
               .ConnectionString;

        public static SqlConnection GetOpenConnection()
        {
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
            return conn;
        }
    }
}
