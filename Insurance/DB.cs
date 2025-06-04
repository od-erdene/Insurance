using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Insurance
{
    class DB : IDisposable
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
            con.ConnectionString = @"Data Source = KHUSLEN\SQLEXPRESS; Initial Catalog = Insurance_New; User ID = sa; Password = 123";
            con.Open();
            cmd.Connection = con;
        }

      
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
         
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Dispose();
                    con = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
                if (ada != null)
                {
                    ada.Dispose();
                    ada = null;
                }
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }
            }
          
        }

   
        ~DB()
        {
            Dispose(false);
        }
    }
}
