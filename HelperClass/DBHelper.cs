using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADNU_CFRS.HelperClass
{
    public class DBHelper : IDisposable
    {
        public MySqlConnection connection;
        private string connStr = Globals.cs;

        public DBHelper()
        {
            OpenConnection();
        }

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }

        private void OpenConnection()
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = connStr;
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}