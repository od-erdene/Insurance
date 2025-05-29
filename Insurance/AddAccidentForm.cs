using System;
using System.Data;
using System.Windows.Forms;

namespace Insurance
{
    public partial class AddAccidentForm : Form
    {

        public AddAccidentForm()
        {
            InitializeComponent();
        }

        private void AddAccidentForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            dtpAccidentTime.Value = DateTime.Now;
            dtpCallTime.Value = DateTime.Now;
            dtpAppliedDate.Value = DateTime.Today;
            dtpCallDate.Value = DateTime.Today;
            dtpAccidentDate.Value = DateTime.Today;
            chkContactedInvestigator_CheckedChanged(null, null);
        }

        private void LoadComboBoxData()
        {
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
            DB db = null;
            try
            {
                db = new DB();
                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear();

                db.ada = new System.Data.SqlClient.SqlDataAdapter(db.cmd);
                DataTable dt = new DataTable();
                db.ada.Fill(dt);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
                comboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data for {comboBox.Name}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkContactedInvestigator_CheckedChanged(object sender, EventArgs e)
        {
            bool contacted = chkContactedInvestigator.Checked;
            txtInvestigatorName.Enabled = contacted;
            txtInvestigatorPhoneNumber.Enabled = contacted;
            cmbInvestigatorOrganization.Enabled = contacted;
            txtReasonForNotContacting.Enabled = !contacted;

            if (!contacted && (sender != null))
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccidentName.Text))
            {
                MessageBox.Show("Accident Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentName.Focus();
                return;
            }

            DB db = null;
            try
            {
                db = new DB();
                string query = @"
                    INSERT INTO Accident (
                        AccidentName, AccidentLocation, AccidentDate, AccidentTime, AccidentDetails,
                        CallTimeTypeID, PropertyTypeID, ContactedInvestigator, ReasonForNotContactingInvestigator,
                        InvestigatorName, InvestigatorPhoneNumber, InvestigatorOrganizationID, AppliedDate,
                        ApplcationSignature, OffenderName, OffenderPhoneNumber, OffenderRegisterNumber,
                        OffenderCommitteeID, OffenderAddress, WitnessName, WitnessPhoneNumber,
                        WitnessRegisterNumber, WitnessCommitteeID, WitnessAddress, BankAccountNumber,
                        BankAccountTypeID, BankID, BankAccountOwnerName, BankAccountDetails, CallTypeID,
                        CallDate, CallTime, CallEmployeeID
                    ) VALUES (
                        @AccidentName, @AccidentLocation, @AccidentDate, @AccidentTime, @AccidentDetails,
                        @CallTimeTypeID, @PropertyTypeID, @ContactedInvestigator, @ReasonForNotContactingInvestigator,
                        @InvestigatorName, @InvestigatorPhoneNumber, @InvestigatorOrganizationID, @AppliedDate,
                        @ApplcationSignature, @OffenderName, @OffenderPhoneNumber, @OffenderRegisterNumber,
                        @OffenderCommitteeID, @OffenderAddress, @WitnessName, @WitnessPhoneNumber,
                        @WitnessRegisterNumber, @WitnessCommitteeID, @WitnessAddress, @BankAccountNumber,
                        @BankAccountTypeID, @BankID, @BankAccountOwnerName, @BankAccountDetails, @CallTypeID,
                        @CallDate, @CallTime, @CallEmployeeID
                    )";

                db.cmd.CommandText = query;
                db.cmd.Parameters.Clear();

                db.cmd.Parameters.AddWithValue("@AccidentName", txtAccidentName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@AccidentLocation", string.IsNullOrWhiteSpace(txtAccidentLocation.Text) ? (object)DBNull.Value : txtAccidentLocation.Text.Trim());
                db.cmd.Parameters.AddWithValue("@AccidentDate", dtpAccidentDate.Value.Date);
                db.cmd.Parameters.AddWithValue("@AccidentTime", dtpAccidentTime.Value.TimeOfDay);
                db.cmd.Parameters.AddWithValue("@AccidentDetails", string.IsNullOrWhiteSpace(txtAccidentDetails.Text) ? (object)DBNull.Value : txtAccidentDetails.Text.Trim());

                db.cmd.Parameters.AddWithValue("@CallTimeTypeID", cmbCallTimeType.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@PropertyTypeID", cmbPropertyType.SelectedValue ?? (object)DBNull.Value);

                db.cmd.Parameters.AddWithValue("@ContactedInvestigator", chkContactedInvestigator.Checked);
                if (chkContactedInvestigator.Checked)
                {
                    db.cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", (object)DBNull.Value);
                    db.cmd.Parameters.AddWithValue("@InvestigatorName", string.IsNullOrWhiteSpace(txtInvestigatorName.Text) ? (object)DBNull.Value : txtInvestigatorName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", string.IsNullOrWhiteSpace(txtInvestigatorPhoneNumber.Text) ? (object)DBNull.Value : txtInvestigatorPhoneNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", cmbInvestigatorOrganization.SelectedValue ?? (object)DBNull.Value);
                }
                else
                {
                    db.cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", string.IsNullOrWhiteSpace(txtReasonForNotContacting.Text) ? (object)DBNull.Value : txtReasonForNotContacting.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorName", (object)DBNull.Value);
                    db.cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", (object)DBNull.Value);
                    db.cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", (object)DBNull.Value);
                }

                db.cmd.Parameters.AddWithValue("@AppliedDate", dtpAppliedDate.Value.Date);
                db.cmd.Parameters.AddWithValue("@ApplcationSignature", string.IsNullOrWhiteSpace(txtApplicationSignature.Text) ? (object)DBNull.Value : txtApplicationSignature.Text.Trim());

                db.cmd.Parameters.AddWithValue("@OffenderName", string.IsNullOrWhiteSpace(txtOffenderName.Text) ? (object)DBNull.Value : txtOffenderName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@OffenderPhoneNumber", string.IsNullOrWhiteSpace(txtOffenderPhoneNumber.Text) ? (object)DBNull.Value : txtOffenderPhoneNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@OffenderRegisterNumber", string.IsNullOrWhiteSpace(txtOffenderRegisterNumber.Text) ? (object)DBNull.Value : txtOffenderRegisterNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@OffenderCommitteeID", cmbOffenderCommittee.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@OffenderAddress", string.IsNullOrWhiteSpace(txtOffenderAddress.Text) ? (object)DBNull.Value : txtOffenderAddress.Text.Trim());

                db.cmd.Parameters.AddWithValue("@WitnessName", string.IsNullOrWhiteSpace(txtWitnessName.Text) ? (object)DBNull.Value : txtWitnessName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@WitnessPhoneNumber", string.IsNullOrWhiteSpace(txtWitnessPhoneNumber.Text) ? (object)DBNull.Value : txtWitnessPhoneNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@WitnessRegisterNumber", string.IsNullOrWhiteSpace(txtWitnessRegisterNumber.Text) ? (object)DBNull.Value : txtWitnessRegisterNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@WitnessCommitteeID", cmbWitnessCommittee.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@WitnessAddress", string.IsNullOrWhiteSpace(txtWitnessAddress.Text) ? (object)DBNull.Value : txtWitnessAddress.Text.Trim());

                db.cmd.Parameters.AddWithValue("@BankAccountNumber", string.IsNullOrWhiteSpace(txtBankAccountNumber.Text) ? (object)DBNull.Value : txtBankAccountNumber.Text.Trim());
                db.cmd.Parameters.AddWithValue("@BankAccountTypeID", cmbBankAccountType.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@BankID", cmbBank.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@BankAccountOwnerName", string.IsNullOrWhiteSpace(txtBankAccountOwnerName.Text) ? (object)DBNull.Value : txtBankAccountOwnerName.Text.Trim());
                db.cmd.Parameters.AddWithValue("@BankAccountDetails", string.IsNullOrWhiteSpace(txtBankAccountDetails.Text) ? (object)DBNull.Value : txtBankAccountDetails.Text.Trim());

                db.cmd.Parameters.AddWithValue("@CallTypeID", cmbCallType.SelectedValue ?? (object)DBNull.Value);
                db.cmd.Parameters.AddWithValue("@CallDate", dtpCallDate.Value.Date);
                db.cmd.Parameters.AddWithValue("@CallTime", dtpCallTime.Value.TimeOfDay);
                db.cmd.Parameters.AddWithValue("@CallEmployeeID", cmbCallEmployee.SelectedValue ?? (object)DBNull.Value);

                db.cmd.ExecuteNonQuery();
                MessageBox.Show("Accident added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add accident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}