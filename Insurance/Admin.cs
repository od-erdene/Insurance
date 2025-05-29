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

            if (e.Node == null)
            {
                return;
            }

            UserControl controlToLoad = null;
            Type controlTypeToInstantiate = null;

            string nodeName = e.Node.Name;
            splitContainer1.Panel2.Controls.Clear();

            if (e.Node.Name == "Node0")
            {
                controlTypeToInstantiate = typeof(UserControlAccident);
            }
            if (e.Node.Name == "Node1")
            {
                controlTypeToInstantiate = typeof(UserControlEmployee);
            }
            if (e.Node.Name == "Node2")
            {
                controlTypeToInstantiate = typeof(UserControlCustomer);
            }
            if (e.Node.Name == "Node3")
            {
                controlTypeToInstantiate = typeof(UserControlDocuments);
            }
            if (e.Node.Name == "Node5")
            {
                controlTypeToInstantiate = typeof(UserControlProvince);
            }
            if (e.Node.Name == "Node6")
            {
                controlTypeToInstantiate = typeof(UserControlCommittee);
            }
            if (e.Node.Name == "Node9")
            {
                controlTypeToInstantiate = typeof(UserControlCallType);
            }
            if (e.Node.Name == "Node10")
            {
                controlTypeToInstantiate = typeof(UserControlCallTimeType);
            }
            if (e.Node.Name == "Node11")
            {
                controlTypeToInstantiate = typeof(UserControlDocumentsType);
            }
            if (e.Node.Name == "Node12")
            {
                controlTypeToInstantiate = typeof(UserControlBank);
            }
            if (e.Node.Name == "Node13")
            {
                controlTypeToInstantiate = typeof(UserControlBankAccountType);
            }
            if (e.Node.Name == "Node14")
            {
                controlTypeToInstantiate = typeof(UserControlPropertyType);
            }
            if (e.Node.Name == "Node15")
            {
                controlTypeToInstantiate = typeof(UserControlInvestigatorOrganization);
            }
            if (controlTypeToInstantiate != null)
            {
                try
                {
                    controlToLoad = (UserControl)Activator.CreateInstance(controlTypeToInstantiate);

                    controlToLoad.Dock = DockStyle.Fill;

                    splitContainer1.Panel2.Controls.Add(controlToLoad);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading control for node '{nodeName}': {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    splitContainer1.Panel2.Controls.Clear();
                }
            }
            else
            {

            }
        }
    }

    }
