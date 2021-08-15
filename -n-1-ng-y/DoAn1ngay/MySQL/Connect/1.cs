using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MySQL.Connect
{
    public class _1
    {
        public static MySqlConnection GetDBConnection(string host, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database+ ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
