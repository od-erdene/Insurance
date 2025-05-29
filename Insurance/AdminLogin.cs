using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Insurance;

namespace Insurance
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DB db = null;
            try
            {
                db = new DB();

                string query = "SELECT EmployeeID FROM Employee WHERE EmployeeUsername = @Username AND EmployeePassword = @Password";
                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear();
                db.cmd.Parameters.AddWithValue("@Username", textBoxUsername.Text);
                db.cmd.Parameters.AddWithValue("@Password", textBoxPassword.Text);

                db.dr = db.cmd.ExecuteReader();

                if (db.dr.Read())
                {
                    this.Hide();
                    using (Admin admin = new Admin())
                    {
                        admin.ShowDialog();
                    }

                    this.Show();
                }
                else
                {
                    info_label.Text = "Хэрэглэгчийн нэр эсвэл нууц үг буруу байна дахин оролдоно уу.";
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Датабаз дээр асуудал гарлаа: {ex.Message}", "Асуудал", MessageBoxButtons.OK, MessageBoxIcon.Error);
                info_label.Text = "Датабаз дээр асуудал гарлаа.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Алдаа гарлаа: {ex.Message}", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                info_label.Text = "Системийн алдаа гарлаа.";
            }
            finally
            {
                if (db != null && db.dr != null && !db.dr.IsClosed)
                {
                    db.dr.Close();
                }
                if (db != null && db.con != null && db.con.State == ConnectionState.Open)
                {
                    db.con.Close();
                    db.con.Dispose(); 
                }
            }
        }
    }
}
