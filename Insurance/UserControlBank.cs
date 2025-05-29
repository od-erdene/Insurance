using System;
using System.Data;
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
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
                        BankID, BankName AS [Банкны нэр]  -- Aliasing column header to Mongolian
                    FROM Bank
                    ORDER BY BankName;"; 

                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear();
                db.ada = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataTable dt = new DataTable();
                db.ada.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns.Contains("BankID"))
                {
                    dataGridView1.Columns["BankID"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("Банкны нэр"))
                {
                    dataGridView1.Columns["Банкны нэр"].HeaderText = "Банкны нэр"; 
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Банкны мэдээлэл ачааллахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string currentBankName = Convert.ToString(dataGridView1.SelectedRows[0].Cells["Банкны нэр"].Value);

                    using (UpdateBankForm updateForm = new UpdateBankForm(bankId, currentBankName))
                    {
                        if (updateForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadBankData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Шинэчлэхэд бэлдэхэд алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Шинэчлэх банкаа сонгоно уу.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string bankName = dataGridView1.SelectedRows[0].Cells["Банкны нэр"].Value.ToString();
                if (MessageBox.Show($"'{bankName}' банкийг устгахдаа итгэлтэй байна уу?", "Устгахыг баталгаажуулна уу", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DB db = null;
                    System.Data.SqlClient.SqlTransaction transaction = null;
                    try
                    {
                        db = new DB();
                        int bankId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["BankID"].Value);

                        string checkQuery = "SELECT COUNT(*) FROM Accident WHERE BankID = @BankID";
                        db.cmd.CommandText = checkQuery;
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@BankID", bankId);
                        int relatedRecordsCount = Convert.ToInt32(db.cmd.ExecuteScalar());

                        if (relatedRecordsCount > 0)
                        {
                            MessageBox.Show($"'{bankName}' банк нь осолтой холбоотой бүртгэлтэй тул устгах боломжгүй.", "Устгах боломжгүй", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        transaction = db.con.BeginTransaction();
                        db.cmd.Transaction = transaction;

                        string deleteQuery = "DELETE FROM Bank WHERE BankID = @BankID";
                        db.cmd.CommandText = deleteQuery;
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@BankID", bankId);
                        int rowsAffected = db.cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Банк амжилттай устгагдлаа.", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBankData();
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Банк олдсонгүй эсвэл аль хэдийн устгагдсан байна.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null) transaction.Rollback();
                        MessageBox.Show("Банк устгахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Устгах банкаа сонгоно уу.", "Мэдээлэл", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}