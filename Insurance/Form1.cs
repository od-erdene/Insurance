﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.ShowDialog();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminLogin admin = new AdminLogin();
            admin.ShowDialog();
            this.Hide();
        }
    }
}
