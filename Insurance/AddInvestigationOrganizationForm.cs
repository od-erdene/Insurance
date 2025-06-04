// AddInvestigationOrganizationForm.cs
using System;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class AddInvestigationOrganizationForm : Form
    {
        public AddInvestigationOrganizationForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Мөрдөн байцаах байгууллагын нэрийг оруулна уу.");
                return;
            }

            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "INSERT INTO InvestigationOrganization (InvestigationOrganizationName) VALUES (@name)";
                db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                db.cmd.ExecuteNonQuery();

                MessageBox.Show("Амжилттай нэмэгдлээ!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Өгөгдлийн санд хадгалахад алдаа гарлаа: " + ex.Message);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}