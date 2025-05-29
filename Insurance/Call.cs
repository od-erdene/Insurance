using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace Insurance
{
    public partial class Call : Form
    {
        private DB db; // Instance of your DB class

        public Call()
        {
            InitializeComponent();
            db = new DB();
            LoadCallTypeComboBox();
        }

        // ... existing code ...

        private void LoadCallTypeComboBox()
        {
            try
            {
                using (DB db = new DB())
                {
                    string query = "SELECT CallTypeID, CallTypeName FROM CallType";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, db.con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "CallTypeName";
                    comboBox1.ValueMember = "CallTypeID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load call types: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string input = textBox2.Text;

            // Regular expression for 24-hour time format HH:mm
            Regex timeFormat = new Regex(@"^(?:[01]\d|2[0-3]):[0-5]\d$");

            if (!timeFormat.IsMatch(input))
            {
                textBox2.BackColor = Color.MistyRose; // indicate error visually
                                                      // Optionally, show a tooltip or warning
                toolTip1.SetToolTip(textBox2, "Please enter time in HH:mm format (00:00 to 23:59)");
            }
            else
            {
                textBox2.BackColor = Color.White; // reset color when valid
                toolTip1.SetToolTip(textBox2, "");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            Regex timeFormat = new Regex(@"^(?:[01]\d|2[0-3]):[0-5]\d$");

            if (!timeFormat.IsMatch(input))
            {
                textBox1.BackColor = Color.MistyRose;
                toolTip1.SetToolTip(textBox1, "Please enter time in HH:mm format (00:00 to 23:59)");
            }
            else
            {
                textBox1.BackColor = Color.White;
                toolTip1.SetToolTip(textBox1, "");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application application = new Application();
            application.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Document document = new Document();
            document.Show();
            this.Hide();
        }

    }
}