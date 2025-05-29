using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class AddBankForm : Form
    {
        public AddBankForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                MessageBox.Show("Банкны нэрийг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankName.Focus();
                return;
            }

            DB db = null;
            try
            {
                db = new DB();
                string checkQuery = "SELECT COUNT(*) FROM Bank WHERE BankName = @BankName";
                db.cmd.CommandText = checkQuery;
                db.cmd.Parameters.Clear();
                db.cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
                int count = Convert.ToInt32(db.cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Энэ банкны нэр аль хэдийн бүртгэгдсэн байна.", "Давхардсан мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBankName.Focus();
                    return;
                }

                string insertQuery = "INSERT INTO Bank (BankName) VALUES (@BankName)";
                db.cmd.CommandText = insertQuery;
                db.cmd.Parameters.Clear();
                db.cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());

                db.cmd.ExecuteNonQuery();
                MessageBox.Show("Банк амжилттай нэмэгдлээ!", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Банк нэмэхэд алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}