namespace Insurance
{
    partial class UserControlProvince
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView grid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(22, 290);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Нэмэх";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(112, 290);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Засах";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(202, 290);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Устгах";
            // 
            // grid
            // 
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.ColumnHeadersHeight = 29;
            this.grid.Location = new System.Drawing.Point(22, 17);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(400, 250);
            this.grid.TabIndex = 3;
            // 
            // UserControlProvince
            // 
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grid);
            this.Name = "UserControlProvince";
            this.Size = new System.Drawing.Size(450, 350);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }
    }
}