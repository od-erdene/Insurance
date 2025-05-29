// UserControlBankAccountType.cs
using System;
using System.Data;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UserControlBankAccountType : UserControl
    {
        public UserControlBankAccountType()
        {
            InitializeComponent();
            Load += UserControlBankAccountType_Load;
        }

        private void UserControlBankAccountType_Load(object sender, EventArgs e)
        {
            LoadBankAccountTypes();
        }

        private void LoadBankAccountTypes()
        {
            DB db = new DB();
            db.cmd.CommandText = "SELECT BankAccountTypeID, BankAccountTypeName FROM BankAccountType";
            DataTable dt = new DataTable();
            dt.Load(db.cmd.ExecuteReader());
            grid.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddBankAccountTypeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadBankAccountTypes();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["BankAccountTypeID"].Value);
                string name = grid.CurrentRow.Cells["BankAccountTypeName"].Value.ToString();
                var form = new UpdateBankAccountTypeForm(id, name);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadBankAccountTypes();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.CurrentRow != null)
            {
                int id = Convert.ToInt32(grid.CurrentRow.Cells["BankAccountTypeID"].Value);
                DialogResult result = MessageBox.Show("Та энэ төрлийг устгахдаа итгэлтэй байна уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DB db = new DB();
                    db.cmd.CommandText = "DELETE FROM BankAccountType WHERE BankAccountTypeID = @id";
                    db.cmd.Parameters.AddWithValue("@id", id);
                    db.cmd.ExecuteNonQuery();
                    LoadBankAccountTypes();
                }
            }
        }

        private void UserControlBankAccountType_Load_1(object sender, EventArgs e)
        {

        }
    }
}
