using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
    static class Database
    {
        /// <summary>
        /// Creates and returns an open IDbConnection object
        /// </summary>
        public static IDbConnection Open()
        {
            var connStrBldr = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "BookiDev",
                IntegratedSecurity = true
            };
            var conn = new SqlConnection(connStrBldr.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
