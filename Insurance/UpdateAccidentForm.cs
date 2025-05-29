using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class UpdateAccidentForm : Form
    {
        // TODO: Replace with your actual connection string
        private string connectionString = "YOUR_CONNECTION_STRING_HERE";
        private int accidentIdToUpdate;

        public UpdateAccidentForm(int accidentId)
        {
            InitializeComponent();
            this.accidentIdToUpdate = accidentId;
        }

        private void UpdateAccidentForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadAccidentDetails();
            // Handle initial state of investigator fields based on loaded data
            chkContactedInvestigator_CheckedChanged(null, null);
        }

        private void LoadComboBoxData()
        {
            // This method is identical to the one in AddAccidentForm.cs
            LoadGenericComboBox(cmbCallTimeType, "SELECT CallTimeTypeID, CallTimeTypeName FROM CallTimeType ORDER BY CallTimeTypeName", "CallTimeTypeName", "CallTimeTypeID");
            LoadGenericComboBox(cmbPropertyType, "SELECT PropertyTypeID, PropertyTypeName FROM PropertyType ORDER BY PropertyTypeName", "PropertyTypeName", "PropertyTypeID");
            LoadGenericComboBox(cmbInvestigatorOrganization, "SELECT InvestigationOrganizationID, InvestigationOrganizationName FROM InvestigationOrganization ORDER BY InvestigationOrganizationName", "InvestigationOrganizationName", "InvestigationOrganizationID");
            LoadGenericComboBox(cmbOffenderCommittee, "SELECT CommitteeID, CommitteeName FROM Committee ORDER BY CommitteeName", "CommitteeName", "CommitteeID");
            LoadGenericComboBox(cmbWitnessCommittee, "SELECT CommitteeID, CommitteeName FROM Committee ORDER BY CommitteeName", "CommitteeName", "CommitteeID");
            LoadGenericComboBox(cmbBankAccountType, "SELECT BankAccountTypeID, BankAccountTypeName FROM BankAccountType ORDER BY BankAccountTypeName", "BankAccountTypeName", "BankAccountTypeID");
            LoadGenericComboBox(cmbBank, "SELECT BankID, BankName FROM Bank ORDER BY BankName", "BankName", "BankID");
            LoadGenericComboBox(cmbCallType, "SELECT CallTypeID, CallTypeName FROM CallType ORDER BY CallTypeName", "CallTypeName", "CallTypeID");
            LoadGenericComboBox(cmbCallEmployee, "SELECT EmployeeID, EmployeeFirstName + ' ' + EmployeeLastName AS FullName FROM Employee ORDER BY FullName", "FullName", "EmployeeID");
        }

        private void LoadGenericComboBox(ComboBox comboBox, string query, string displayMember, string valueMember)
        {
            // This method is identical to the one in AddAccidentForm.cs
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data for {comboBox.Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkContactedInvestigator_CheckedChanged(object sender, EventArgs e)
        {
            // This method is identical to the one in AddAccidentForm.cs
            bool contacted = chkContactedInvestigator.Checked;
            txtInvestigatorName.Enabled = contacted;
            txtInvestigatorPhoneNumber.Enabled = contacted;
            cmbInvestigatorOrganization.Enabled = contacted;
            txtReasonForNotContacting.Enabled = !contacted;

            if (!contacted && (sender != null)) // Avoid clearing on initial load if already cleared by LoadAccidentDetails
            {
                txtInvestigatorName.Clear();
                txtInvestigatorPhoneNumber.Clear();
                cmbInvestigatorOrganization.SelectedIndex = -1;
            }
            else if (contacted && (sender != null))
            {
                txtReasonForNotContacting.Clear();
            }
        }

        private void LoadAccidentDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Accident WHERE AccidentID = @AccidentID";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@AccidentID", this.accidentIdToUpdate);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        txtAccidentName.Text = row["AccidentName"].ToString();
                        txtAccidentLocation.Text = row["AccidentLocation"].ToString();
                        dtpAccidentDate.Value = Convert.ToDateTime(row["AccidentDate"]);
                        if (row["AccidentTime"] != DBNull.Value && row["AccidentTime"] is TimeSpan)
                        {
                            TimeSpan accidentTime = (TimeSpan)row["AccidentTime"];
                            dtpAccidentTime.Value = DateTime.Today.Add(accidentTime);
                        }
                        txtAccidentDetails.Text = row["AccidentDetails"].ToString();

                        cmbCallTimeType.SelectedValue = row["CallTimeTypeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["CallTimeTypeID"]);
                        cmbPropertyType.SelectedValue = row["PropertyTypeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["PropertyTypeID"]);

                        chkContactedInvestigator.Checked = Convert.ToBoolean(row["ContactedInvestigator"]);
                        if (chkContactedInvestigator.Checked)
                        {
                            txtInvestigatorName.Text = row["InvestigatorName"].ToString();
                            txtInvestigatorPhoneNumber.Text = row["InvestigatorPhoneNumber"].ToString();
                            cmbInvestigatorOrganization.SelectedValue = row["InvestigatorOrganizationID"] == DBNull.Value ? -1 : Convert.ToInt32(row["InvestigatorOrganizationID"]);
                            txtReasonForNotContacting.Clear(); // Ensure this is clear if contacted
                        }
                        else
                        {
                            txtReasonForNotContacting.Text = row["ReasonForNotContactingInvestigator"].ToString();
                            txtInvestigatorName.Clear();
                            txtInvestigatorPhoneNumber.Clear();
                            cmbInvestigatorOrganization.SelectedIndex = -1;
                        }

                        dtpAppliedDate.Value = Convert.ToDateTime(row["AppliedDate"]);
                        txtApplicationSignature.Text = row["ApplcationSignature"].ToString();

                        // Offender
                        txtOffenderName.Text = row["OffenderName"].ToString();
                        txtOffenderPhoneNumber.Text = row["OffenderPhoneNumber"].ToString();
                        txtOffenderRegisterNumber.Text = row["OffenderRegisterNumber"].ToString();
                        cmbOffenderCommittee.SelectedValue = row["OffenderCommitteeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["OffenderCommitteeID"]);
                        txtOffenderAddress.Text = row["OffenderAddress"].ToString();

                        // Witness
                        txtWitnessName.Text = row["WitnessName"].ToString();
                        txtWitnessPhoneNumber.Text = row["WitnessPhoneNumber"].ToString();
                        txtWitnessRegisterNumber.Text = row["WitnessRegisterNumber"].ToString();
                        cmbWitnessCommittee.SelectedValue = row["WitnessCommitteeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["WitnessCommitteeID"]);
                        txtWitnessAddress.Text = row["WitnessAddress"].ToString();

                        // Bank Account
                        txtBankAccountNumber.Text = row["BankAccountNumber"].ToString();
                        cmbBankAccountType.SelectedValue = row["BankAccountTypeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["BankAccountTypeID"]);
                        cmbBank.SelectedValue = row["BankID"] == DBNull.Value ? -1 : Convert.ToInt32(row["BankID"]);
                        txtBankAccountOwnerName.Text = row["BankAccountOwnerName"].ToString();
                        txtBankAccountDetails.Text = row["BankAccountDetails"].ToString();

                        // Call Details
                        cmbCallType.SelectedValue = row["CallTypeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["CallTypeID"]);
                        dtpCallDate.Value = Convert.ToDateTime(row["CallDate"]);
                        if (row["CallTime"] != DBNull.Value && row["CallTime"] is TimeSpan)
                        {
                            TimeSpan callTime = (TimeSpan)row["CallTime"];
                            dtpCallTime.Value = DateTime.Today.Add(callTime);
                        }
                        cmbCallEmployee.SelectedValue = row["CallEmployeeID"] == DBNull.Value ? -1 : Convert.ToInt32(row["CallEmployeeID"]);
                    }
                    else
                    {
                        MessageBox.Show("Accident not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load accident details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: Add comprehensive validation for all fields
            if (string.IsNullOrWhiteSpace(txtAccidentName.Text))
            {
                MessageBox.Show("Accident Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentName.Focus();
                return;
            }
            // Add more validation...

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                        UPDATE Accident SET
                            AccidentName = @AccidentName, AccidentLocation = @AccidentLocation, AccidentDate = @AccidentDate,
                            AccidentTime = @AccidentTime, AccidentDetails = @AccidentDetails, CallTimeTypeID = @CallTimeTypeID,
                            PropertyTypeID = @PropertyTypeID, ContactedInvestigator = @ContactedInvestigator,
                            ReasonForNotContactingInvestigator = @ReasonForNotContactingInvestigator, InvestigatorName = @InvestigatorName,
                            InvestigatorPhoneNumber = @InvestigatorPhoneNumber, InvestigatorOrganizationID = @InvestigatorOrganizationID,
                            AppliedDate = @AppliedDate, ApplcationSignature = @ApplcationSignature, OffenderName = @OffenderName,
                            OffenderPhoneNumber = @OffenderPhoneNumber, OffenderRegisterNumber = @OffenderRegisterNumber,
                            OffenderCommitteeID = @OffenderCommitteeID, OffenderAddress = @OffenderAddress, WitnessName = @WitnessName,
                            WitnessPhoneNumber = @WitnessPhoneNumber, WitnessRegisterNumber = @WitnessRegisterNumber,
                            WitnessCommitteeID = @WitnessCommitteeID, WitnessAddress = @WitnessAddress, BankAccountNumber = @BankAccountNumber,
                            BankAccountTypeID = @BankAccountTypeID, BankID = @BankID, BankAccountOwnerName = @BankAccountOwnerName,
                            BankAccountDetails = @BankAccountDetails, CallTypeID = @CallTypeID, CallDate = @CallDate,
                            CallTime = @CallTime, CallEmployeeID = @CallEmployeeID
                        WHERE AccidentID = @AccidentID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@AccidentID", this.accidentIdToUpdate);
                        cmd.Parameters.AddWithValue("@AccidentName", txtAccidentName.Text.Trim());
                        cmd.Parameters.AddWithValue("@AccidentLocation", string.IsNullOrWhiteSpace(txtAccidentLocation.Text) ? (object)DBNull.Value : txtAccidentLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@AccidentDate", dtpAccidentDate.Value.Date);
                        cmd.Parameters.AddWithValue("@AccidentTime", dtpAccidentTime.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@AccidentDetails", string.IsNullOrWhiteSpace(txtAccidentDetails.Text) ? (object)DBNull.Value : txtAccidentDetails.Text.Trim());

                        cmd.Parameters.AddWithValue("@CallTimeTypeID", cmbCallTimeType.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@PropertyTypeID", cmbPropertyType.SelectedValue ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@ContactedInvestigator", chkContactedInvestigator.Checked);
                        if (chkContactedInvestigator.Checked)
                        {
                            cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InvestigatorName", string.IsNullOrWhiteSpace(txtInvestigatorName.Text) ? (object)DBNull.Value : txtInvestigatorName.Text.Trim());
                            cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", string.IsNullOrWhiteSpace(txtInvestigatorPhoneNumber.Text) ? (object)DBNull.Value : txtInvestigatorPhoneNumber.Text.Trim());
                            cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", cmbInvestigatorOrganization.SelectedValue ?? (object)DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", string.IsNullOrWhiteSpace(txtReasonForNotContacting.Text) ? (object)DBNull.Value : txtReasonForNotContacting.Text.Trim());
                            cmd.Parameters.AddWithValue("@InvestigatorName", (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", (object)DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@AppliedDate", dtpAppliedDate.Value.Date);
                        cmd.Parameters.AddWithValue("@ApplcationSignature", string.IsNullOrWhiteSpace(txtApplicationSignature.Text) ? (object)DBNull.Value : txtApplicationSignature.Text.Trim());

                        cmd.Parameters.AddWithValue("@OffenderName", string.IsNullOrWhiteSpace(txtOffenderName.Text) ? (object)DBNull.Value : txtOffenderName.Text.Trim());
                        cmd.Parameters.AddWithValue("@OffenderPhoneNumber", string.IsNullOrWhiteSpace(txtOffenderPhoneNumber.Text) ? (object)DBNull.Value : txtOffenderPhoneNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@OffenderRegisterNumber", string.IsNullOrWhiteSpace(txtOffenderRegisterNumber.Text) ? (object)DBNull.Value : txtOffenderRegisterNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@OffenderCommitteeID", cmbOffenderCommittee.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@OffenderAddress", string.IsNullOrWhiteSpace(txtOffenderAddress.Text) ? (object)DBNull.Value : txtOffenderAddress.Text.Trim());

                        cmd.Parameters.AddWithValue("@WitnessName", string.IsNullOrWhiteSpace(txtWitnessName.Text) ? (object)DBNull.Value : txtWitnessName.Text.Trim());
                        cmd.Parameters.AddWithValue("@WitnessPhoneNumber", string.IsNullOrWhiteSpace(txtWitnessPhoneNumber.Text) ? (object)DBNull.Value : txtWitnessPhoneNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@WitnessRegisterNumber", string.IsNullOrWhiteSpace(txtWitnessRegisterNumber.Text) ? (object)DBNull.Value : txtWitnessRegisterNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@WitnessCommitteeID", cmbWitnessCommittee.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@WitnessAddress", string.IsNullOrWhiteSpace(txtWitnessAddress.Text) ? (object)DBNull.Value : txtWitnessAddress.Text.Trim());

                        cmd.Parameters.AddWithValue("@BankAccountNumber", string.IsNullOrWhiteSpace(txtBankAccountNumber.Text) ? (object)DBNull.Value : txtBankAccountNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@BankAccountTypeID", cmbBankAccountType.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BankID", cmbBank.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BankAccountOwnerName", string.IsNullOrWhiteSpace(txtBankAccountOwnerName.Text) ? (object)DBNull.Value : txtBankAccountOwnerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@BankAccountDetails", string.IsNullOrWhiteSpace(txtBankAccountDetails.Text) ? (object)DBNull.Value : txtBankAccountDetails.Text.Trim());

                        cmd.Parameters.AddWithValue("@CallTypeID", cmbCallType.SelectedValue ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CallDate", dtpCallDate.Value.Date);
                        cmd.Parameters.AddWithValue("@CallTime", dtpCallTime.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@CallEmployeeID", cmbCallEmployee.SelectedValue ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Accident updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update accident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}