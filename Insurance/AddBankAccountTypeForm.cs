// AddBankAccountTypeForm.cs
using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class AddBankAccountTypeForm : Form
    {
        public AddBankAccountTypeForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Дансны төрлийн нэр оруулна уу.");
                return;
            }

            DB db = new DB();
            db.cmd.CommandText = "INSERT INTO BankAccountType (BankAccountTypeName) VALUES (@name)";
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
    }
}
