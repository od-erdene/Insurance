// UserControlDocumentsType.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UserControlDocumentsType : UserControl
    {
        public UserControlDocumentsType()
        {
            InitializeComponent();
            Load += UserControlDocumentsType_Load;
        }

        private void UserControlDocumentsType_Load(object sender, EventArgs e)
        {
            LoadDocumentsTypes();
        }

        private void LoadDocumentsTypes()
        {
            DB db = null; 
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT DocumentsID, DocumentsName FROM Documents ORDER BY DocumentsName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                if (grid.Columns.Contains("DocumentsName"))
                    grid.Columns["DocumentsName"].HeaderText = "Баримт бичгийн төрөл";
                if (grid.Columns.Contains("DocumentsID"))
                    grid.Columns["DocumentsID"].Visible = false; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Баримт бичгийн төрлийг ачаалахад алдаа гарлаа: " + ex.Message);
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
            var form = new AddDocumentsTypeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDocumentsTypes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["DocumentsID"].Value);
                string name = grid.CurrentRow.Cells["DocumentsName"].Value.ToString();

                var form = new UpdateDocumentsTypeForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadDocumentsTypes();
                }
            }
            else
            {
                MessageBox.Show("Засах баримт бичгийн төрлийг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["DocumentsID"].Value);
                DialogResult result = MessageBox.Show("Та энэ баримт бичгийн төрлийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null; 
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM Documents WHERE DocumentsID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadDocumentsTypes();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) 
                        {
                            MessageBox.Show("Энэ баримт бичгийн төрөлтэй холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
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
                MessageBox.Show("Устгах баримт бичгийн төрлийг сонгоно уу.");
            }
        }
    }
}