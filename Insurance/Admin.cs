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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();

            UserControl selectedReport = null;

            switch (e.Node.Index)
            {
                case 1:
                    selectedReport = new UserControlAccident();
                    break;
                case 2:
                    selectedReport = new UserControlAccident();
                    break;
                case 3:
                    selectedReport = new UserControlAccident();
                    break;
                case 4:
                    selectedReport = new UserControlAccident();
                    break;
                case 6:
                    selectedReport = new UserControlAccident();
                    break;
                case 7:
                    selectedReport = new UserControlAccident();
                    break;
                case 9:
                    selectedReport = new UserControlAccident();
                    break;
                case 10:
                    selectedReport = new UserControlAccident();
                    break;
                case 11:
                    selectedReport = new UserControlAccident();
                    break;
                case 12:
                    selectedReport = new UserControlAccident();
                    break;
                case 13:
                    selectedReport = new UserControlAccident();
                    break;
                case 14:
                    selectedReport = new UserControlAccident();
                    break;
                case 15:
                    selectedReport = new UserControlAccident();
                    break;
                default:
                    break;
            }

            if (selectedReport != null)
            {
                selectedReport.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(selectedReport);
            }
        }
    }

    }
