// UpdateCallTypeForm.cs
using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UpdateCallTypeForm : Form
    {
        private int id;

        public UpdateCallTypeForm(int id, string name)
        {
            InitializeComponent();
            this.id = id;
            txtName.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Дуудлагын төрлийн нэрийг оруулна уу.");
                return;
            }

            DB db = new DB();
            db.cmd.CommandText = "UPDATE CallType SET CallTypeName = @name WHERE CallTypeID = @id";
            db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            db.cmd.Parameters.AddWithValue("@id", id);
            db.cmd.ExecuteNonQuery();

            MessageBox.Show("Амжилттай шинэчлэгдлээ!");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {

        }
    }
}
