// UserControlInvestigationOrganization.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UserControlInvestigationOrganization : UserControl
    {
        public UserControlInvestigationOrganization()
        {
            InitializeComponent();
            Load += UserControlInvestigationOrganization_Load;
        }

        private void UserControlInvestigationOrganization_Load(object sender, EventArgs e)
        {
            LoadInvestigationOrganizations();
        }

        private void LoadInvestigationOrganizations()
        {
            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT InvestigationOrganizationID, InvestigationOrganizationName FROM InvestigationOrganization ORDER BY InvestigationOrganizationName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                // Optionally, rename column headers for better display
                if (grid.Columns.Contains("InvestigationOrganizationName"))
                    grid.Columns["InvestigationOrganizationName"].HeaderText = "Мөрдөн байцаах байгууллага";
                if (grid.Columns.Contains("InvestigationOrganizationID"))
                    grid.Columns["InvestigationOrganizationID"].Visible = false; // Hide ID column
            }
            catch (Exception ex)
            {
                MessageBox.Show("Мөрдөн байцаах байгууллагыг ачаалахад алдаа гарлаа: " + ex.Message);
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
            var form = new AddInvestigationOrganizationForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadInvestigationOrganizations();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["InvestigationOrganizationID"].Value);
                string name = grid.CurrentRow.Cells["InvestigationOrganizationName"].Value.ToString();

                var form = new UpdateInvestigationOrganizationForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInvestigationOrganizations();
                }
            }
            else
            {
                MessageBox.Show("Засах мөрдөн байцаах байгууллагыг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["InvestigationOrganizationID"].Value);
                DialogResult result = MessageBox.Show("Та энэ мөрдөн байцаах байгууллагыг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null; // Initialize to null
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM InvestigationOrganization WHERE InvestigationOrganizationID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadInvestigationOrganizations();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key Violation
                        {
                            MessageBox.Show("Энэ мөрдөн байцаах байгууллагатай холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
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
                MessageBox.Show("Устгах мөрдөн байцаах байгууллагыг сонгоно уу.");
            }
        }
    }
}