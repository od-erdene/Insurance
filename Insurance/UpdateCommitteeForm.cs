// UpdateCommitteeForm.cs
using System;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UpdateCommitteeForm : Form
    {
        private int committeeId;
        private int currentProvinceId;

        public UpdateCommitteeForm(int id, string committeeName, int provinceId)
        {
            InitializeComponent();
            this.committeeId = id;
            this.currentProvinceId = provinceId;
            txtName.Text = committeeName;
            LoadProvinces();
        }

        private void LoadProvinces()
        {
            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT ProvinceID, ProvinceName FROM Province ORDER BY ProvinceName";
                using (SqlDataReader dr = db.cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ComboBoxItem item = new ComboBoxItem(dr["ProvinceName"].ToString(), Convert.ToInt32(dr["ProvinceID"]));
                        cmbProvince.Items.Add(item);
                        if (item.Value == currentProvinceId)
                        {
                            cmbProvince.SelectedItem = item;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Аймаг ачаалахад алдаа гарлаа: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Хороо/багийн нэрийг оруулна уу.");
                return;
            }

            if (cmbProvince.SelectedItem == null)
            {
                MessageBox.Show("Аймаг сонгоно уу.");
                return;
            }

            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "UPDATE Committee SET CommitteeName = @name, ProvinceID = @provinceId WHERE CommitteeID = @id";
                db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@provinceId", ((ComboBoxItem)cmbProvince.SelectedItem).Value);
                db.cmd.Parameters.AddWithValue("@id", committeeId);
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

        // Helper class for ComboBox items
        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void UpdateCommitteeForm_Load(object sender, EventArgs e)
        {

        }
    }
}