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
    public partial class Investigator : Form
    {
        public Investigator()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Witness witness = new Witness();
            witness.ShowDialog();
            this.Hide();
        }

        private void Investigator_Load(object sender, EventArgs e)
        {

        }
    }
}
