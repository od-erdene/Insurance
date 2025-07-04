﻿// UserControlCallTimeType.cs
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SqlException

namespace Insurance
{
    public partial class UserControlCallTimeType : UserControl
    {
        public UserControlCallTimeType()
        {
            InitializeComponent();
            Load += UserControlCallTimeType_Load;
        }

        private void UserControlCallTimeType_Load(object sender, EventArgs e)
        {
            LoadCallTimeTypes();
        }

        private void LoadCallTimeTypes()
        {
            DB db = null; // Initialize to null
            try
            {
                db = new DB();
                db.cmd.CommandText = "SELECT CallTimeTypeID, CallTimeTypeName FROM CallTimeType ORDER BY CallTimeTypeName";
                DataTable dt = new DataTable();
                dt.Load(db.cmd.ExecuteReader());
                grid.DataSource = dt;

                // Optionally, rename column headers for better display
                if (grid.Columns.Contains("CallTimeTypeName"))
                    grid.Columns["CallTimeTypeName"].HeaderText = "Дуудлагын цагийн төрөл";
                if (grid.Columns.Contains("CallTimeTypeID"))
                    grid.Columns["CallTimeTypeID"].Visible = false; // Hide ID column
            }
            catch (Exception ex)
            {
                MessageBox.Show("Дуудлагын цагийн төрөл ачаалахад алдаа гарлаа: " + ex.Message);
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
            var form = new AddCallTimeTypeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCallTimeTypes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CallTimeTypeID"].Value);
                string name = grid.CurrentRow.Cells["CallTimeTypeName"].Value.ToString();

                var form = new UpdateCallTimeTypeForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCallTimeTypes();
                }
            }
            else
            {
                MessageBox.Show("Засах дуудлагын цагийн төрлийг сонгоно уу.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CallTimeTypeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ дуудлагын цагийн төрлийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = null; // Initialize to null
                    try
                    {
                        db = new DB();
                        db.cmd.CommandText = "DELETE FROM CallTimeType WHERE CallTimeTypeID = @id";
                        db.cmd.Parameters.AddWithValue("@id", id);
                        db.cmd.ExecuteNonQuery();
                        MessageBox.Show("Амжилттай устгагдлаа!");
                        LoadCallTimeTypes();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547) // Foreign Key Violation
                        {
                            MessageBox.Show("Энэ дуудлагын цагийн төрөлтэй холбоотой өгөгдөл байгаа тул устгах боломжгүй.");
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
                MessageBox.Show("Устгах дуудлагын цагийн төрлийг сонгоно уу.");
            }
        }
    }
}