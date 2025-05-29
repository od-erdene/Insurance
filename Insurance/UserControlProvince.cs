// UserControlProvince.cs
using System;
using System.Data;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UserControlProvince : UserControl
    {
        public UserControlProvince()
        {
            InitializeComponent();
            Load += UserControlProvince_Load;
        }

        private void UserControlProvince_Load(object sender, EventArgs e)
        {
            LoadProvinces();
        }

        private void LoadProvinces()
        {
            DB db = new DB();
            db.cmd.CommandText = "SELECT ProvinceID, ProvinceName FROM Province";
            DataTable dt = new DataTable();
            dt.Load(db.cmd.ExecuteReader());
            grid.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddProvinceForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProvinces();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["ProvinceID"].Value);
                string name = grid.CurrentRow.Cells["ProvinceName"].Value.ToString();
                var form = new UpdateProvinceForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProvinces();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["ProvinceID"].Value);
                DialogResult result = MessageBox.Show("Та энэ аймгийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = new DB();
                    db.cmd.CommandText = "DELETE FROM Province WHERE ProvinceID = @id";
                    db.cmd.Parameters.AddWithValue("@id", id);
                    db.cmd.ExecuteNonQuery();
                    LoadProvinces();
                }
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }
    }
}

