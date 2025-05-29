using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Insurance
{
    public partial class Application : Form
    {
        public int accidentId;
        public Application()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text;

            if (accidentId <= 0) return; 

            using (DB db = new DB())
            {
                try
                {
                    string query = "UPDATE Accident SET ApplicationSignature = @signature WHERE AccidentID = @id";

                    db.cmd.CommandText = query;
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@signature", input);
                    db.cmd.Parameters.AddWithValue("@id", accidentId);

                    db.cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating ApplicationSignature: " + ex.Message);
                }
            }
        }

        private void Application_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
