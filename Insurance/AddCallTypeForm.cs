// AddCallTypeForm.cs
using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class AddCallTypeForm : Form
    {
        public AddCallTypeForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Дуудлагын төрлийн нэрийг оруулна уу.");
                return;
            }

            DB db = new DB();
            db.cmd.CommandText = "INSERT INTO CallType (CallTypeName) VALUES (@name)";
            db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            db.cmd.ExecuteNonQuery();

            MessageBox.Show("Амжилттай нэмэгдлээ!");
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
