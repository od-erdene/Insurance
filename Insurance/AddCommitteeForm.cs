// AddCommitteeForm.cs
using System;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class AddCommitteeForm : Form
    {
        public AddCommitteeForm()
        {
            InitializeComponent();
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
                        cmbProvince.Items.Add(new ComboBoxItem(dr["ProvinceName"].ToString(), Convert.ToInt32(dr["ProvinceID"])));
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
                db.cmd.CommandText = "INSERT INTO Committee (CommitteeName, ProvinceID) VALUES (@name, @provinceId)";
                db.cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@provinceId", ((ComboBoxItem)cmbProvince.SelectedItem).Value);
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
    }
}