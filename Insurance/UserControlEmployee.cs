// UserControlEmployee.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class UserControlEmployee : UserControl
    {
        public UserControlEmployee()
        {
            InitializeComponent();
            Load += UserControlEmployee_Load;
        }

        private void UserControlEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            DB db = null;
            try
            {
                db = new DB();
                db.cmd.CommandText = @"
                    SELECT
                        e.EmployeeID,
                        e.EmployeeLastName,
                        e.EmployeeFirstName,
                        e.EmployeePhoneNumber,
                        e.EmployeeAddress,
                        comm.CommitteeName,
                        e.EmployeeUsername,
                        e.EmployeePassword, -- In real app, avoid displaying passwords
                        e.CommitteID -- Hidden but needed for UpdateForm
                    FROM Employee e
                    JOIN Committee comm ON e.CommitteID = comm.CommitteeID
                    ORDER BY e.EmployeeLastName, e.EmployeeFirstName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                // Rename and hide columns for better display
                if (grid.Columns.Contains("EmployeeLastName"))
                    grid.Columns["EmployeeLastName"].HeaderText = "Овог";
                if (grid.Columns.Contains("EmployeeFirstName"))
                    grid.Columns["EmployeeFirstName"].HeaderText = "Нэр";
                if (grid.Columns.Contains("EmployeePhoneNumber"))
                    grid.Columns["EmployeePhoneNumber"].HeaderText = "Утас";
                if (grid.Columns.Contains("EmployeeAddress"))
                    grid.Columns["EmployeeAddress"].HeaderText = "Хаяг";
                if (grid.Columns.Contains("CommitteeName"))
                    grid.Columns["CommitteeName"].HeaderText = "Хороо";
                if (grid.Columns.Contains("EmployeeUsername"))
                    grid.Columns["EmployeeUsername"].HeaderText = "Нэвтрэх нэр";

                // Hide ID columns
                if (grid.Columns.Contains("EmployeeID"))
                    grid.Columns["EmployeeID"].Visible = false;
                if (grid.Columns.Contains("CommitteID"))
                    grid.Columns["CommitteID"].Visible = false;

                if (grid.Columns.Contains("EmployeePassword"))
                    grid.Columns["EmployeePassword"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ажилтны мэдээлэл ачаалахад алдаа гарлаа: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddEmployeeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["EmployeeID"].Value);
                string lastName = grid.CurrentRow.Cells["EmployeeLastName"].Value.ToString();
                string firstName = grid.CurrentRow.Cells["EmployeeFirstName"].Value.ToString();
                string phoneNumber = grid.CurrentRow.Cells["EmployeePhoneNumber"].Value.ToString();
                string address = grid.CurrentRow.Cells["EmployeeAddress"].Value.ToString();
                int committeeId = Convert.ToInt32(grid.CurrentRow.Cells["CommitteID"].Value);
                string username = grid.CurrentRow.Cells["EmployeeUsername"].Value.ToString();
                string password = grid.CurrentRow.Cells["EmployeePassword"].Value.ToString();

                var form = new UpdateEmployeeForm(id, lastName, firstName, phoneNumber, address, committeeId, username, password);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Засах ажилтныг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["EmployeeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ ажилтныг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null;
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM Employee WHERE EmployeeID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadEmployees();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key Violation
                        {
                            MessageBox.Show("Энэ ажилтантай холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
                        }
                        else
                        {
                            MessageBox.Show("Устгахад алдаа гарлаа: " + ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Алдаа гарлаа: " + ex.Message);
                    }
                    finally
                    {
                        if (db != null)
                        {
                            db.Dispose();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Устгах ажилтныг сонгоно уу.");
            }
        }
    }
}