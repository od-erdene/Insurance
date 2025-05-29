using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insurance
{
    public partial class Bank : Form
    {
        public Bank()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Call call = new Call();
            call.Show();
            this.Hide();
        }

        private void Bank_Load(object sender, EventArgs e)
        {

        }
    }
}
