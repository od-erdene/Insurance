// UpdateCustomerForm.Designer.cs
namespace Insurance
{
    partial class UpdateCustomerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblRegisterNumber;
        private System.Windows.Forms.TextBox txtRegisterNumber;
        private System.Windows.Forms.Label lblPhoneNumber1;
        private System.Windows.Forms.TextBox txtPhoneNumber1;
        private System.Windows.Forms.Label lblPhoneNumber2;
        private System.Windows.Forms.TextBox txtPhoneNumber2;
        private System.Windows.Forms.Label lblCommittee;
        private System.Windows.Forms.ComboBox cmbCommittee;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
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
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblRegisterNumber = new System.Windows.Forms.Label();
            this.txtRegisterNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber1 = new System.Windows.Forms.Label();
            this.txtPhoneNumber1 = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber2 = new System.Windows.Forms.Label();
            this.txtPhoneNumber2 = new System.Windows.Forms.TextBox();
            this.lblCommittee = new System.Windows.Forms.Label();
            this.cmbCommittee = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(12, 25);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(43, 16);
            this.lblLastName.TabIndex = 0;
            this.lblLastName.Text = "Овог:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(140, 22);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(250, 22);
            this.txtLastName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(12, 55);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(32, 16);
            this.lblFirstName.TabIndex = 2;
            this.lblFirstName.Text = "Нэр:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(140, 52);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(250, 22);
            this.txtFirstName.TabIndex = 3;
            // 
            // lblRegisterNumber
            // 
            this.lblRegisterNumber.AutoSize = true;
            this.lblRegisterNumber.Location = new System.Drawing.Point(12, 85);
            this.lblRegisterNumber.Name = "lblRegisterNumber";
            this.lblRegisterNumber.Size = new System.Drawing.Size(108, 16);
            this.lblRegisterNumber.TabIndex = 4;
            this.lblRegisterNumber.Text = "Регистрийн дугаар:";
            // 
            // txtRegisterNumber
            // 
            this.txtRegisterNumber.Location = new System.Drawing.Point(140, 82);
            this.txtRegisterNumber.Name = "txtRegisterNumber";
            this.txtRegisterNumber.Size = new System.Drawing.Size(250, 22);
            this.txtRegisterNumber.TabIndex = 5;
            // 
            // lblPhoneNumber1
            // 
            this.lblPhoneNumber1.AutoSize = true;
            this.lblPhoneNumber1.Location = new System.Drawing.Point(12, 115);
            this.lblPhoneNumber1.Name = "lblPhoneNumber1";
            this.lblPhoneNumber1.Size = new System.Drawing.Size(107, 16);
            this.lblPhoneNumber1.TabIndex = 6;
            this.lblPhoneNumber1.Text = "Утас 1:";
            // 
            // txtPhoneNumber1
            // 
            this.txtPhoneNumber1.Location = new System.Drawing.Point(140, 112);
            this.txtPhoneNumber1.Name = "txtPhoneNumber1";
            this.txtPhoneNumber1.Size = new System.Drawing.Size(250, 22);
            this.txtPhoneNumber1.TabIndex = 7;
            // 
            // lblPhoneNumber2
            // 
            this.lblPhoneNumber2.AutoSize = true;
            this.lblPhoneNumber2.Location = new System.Drawing.Point(12, 145);
            this.lblPhoneNumber2.Name = "lblPhoneNumber2";
            this.lblPhoneNumber2.Size = new System.Drawing.Size(107, 16);
            this.lblPhoneNumber2.TabIndex = 8;
            this.lblPhoneNumber2.Text = "Утас 2:";
            // 
            // txtPhoneNumber2
            // 
            this.txtPhoneNumber2.Location = new System.Drawing.Point(140, 142);
            this.txtPhoneNumber2.Name = "txtPhoneNumber2";
            this.txtPhoneNumber2.Size = new System.Drawing.Size(250, 22);
            this.txtPhoneNumber2.TabIndex = 9;
            // 
            // lblCommittee
            // 
            this.lblCommittee.AutoSize = true;
            this.lblCommittee.Location = new System.Drawing.Point(12, 175);
            this.lblCommittee.Name = "lblCommittee";
            this.lblCommittee.Size = new System.Drawing.Size(46, 16);
            this.lblCommittee.TabIndex = 10;
            this.lblCommittee.Text = "Хороо:";
            // 
            // cmbCommittee
            // 
            this.cmbCommittee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommittee.FormattingEnabled = true;
            this.cmbCommittee.Location = new System.Drawing.Point(140, 172);
            this.cmbCommittee.Name = "cmbCommittee";
            this.cmbCommittee.Size = new System.Drawing.Size(250, 24);
            this.cmbCommittee.TabIndex = 11;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 205);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(46, 16);
            this.lblAddress.TabIndex = 12;
            this.lblAddress.Text = "Хаяг:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(140, 202);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(250, 22);
            this.txtAddress.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(140, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 25);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Хадгалах";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(235, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Болих";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UpdateCustomerForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 280);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblRegisterNumber);
            this.Controls.Add(this.txtRegisterNumber);
            this.Controls.Add(this.lblPhoneNumber1);
            this.Controls.Add(this.txtPhoneNumber1);
            this.Controls.Add(this.lblPhoneNumber2);
            this.Controls.Add(this.txtPhoneNumber2);
            this.Controls.Add(this.lblCommittee);
            this.Controls.Add(this.cmbCommittee);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Харилцагч засах";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}