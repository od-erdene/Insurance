namespace Insurance
{
    partial class Witness
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
            this.txtWitnessName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWitnessRegNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWitnessPhoneNumber = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.cmbDistrict = new System.Windows.Forms.ComboBox();
            this.cmbKhoroo = new System.Windows.Forms.ComboBox();
            this.txtWitnessAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(111, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "ГЭРЧИЙН ТУХАЙ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Гэрчийн овог нэр";
            // 
            // txtWitnessName
            // 
            this.txtWitnessName.Location = new System.Drawing.Point(133, 170);
            this.txtWitnessName.Name = "txtWitnessName";
            this.txtWitnessName.Size = new System.Drawing.Size(449, 31);
            this.txtWitnessName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = " Регистрийн дугаар";
            // 
            // txtWitnessRegNumber
            // 
            this.txtWitnessRegNumber.Location = new System.Drawing.Point(133, 292);
            this.txtWitnessRegNumber.Name = "txtWitnessRegNumber";
            this.txtWitnessRegNumber.Size = new System.Drawing.Size(449, 31);
            this.txtWitnessRegNumber.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 355);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Хаяг:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 478);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Утасны дугаар:";
            // 
            // txtWitnessPhoneNumber
            // 
            this.txtWitnessPhoneNumber.Location = new System.Drawing.Point(133, 528);
            this.txtWitnessPhoneNumber.Name = "txtWitnessPhoneNumber";
            this.txtWitnessPhoneNumber.Size = new System.Drawing.Size(449, 31);
            this.txtWitnessPhoneNumber.TabIndex = 8;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(392, 596);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(190, 74);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "Дараах";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.Location = new System.Drawing.Point(133, 405);
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.Size = new System.Drawing.Size(121, 33);
            this.cmbDistrict.TabIndex = 10;
            // 
            // cmbKhoroo
            // 
            this.cmbKhoroo.FormattingEnabled = true;
            this.cmbKhoroo.Location = new System.Drawing.Point(284, 405);
            this.cmbKhoroo.Name = "cmbKhoroo";
            this.cmbKhoroo.Size = new System.Drawing.Size(121, 33);
            this.cmbKhoroo.TabIndex = 11;
            // 
            // txtWitnessAddress
            // 
            this.txtWitnessAddress.Location = new System.Drawing.Point(447, 407);
            this.txtWitnessAddress.Name = "txtWitnessAddress";
            this.txtWitnessAddress.Size = new System.Drawing.Size(449, 31);
            this.txtWitnessAddress.TabIndex = 12;
            // 
            // Witness
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 811);
            this.Controls.Add(this.txtWitnessAddress);
            this.Controls.Add(this.cmbKhoroo);
            this.Controls.Add(this.cmbDistrict);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtWitnessPhoneNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWitnessRegNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWitnessName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Witness";
            this.Text = "Witness";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWitnessName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWitnessRegNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWitnessPhoneNumber;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox cmbDistrict;
        private System.Windows.Forms.ComboBox cmbKhoroo;
        private System.Windows.Forms.TextBox txtWitnessAddress;
    }
}