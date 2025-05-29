using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Insurance;

namespace Insurance
{
    public partial class Register : Form
    {
        private int customerId = 1;

        public Register()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Accident accident = new Accident();
            accident.ShowDialog();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            LoadPropertyTypes();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            string lastName = txtLastName.Text.Trim();

            if (!string.IsNullOrEmpty(lastName))
            {
                try
                {
                    using (DB db = new DB())
                    {
                        db.cmd.CommandText = "UPDATE Customer SET CustomerLastName = @CustomerLastName WHERE CustomerID = @id";
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@CustomerLastName", lastName);
                        db.cmd.Parameters.AddWithValue("@id", customerId);
                        db.cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating last name: " + ex.Message);
                }
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text.Trim();

            if (!string.IsNullOrEmpty(firstName))
            {
                try
                {
                    using (DB db = new DB())
                    {
                        db.cmd.CommandText = "UPDATE Customer SET CustomerFirstName = @CustomerFirstName WHERE CustomerID = @id";
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@CustomerFirstName", firstName);
                        db.cmd.Parameters.AddWithValue("@id", customerId);
                        db.cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating first name: " + ex.Message);
                }
            }
        }

        private void txtPhone1_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = txtPhone1.Text.Trim();

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                try
                {
                    using (DB db = new DB())
                    {
                        db.cmd.CommandText = "UPDATE Customer SET CustomerPhoneNumber1 = @CustomerPhoneNumber1 WHERE CustomerID = @id";
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber1", phoneNumber);
                        db.cmd.Parameters.AddWithValue("@id", customerId);
                        db.cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating phone number: " + ex.Message);
                }
            }
        }

        private void txtPhone2_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber2 = txtPhone2.Text.Trim();

            if (!string.IsNullOrEmpty(phoneNumber2))
            {
                try
                {
                    using (DB db = new DB())
                    {
                        db.cmd.CommandText = "UPDATE Customer SET CustomerPhoneNumber2 = @CustomerPhoneNumber2 WHERE CustomerID = @id";
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber2", phoneNumber2);
                        db.cmd.Parameters.AddWithValue("@id", customerId);
                        db.cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating phone number: " + ex.Message);
                }
            }
        }

        private void LoadPropertyTypes()
        {
            try
            {
                using (DB db = new DB())
                {
                    db.cmd.CommandText = "SELECT PropertyTypeID, PropertyTypeName FROM PropertyType";
                    db.ada.SelectCommand = db.cmd;
                    DataTable dt = new DataTable();
                    db.ada.Fill(dt);

                    cmbPropertyType.DataSource = dt;
                    cmbPropertyType.DisplayMember = "PropertyTypeName";
                    cmbPropertyType.ValueMember = "PropertyTypeID";
                    cmbPropertyType.SelectedIndex = -1; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading property types: " + ex.Message);
            }
        }


        private void cmbPropertyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPropertyType.SelectedIndex != -1)
            {
                int selectedId = Convert.ToInt32(cmbPropertyType.SelectedValue);
                string selectedName = cmbPropertyType.Text;

                Console.WriteLine($"Selected: {selectedName} (ID: {selectedId})");
            }
        }
    }
}
