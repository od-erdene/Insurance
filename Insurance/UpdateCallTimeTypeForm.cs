// UpdateCallTimeTypeForm.cs
using System;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UpdateCallTimeTypeForm : Form
    {
        private int callTimeTypeId;

        public UpdateCallTimeTypeForm(int id, string name)
        {
            InitializeComponent();
            this.callTimeTypeId = id;
            txtName.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Дуудлагын цагийн төрлийн нэрийг оруулна уу.");
                return;
            }

            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "UPDATE CallTimeType SET CallTimeTypeName = @name WHERE CallTimeTypeID = @id";
                db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@id", callTimeTypeId);
                db.cmd.ExecuteNonQuery();

                MessageBox.Show("Амжилттай шинэчлэгдлээ!");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Өгөгдлийн санд шинэчлэхэд алдаа гарлаа: " + ex.Message);
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