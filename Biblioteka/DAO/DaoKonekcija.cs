using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.DAO
{
    public static class DaoKonekcija
    {
        private static string connString = "Server=localhost;Database=Biblioteka;Integrated security=True;MultipleActiveResultSets=True";

        public static SqlConnection NewConnection()
        { 
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            return conn;
        }
    }
}
