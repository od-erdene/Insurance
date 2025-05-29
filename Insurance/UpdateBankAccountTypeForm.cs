// UpdateBankAccountTypeForm.cs
using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UpdateBankAccountTypeForm : Form
    {
        private int id;

        public UpdateBankAccountTypeForm(int id, string name)
        {
            InitializeComponent();
            this.id = id;
            txtName.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Дансны төрлийн нэрийг оруулна уу.");
                return;
            }

            DB db = new DB();
            db.cmd.CommandText = "UPDATE BankAccountType SET BankAccountTypeName = @name WHERE BankAccountTypeID = @id";
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
    }
}
