using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySQL.Connect;
namespace MySQL.Connect
{
    public class _2
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "156.67.72.151";            
            string database = "u471372627_QuanLyKhoHang";
            string username = "u471372627_admin123";
            string password = "Anhngheo8x";

            return _1.GetDBConnection(host, database, username, password);
        }
    }
}
