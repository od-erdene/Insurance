// UserControlCommittee.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UserControlCommittee : UserControl
    {
        public UserControlCommittee()
        {
            InitializeComponent();
            Load += UserControlCommittee_Load;
        }

        private void UserControlCommittee_Load(object sender, EventArgs e)
        {
            LoadCommittees();
        }

        private void LoadCommittees()
        {
            DB db = null; 
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT c.CommitteeID, c.CommitteeName, p.ProvinceName, c.ProvinceID FROM Committee c JOIN Province p ON c.ProvinceID = p.ProvinceID ORDER BY c.CommitteeName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                if (grid.Columns.Contains("CommitteeName"))
                    grid.Columns["CommitteeName"].HeaderText = "Хороо/Сумын Нэр";
                if (grid.Columns.Contains("ProvinceName"))
                    grid.Columns["ProvinceName"].HeaderText = "Аймаг/Хотын Нэр";
                if (grid.Columns.Contains("CommitteeID"))
                    grid.Columns["CommitteeID"].Visible = false; // Hide ID column
                if (grid.Columns.Contains("ProvinceID"))
                    grid.Columns["ProvinceID"].Visible = false; // Hide ProvinceID column
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хороо/баг ачаалахад алдаа гарлаа: " + ex.Message);
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddCommitteeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCommittees();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CommitteeID"].Value);
                string name = grid.CurrentRow.Cells["CommitteeName"].Value.ToString();
                int provinceId = Convert.ToInt32(grid.CurrentRow.Cells["ProvinceID"].Value);

                var form = new UpdateCommitteeForm(id, name, provinceId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCommittees();
                }
            }
            else
            {
                MessageBox.Show("Засах хороо/багийг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CommitteeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ хороо/багийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null; // Initialize to null
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM Committee WHERE CommitteeID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadCommittees();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key Violation
                        {
                            MessageBox.Show("Энэ хороо/багтай холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
                        }
                        else
                        {
                            MessageBox.Show("Устгахад алдаа гарлаа: " + ex.Message);
                        }
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
            }
            else
            {
                MessageBox.Show("Устгах хороо/багийг сонгоно уу.");
            }
        }
    }
}