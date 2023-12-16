using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectICU_Server.Net.SQL
{
    internal class MySqlDataBase
    {
        private readonly string _connectionString;
        public MySqlDataBase()
        {
            _connectionString = "Server=46.31.77.173,3306;Database=javaproject;Uid=JavaProject;Pwd=JavaProject_ICU123;";
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
