using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookiAPI.DataAccessLayer
{
    public static class Database
    {
        /// <summary>
        /// Creates and returns an open IDbConnection object
        /// </summary>

        private static bool _testEnv = false;
        public static bool testEnv
        {
            get => _testEnv;
            set => _testEnv = value;
        }
        public static IDbConnection Open()
        {
            string dbName = "BookiDev";
            if (testEnv)
                dbName = "BookiTest";

            var connStrBldr = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };
            var conn = new SqlConnection(connStrBldr.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
