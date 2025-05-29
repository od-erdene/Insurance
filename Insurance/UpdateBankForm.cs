using System;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UpdateBankForm : Form
    {
        private int bankIdToUpdate;

        public UpdateBankForm(int bankId, string currentBankName)
        {
            InitializeComponent();
            this.bankIdToUpdate = bankId;
            txtBankName.Text = currentBankName;
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
                string checkQuery = "SELECT COUNT(*) FROM Bank WHERE BankName = @BankName AND BankID != @BankID";
                db.cmd.CommandText = checkQuery;
                db.cmd.Parameters.Clear();
                db.cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@BankID", this.bankIdToUpdate);
                int count = Convert.ToInt32(db.cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Энэ банкны нэр өөр банканд аль хэдийн бүртгэгдсэн байна.", "Давхардсан мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBankName.Focus();
                    return;
                }

                string updateQuery = "UPDATE Bank SET BankName = @BankName WHERE BankID = @BankID";
                db.cmd.CommandText = updateQuery;
                db.cmd.Parameters.Clear();
                db.cmd.Parameters.AddWithValue("@BankName", txtBankName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@BankID", this.bankIdToUpdate);

                int rowsAffected = db.cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Банкны мэдээлэл амжилттай шинэчлэгдлээ!", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Банкны мэдээллийг шинэчлэхэд алдаа гарлаа эсвэл өөрчлөлт хийгдсэнгүй.", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Банк шинэчлэхэд алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}