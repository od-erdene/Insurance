using System;
using System.Data;
using System.Windows.Forms;

namespace Insurance
{
    public partial class UserControlAccident : UserControl
    {

        public UserControlAccident()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void UserControlAccident_Load(object sender, EventArgs e)
        {
            LoadAccidentData();
        }

        public void LoadAccidentData()
        {
            DB db = null; // Declare here to ensure it's accessible in catch/finally if needed, though not strictly necessary with current DB class
            try
            {
                db = new DB(); // Instantiates and opens connection
                string query = @"
                    SELECT
                        A.AccidentID, A.AccidentName, A.AccidentLocation, A.AccidentDate, A.AccidentTime,
                        CTT.CallTimeTypeName AS CallTimeType, PT.PropertyTypeName AS PropertyType,
                        A.ContactedInvestigator, A.InvestigatorName,
                        IO.InvestigationOrganizationName AS InvestigationOrganization, A.AppliedDate,
                        A.OffenderName, OC.CommitteeName AS OffenderCommittee,
                        A.WitnessName, WC.CommitteeName AS WitnessCommittee,
                        BAT.BankAccountTypeName AS BankAccountType, B.BankName, A.BankAccountOwnerName,
                        CT.CallTypeName AS CallType, A.CallDate, A.CallTime,
                        Emp.EmployeeFirstName + ' ' + Emp.EmployeeLastName AS CallEmployeeName,
                        A.AccidentDetails, A.ReasonForNotContactingInvestigator, A.InvestigatorPhoneNumber,
                        A.ApplcationSignature, A.OffenderPhoneNumber, A.OffenderRegisterNumber,
                        A.OffenderAddress, A.WitnessPhoneNumber, A.WitnessRegisterNumber,
                        A.WitnessAddress, A.BankAccountNumber, A.BankAccountDetails
                    FROM Accident A
                    LEFT JOIN CallTimeType CTT ON A.CallTimeTypeID = CTT.CallTimeTypeID
                    LEFT JOIN PropertyType PT ON A.PropertyTypeID = PT.PropertyTypeID
                    LEFT JOIN InvestigationOrganization IO ON A.InvestigatorOrganizationID = IO.InvestigationOrganizationID
                    LEFT JOIN Committee OC ON A.OffenderCommitteeID = OC.CommitteeID
                    LEFT JOIN Committee WC ON A.WitnessCommitteeID = WC.CommitteeID
                    LEFT JOIN BankAccountType BAT ON A.BankAccountTypeID = BAT.BankAccountTypeID
                    LEFT JOIN Bank B ON A.BankID = B.BankID
                    LEFT JOIN CallType CT ON A.CallTypeID = CT.CallTypeID
                    LEFT JOIN Employee Emp ON A.CallEmployeeID = Emp.EmployeeID
                    ORDER BY A.AccidentID DESC;";

                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear(); 
                db.ada = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataTable dt = new DataTable();
                db.ada.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load accident data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add Button
        {
            using (AddAccidentForm addForm = new AddAccidentForm()) // AddAccidentForm will use its own DB instance
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAccidentData();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Update Button
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int accidentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AccidentID"].Value);
                    // UpdateAccidentForm will use its own DB instance
                    using (UpdateAccidentForm updateForm = new UpdateAccidentForm(accidentId))
                    {
                        if (updateForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadAccidentData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error preparing update: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an accident to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Delete Button
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete this accident and all its related documents?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DB db = null;
                    System.Data.SqlClient.SqlTransaction transaction = null;
                    try
                    {
                        db = new DB(); // Instantiates and opens connection
                        transaction = db.con.BeginTransaction();
                        db.cmd.Transaction = transaction; // Assign transaction to command

                        int accidentId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AccidentID"].Value);

                        // First, delete related documents
                        string deleteDocsQuery = "DELETE FROM AccidentDocuments WHERE AccidentID = @AccidentID";
                        db.cmd.CommandText = deleteDocsQuery;
                        db.cmd.Parameters.Clear();
                        db.cmd.Parameters.AddWithValue("@AccidentID", accidentId);
                        db.cmd.ExecuteNonQuery();

                        // Then, delete the accident
                        string deleteAccidentQuery = "DELETE FROM Accident WHERE AccidentID = @AccidentID";
                        db.cmd.CommandText = deleteAccidentQuery;
                        db.cmd.Parameters.Clear(); // Clear parameters from previous command
                        db.cmd.Parameters.AddWithValue("@AccidentID", accidentId);
                        int rowsAffected = db.cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Accident deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAccidentData();
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Accident not found or already deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (transaction != null) transaction.Rollback();
                        MessageBox.Show("Failed to delete accident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an accident to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}