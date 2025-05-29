namespace Insurance
{
    partial class Offender
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOffenderName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOffenderPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOffenderAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOtherInfo = new System.Windows.Forms.TextBox();
            this.cmbDistrict = new System.Windows.Forms.ComboBox();
            this.cmbKhoroo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(97, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(611, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "БУРУУТАЙ ТАЛЫН МЭДЭЭЛЭЛ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Овог нэр";
            // 
            // txtOffenderName
            // 
            this.txtOffenderName.Location = new System.Drawing.Point(128, 201);
            this.txtOffenderName.Name = "txtOffenderName";
            this.txtOffenderName.Size = new System.Drawing.Size(329, 31);
            this.txtOffenderName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Утасны дугаар";
            // 
            // txtOffenderPhone
            // 
            this.txtOffenderPhone.Location = new System.Drawing.Point(128, 325);
            this.txtOffenderPhone.Name = "txtOffenderPhone";
            this.txtOffenderPhone.Size = new System.Drawing.Size(329, 31);
            this.txtOffenderPhone.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Хаяг:";
            // 
            // txtOffenderAddress
            // 
            this.txtOffenderAddress.Location = new System.Drawing.Point(411, 458);
            this.txtOffenderAddress.Name = "txtOffenderAddress";
            this.txtOffenderAddress.Size = new System.Drawing.Size(329, 31);
            this.txtOffenderAddress.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 525);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Бусад мэдээлэл";
            // 
            // txtOtherInfo
            // 
            this.txtOtherInfo.Location = new System.Drawing.Point(128, 589);
            this.txtOtherInfo.Name = "txtOtherInfo";
            this.txtOtherInfo.Size = new System.Drawing.Size(329, 31);
            this.txtOtherInfo.TabIndex = 8;
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.Location = new System.Drawing.Point(128, 456);
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.Size = new System.Drawing.Size(121, 33);
            this.cmbDistrict.TabIndex = 9;
            // 
            // cmbKhoroo
            // 
            this.cmbKhoroo.FormattingEnabled = true;
            this.cmbKhoroo.Location = new System.Drawing.Point(271, 458);
            this.cmbKhoroo.Name = "cmbKhoroo";
            this.cmbKhoroo.Size = new System.Drawing.Size(121, 33);
            this.cmbKhoroo.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 676);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 76);
            this.button1.TabIndex = 11;
            this.button1.Text = "Дараах";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Offender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 831);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbKhoroo);
            this.Controls.Add(this.cmbDistrict);
            this.Controls.Add(this.txtOtherInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOffenderAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOffenderPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOffenderName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Offender";
            this.Text = "Offender";
            this.Load += new System.EventHandler(this.Offender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOffenderName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOffenderPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOffenderAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOtherInfo;
        private System.Windows.Forms.ComboBox cmbDistrict;
        private System.Windows.Forms.ComboBox cmbKhoroo;
        private System.Windows.Forms.Button button1;
    }
}