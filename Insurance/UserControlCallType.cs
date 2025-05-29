// UserControlCallType.cs
using System;
using System.Data;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UserControlCallType : UserControl
    {
        public UserControlCallType()
        {
            InitializeComponent();
            Load += UserControlCallType_Load;
        }

        private void UserControlCallType_Load(object sender, EventArgs e)
        {
            LoadCallTypes();
        }

        private void LoadCallTypes()
        {
            DB db = new DB();
            db.cmd.CommandText = "SELECT CallTypeID, CallTypeName FROM CallType";
            DataTable dt = new DataTable();
            dt.Load(db.cmd.ExecuteReader());
            grid.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddCallTypeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCallTypes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CallTypeID"].Value);
                string name = grid.CurrentRow.Cells["CallTypeName"].Value.ToString();
                var form = new UpdateCallTypeForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCallTypes();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["CallTypeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ төрлийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = new DB();
                    db.cmd.CommandText = "DELETE FROM CallType WHERE CallTypeID = @id";
                    db.cmd.Parameters.AddWithValue("@id", id);
                    db.cmd.ExecuteNonQuery();
                    LoadCallTypes();
                }
            }
        }
    }
}
