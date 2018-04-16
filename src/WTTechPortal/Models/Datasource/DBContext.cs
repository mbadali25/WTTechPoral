using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WTTechPortal.Models.Datasource
{
    public class DBContext
    {
        public string ConnectionString { get; set; }

        public DBContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

    }
}
