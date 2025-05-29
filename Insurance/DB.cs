using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Insurance
{
     class DB
    {
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public DataSet ds = new DataSet();
        public SqlDataAdapter ada = new SqlDataAdapter();
        public SqlDataReader dr;

        public DB()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Close();
                con.Dispose();

            }
            con.ConnectionString = @"Data Source = 172.16.35.45; Initial Catalog = Insurance_New; User ID = sa; Password = 123";
            con.Open();
            cmd.Connection = con;
        }
    }
}
