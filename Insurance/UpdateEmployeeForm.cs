// UpdateEmployeeForm.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class UpdateEmployeeForm : Form
    {
        private int employeeId;

        public UpdateEmployeeForm(int id, string lastName, string firstName, string phoneNumber, string address, int committeeId, string username, string password)
        {
            InitializeComponent();
            this.employeeId = id;
            txtLastName.Text = lastName;
            txtFirstName.Text = firstName;
            txtPhoneNumber.Text = phoneNumber;
            txtAddress.Text = address;
            txtUsername.Text = username;
            txtPassword.Text = password; // Should not display actual password in real apps, only for demo

            LoadComboBoxes();
            cmbCommittee.SelectedValue = committeeId; // Set committee
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
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                cmbCommittee.SelectedValue == null)
            {
                MessageBox.Show("Бүх заавал бөглөх талбарыг бөглөнө үү.");
                return;
            }

            DB db = null;
            try
            {
                db = new DB();
                db.cmd.CommandText = "UPDATE Employee SET EmployeeLastName = @lastName, EmployeeFirstName = @firstName, EmployeePhoneNumber = @phoneNumber, EmployeeAddress = @address, CommitteID = @committeeID, EmployeeUsername = @username, EmployeePassword = @password WHERE EmployeeID = @id";
                db.cmd.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                db.cmd.Parameters.AddWithValue("@committeeID", Convert.ToInt32(cmbCommittee.SelectedValue));
                db.cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                db.cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim()); // In a real app, hash this password
                db.cmd.Parameters.AddWithValue("@id", employeeId);
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