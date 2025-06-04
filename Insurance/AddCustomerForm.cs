// AddCustomerForm.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
            Load += AddCustomerForm_Load;
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            DB db = null;
            try
            {
                db = new DB();

                // Load Committees
                db.cmd.CommandText = "SELECT CommitteeID, CommitteeName FROM Committee ORDER BY CommitteeName";
                DataTable dtCommittees = new DataTable();
                dtCommittees.Load(db.cmd.ExecuteReader());
                cmbCommittee.DataSource = dtCommittees;
                cmbCommittee.DisplayMember = "CommitteeName";
                cmbCommittee.ValueMember = "CommitteeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Өгөгдөл ачаалахад алдаа гарлаа: " + ex.Message);
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
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtRegisterNumber.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber1.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) || cmbCommittee.SelectedValue == null)
            {
                MessageBox.Show("Бүх заавал бөглөх талбарыг бөглөнө үү.");
                return;
            }

            DB db = null;
            try
            {
                db = new DB();
                db.cmd.CommandText = "INSERT INTO Customer (CustomerLastName, CustomerFirstName, CustomerRegisterNumber, CustomerPhoneNumber1, CustomerPhoneNumber2, CommitteeID, CustomerAddress) VALUES (@lastName, @firstName, @registerNumber, @phone1, @phone2, @committeeID, @address)";
                db.cmd.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@registerNumber", txtRegisterNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@phone1", txtPhoneNumber1.Text.Trim());
                db.cmd.Parameters.AddWithValue("@phone2", txtPhoneNumber2.Text.Trim());
                db.cmd.Parameters.AddWithValue("@committeeID", Convert.ToInt32(cmbCommittee.SelectedValue));
                db.cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
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