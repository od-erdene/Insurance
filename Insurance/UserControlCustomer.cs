// UserControlCustomer.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class UserControlCustomer : UserControl
    {
        public UserControlCustomer()
        {
            InitializeComponent();
            Load += UserControlCustomer_Load;
        }

        private void UserControlCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            DB db = null;
            try
            {
                db = new DB();
                db.cmd.CommandText = @"
                    SELECT
                        c.CustomerID,
                        c.CustomerLastName,
                        c.CustomerFirstName,
                        c.CustomerRegisterNumber,
                        c.CustomerPhoneNumber1,
                        c.CustomerPhoneNumber2,
                        comm.CommitteeName,
                        c.CustomerAddress,
                        c.CommitteeID -- Hidden but needed for UpdateForm
                    FROM Customer c
                    JOIN Committee comm ON c.CommitteeID = comm.CommitteeID
                    ORDER BY c.CustomerLastName, c.CustomerFirstName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                // Rename and hide columns for better display
                if (grid.Columns.Contains("CustomerLastName"))
                    grid.Columns["CustomerLastName"].HeaderText = "Овог";
                if (grid.Columns.Contains("CustomerFirstName"))
                    grid.Columns["CustomerFirstName"].HeaderText = "Нэр";
                if (grid.Columns.Contains("CustomerRegisterNumber"))
                    grid.Columns["CustomerRegisterNumber"].HeaderText = "Регистр";
                if (grid.Columns.Contains("CustomerPhoneNumber1"))
                    grid.Columns["CustomerPhoneNumber1"].HeaderText = "Утас 1";
                if (grid.Columns.Contains("CustomerPhoneNumber2"))
                    grid.Columns["CustomerPhoneNumber2"].HeaderText = "Утас 2";
                if (grid.Columns.Contains("CommitteeName"))
                    grid.Columns["CommitteeName"].HeaderText = "Хороо";
                if (grid.Columns.Contains("CustomerAddress"))
                    grid.Columns["CustomerAddress"].HeaderText = "Хаяг";

                // Hide ID columns
                if (grid.Columns.Contains("CustomerID"))
                    grid.Columns["CustomerID"].Visible = false;
                if (grid.Columns.Contains("CommitteeID"))
                    grid.Columns["CommitteeID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Харилцагчийн мэдээлэл ачаалахад алдаа гарлаа: " + ex.Message);
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
            var form = new AddCustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CustomerID"].Value);
                string lastName = grid.CurrentRow.Cells["CustomerLastName"].Value.ToString();
                string firstName = grid.CurrentRow.Cells["CustomerFirstName"].Value.ToString();
                string registerNumber = grid.CurrentRow.Cells["CustomerRegisterNumber"].Value.ToString();
                string phone1 = grid.CurrentRow.Cells["CustomerPhoneNumber1"].Value.ToString();
                string phone2 = grid.CurrentRow.Cells["CustomerPhoneNumber2"].Value.ToString();
                int committeeId = Convert.ToInt32(grid.CurrentRow.Cells["CommitteeID"].Value);
                string address = grid.CurrentRow.Cells["CustomerAddress"].Value.ToString();

                var form = new UpdateCustomerForm(id, lastName, firstName, registerNumber, phone1, phone2, committeeId, address);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Засах харилцагчийг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CustomerID"].Value);
                DialogResult result = MessageBox.Show("Та энэ харилцагчийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null;
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM Customer WHERE CustomerID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadCustomers();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) 
                        {
                            MessageBox.Show("Энэ харилцагчтай холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
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
                MessageBox.Show("Устгах харилцагчийг сонгоно уу.");
            }
        }
    }
}