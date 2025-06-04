// UserControlPropertyType.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UserControlPropertyType : UserControl
    {
        public UserControlPropertyType()
        {
            InitializeComponent();
            Load += UserControlPropertyType_Load;
        }

        private void UserControlPropertyType_Load(object sender, EventArgs e)
        {
            LoadPropertyTypes();
        }

        private void LoadPropertyTypes()
        {
            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT PropertyTypeID, PropertyTypeName FROM PropertyType ORDER BY PropertyTypeName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                // Optionally, rename column headers for better display
                if (grid.Columns.Contains("PropertyTypeName"))
                    grid.Columns["PropertyTypeName"].HeaderText = "Эд хөрөнгийн төрөл";
                if (grid.Columns.Contains("PropertyTypeID"))
                    grid.Columns["PropertyTypeID"].Visible = false; // Hide ID column
            }
            catch (Exception ex)
            {
                MessageBox.Show("Эд хөрөнгийн төрлийг ачаалахад алдаа гарлаа: " + ex.Message);
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
            var form = new AddPropertyTypeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPropertyTypes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["PropertyTypeID"].Value);
                string name = grid.CurrentRow.Cells["PropertyTypeName"].Value.ToString();

                var form = new UpdatePropertyTypeForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPropertyTypes();
                }
            }
            else
            {
                MessageBox.Show("Засах эд хөрөнгийн төрлийг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["PropertyTypeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ эд хөрөнгийн төрлийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null; // Initialize to null
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM PropertyType WHERE PropertyTypeID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadPropertyTypes();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key Violation
                        {
                            MessageBox.Show("Энэ эд хөрөнгийн төрөлтэй холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
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
                MessageBox.Show("Устгах эд хөрөнгийн төрлийг сонгоно уу.");
            }
        }
    }
}