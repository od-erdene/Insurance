namespace Insurance
{
    partial class Admin
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Тохиолдлууд");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Ажилчид");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Хэрэглэгчид");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Тохиолдлуудын бичиг баримтууд");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Дүүрэг Сум");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Сум Хороо");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Байршлууд", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Дуудлагын төрөл");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Дуудлага бүртгүүлсэн хугацааны төрөл");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Бичиг баримтууд");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Банкууд");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Банкны дансны эздийн төрлүүд");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Эд хөрөнгийн төрлүүд");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Мөрдөн шалгах байгууллагууд");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Бусад мэдээллүүд", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 266;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Тохиолдлууд";
            treeNode2.Name = "Node1";
            treeNode2.Text = "Ажилчид";
            treeNode3.Name = "Node2";
            treeNode3.Text = "Хэрэглэгчид";
            treeNode4.Name = "Node3";
            treeNode4.Text = "Тохиолдлуудын бичиг баримтууд";
            treeNode5.Name = "Node5";
            treeNode5.Text = "Дүүрэг Сум";
            treeNode6.Name = "Node6";
            treeNode6.Text = "Сум Хороо";
            treeNode7.Name = "Node4";
            treeNode7.Text = "Байршлууд";
            treeNode8.Name = "Node9";
            treeNode8.Text = "Дуудлагын төрөл";
            treeNode9.Name = "Node10";
            treeNode9.Text = "Дуудлага бүртгүүлсэн хугацааны төрөл";
            treeNode10.Name = "Node11";
            treeNode10.Text = "Бичиг баримтууд";
            treeNode11.Name = "Node12";
            treeNode11.Text = "Банкууд";
            treeNode12.Name = "Node13";
            treeNode12.Text = "Банкны дансны эздийн төрлүүд";
            treeNode13.Name = "Node14";
            treeNode13.Text = "Эд хөрөнгийн төрлүүд";
            treeNode14.Name = "Node15";
            treeNode14.Text = "Мөрдөн шалгах байгууллагууд";
            treeNode15.Name = "Node8";
            treeNode15.Text = "Бусад мэдээллүүд";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode7,
            treeNode15});
            this.treeView1.Size = new System.Drawing.Size(266, 450);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Admin";
            this.Text = "Admin";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
    }
}