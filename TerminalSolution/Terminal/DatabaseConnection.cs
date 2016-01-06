using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlServerCe;
using System.IO;
using System.Data;

namespace Terminal
{
    // DEPRECATED - NAPISAĆ OD NOWA POD MS SQL SERVER
    class DatabaseConnection
    {
        public static String rootPath;
        public static SqlCeConnection InitializeDatabase(String rootPath)
        {
            DatabaseConnection.rootPath = rootPath;
            String connectionString = CreateDatabase();
            SqlCeConnection conn = new SqlCeConnection(connectionString);
            conn.Open();

            CreateTable(conn);

            return conn;
        }

        private static String CreateDatabase()
        {
            String dbPath = String.Format("{0}scanner.sdf", rootPath);
            if (File.Exists(dbPath))
                File.Delete(dbPath);

            String connectionString = String.Format("DataSource=\"{0}\";Max Database Size=3000;", dbPath);
            SqlCeEngine en = new SqlCeEngine(connectionString);
            en.CreateDatabase();
            en.Dispose();

            return connectionString;
        }

        public static void CreateTable(SqlCeConnection conn)
        {
            using (SqlCeCommand comm = new SqlCeCommand())
            {
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "CREATE TABLE gnis ([Id] [int] IDENTITY(1,1) PRIMARY KEY, [Name] [nvarchar](110) NOT NULL, [Geometry] [varbinary](429) NOT NULL)";
                comm.ExecuteNonQuery();
            }
        }
    }
}
