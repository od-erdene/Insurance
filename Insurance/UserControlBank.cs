using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UserControlBank : UserControl
    {
        public UserControlBank()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            LoadBankData();
        }
        public void LoadBankData()
        {
            DB db = null; 
            try
            {
                db = new DB(); 
                string query = @"
                    SELECT
                        BankID,BankName
                    FROM Bank";

                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear();
                db.ada = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataTable dt = new DataTable();
                db.ada.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load accident data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AddBankForm addForm = new AddBankForm()) 
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBankData();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int bankId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BankID"].Value);
                    using (UpdateBankForm updateForm = new UpdateBankForm())
                    {
                        if (updateForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadBankData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error preparing update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a bank to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
        }
    }
}
