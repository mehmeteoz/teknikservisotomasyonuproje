using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServisOtomasyonuProje
{
    internal class SQLConnect
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=teknikServisOtomasyonDB;Integrated Security=True;";
        SqlConnection con;

        public SqlConnection connectToSQL()
        {
            con = new SqlConnection(connectionString);
            return con;
        }    
    }
}

