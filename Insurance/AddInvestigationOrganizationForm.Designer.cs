﻿// AddInvestigationOrganizationForm.Designer.cs
namespace Insurance
{
    partial class AddInvestigationOrganizationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 28);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(158, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Мөрдөн байцаах байгууллага:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(190, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(180, 20);
            this.txtName.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(190, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(285, 60);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddInvestigatorOrganizationForm
            // 
            this.ClientSize = new System.Drawing.Size(390, 110);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddInvestigatorOrganizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Мөрдөн байцаах байгууллага нэмэх";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}