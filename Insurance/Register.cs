using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace Insurance
{
    public partial class Register : Form
    {
        private int currentCustomerId = 0;
        private int currentAccidentId = 0;

        public Register()
        {
            InitializeComponent();
            this.Text = "Эд хөрөнгийн даатгалын нөхөн төлбөрийн өргөдлийн маягт";
        }

        private void Register_Load(object sender, EventArgs e)
        {
            LoadProvinces();
            LoadPropertyTypes();
            LoadInvestigatorOrganizations();
            LoadBankAccountTypes();
            LoadBankNames();

            // Initialize ComboBoxes for Committees to be empty until Province is selected
            cmbCustomerCommittee.DataSource = null;
            cmbWitnessCommittee.DataSource = null;
            cmbOffenderCommittee.DataSource = null;

            // Set default state for investigator contact
            chkContactedInvestigator_CheckedChanged(null, null);
            dtpAccidentDate.Value = DateTime.Today;
            dtpApplicationDate.Value = DateTime.Today;
        }

        private void LoadProvinces()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT ProvinceID, ProvinceName FROM Province ORDER BY ProvinceName";
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "Provinces");

                    // Customer Province
                    cmbCustomerProvince.DataSource = db.ds.Tables["Provinces"].Copy(); // Use Copy() for independent sources
                    cmbCustomerProvince.DisplayMember = "ProvinceName";
                    cmbCustomerProvince.ValueMember = "ProvinceID";
                    cmbCustomerProvince.SelectedIndex = -1;

                    // Witness Province
                    cmbWitnessProvince.DataSource = db.ds.Tables["Provinces"].Copy();
                    cmbWitnessProvince.DisplayMember = "ProvinceName";
                    cmbWitnessProvince.ValueMember = "ProvinceID";
                    cmbWitnessProvince.SelectedIndex = -1;

                    // Offender Province
                    cmbOffenderProvince.DataSource = db.ds.Tables["Provinces"].Copy();
                    cmbOffenderProvince.DisplayMember = "ProvinceName";
                    cmbOffenderProvince.ValueMember = "ProvinceID";
                    cmbOffenderProvince.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading provinces: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadCommittees(ComboBox cmbProvince, ComboBox cmbCommittee)
        {
            if (cmbProvince.SelectedValue == null)
            {
                cmbCommittee.DataSource = null;
                return;
            }

            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT CommitteeID, CommitteeName FROM Committee WHERE ProvinceID = @ProvinceID ORDER BY CommitteeName";
                    db.cmd.Parameters.Clear(); // Clear previous parameters
                    db.cmd.Parameters.AddWithValue("@ProvinceID", (int)cmbProvince.SelectedValue);
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "Committees");

                    cmbCommittee.DataSource = db.ds.Tables["Committees"];
                    cmbCommittee.DisplayMember = "CommitteeName";
                    cmbCommittee.ValueMember = "CommitteeID";
                    cmbCommittee.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading committees: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbCustomerProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCommittees(cmbCustomerProvince, cmbCustomerCommittee);
        }

        private void cmbWitnessProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCommittees(cmbWitnessProvince, cmbWitnessCommittee);
        }

        private void cmbOffenderProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCommittees(cmbOffenderProvince, cmbOffenderCommittee);
        }

        private void LoadPropertyTypes()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT PropertyTypeID, PropertyTypeName FROM PropertyType ORDER BY PropertyTypeName";
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "PropertyTypes");
                    cmbPropertyType.DataSource = db.ds.Tables["PropertyTypes"];
                    cmbPropertyType.DisplayMember = "PropertyTypeName";
                    cmbPropertyType.ValueMember = "PropertyTypeID";
                    cmbPropertyType.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading property types: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadInvestigatorOrganizations()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT InvestigationOrganizationID, InvestigationOrganizationName FROM InvestigationOrganization ORDER BY InvestigationOrganizationName";
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "InvestigatorOrganizations");
                    cmbInvestigatorOrganization.DataSource = db.ds.Tables["InvestigatorOrganizations"];
                    cmbInvestigatorOrganization.DisplayMember = "InvestigationOrganizationName";
                    cmbInvestigatorOrganization.ValueMember = "InvestigationOrganizationID";
                    cmbInvestigatorOrganization.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading investigator organizations: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadBankAccountTypes()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT BankAccountTypeID, BankAccountTypeName FROM BankAccountType ORDER BY BankAccountTypeName";
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "BankAccountTypes");
                    cmbBankAccountType.DataSource = db.ds.Tables["BankAccountTypes"];
                    cmbBankAccountType.DisplayMember = "BankAccountTypeName";
                    cmbBankAccountType.ValueMember = "BankAccountTypeID";
                    cmbBankAccountType.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading bank account types: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadBankNames()
        {
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT BankID, BankName FROM Bank ORDER BY BankName";
                    db.ada = new SqlDataAdapter(db.cmd);
                    db.ds = new DataSet();
                    db.ada.Fill(db.ds, "Banks");
                    cmbBankName.DataSource = db.ds.Tables["Banks"];
                    cmbBankName.DisplayMember = "BankName";
                    cmbBankName.ValueMember = "BankID";
                    cmbBankName.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading banks: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkContactedInvestigator_CheckedChanged(object sender, EventArgs e)
        {
            bool contacted = chkContactedInvestigator.Checked;
            cmbInvestigatorOrganization.Enabled = contacted;
            txtInvestigatorName.Enabled = contacted;
            txtInvestigatorPhone.Enabled = contacted;
            txtReasonNotContactedInvestigator.Enabled = !contacted;
            if (contacted)
            {
                txtReasonNotContactedInvestigator.Clear();
            }
            else
            {
                cmbInvestigatorOrganization.SelectedIndex = -1;
                txtInvestigatorName.Clear();
                txtInvestigatorPhone.Clear();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerLastName.Text))
            {
                MessageBox.Show("Даатгуулагчийн овог оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerFirstName.Text))
            {
                MessageBox.Show("Даатгуулагчийн нэр оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerFirstName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerPhone1.Text))
            {
                MessageBox.Show("Даатгуулагчийн утасны дугаар 1 оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerPhone1.Focus();
                return false;
            }
            if (cmbPropertyType.SelectedValue == null)
            {
                MessageBox.Show("Эд хөрөнгийн төрөл сонгоно уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPropertyType.Focus();
                return false;
            }

            // Accident Info
            if (string.IsNullOrWhiteSpace(txtAccidentLocation.Text))
            {
                MessageBox.Show("Тохиолдол болсон газар оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentLocation.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAccidentDetails.Text))
            {
                MessageBox.Show("Тохиолдлын талаар дэлгэрэнгүй бичнэ үү.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentDetails.Focus();
                return false;
            }

            // Call Type selection
            if (!chkCallTypeImmediate.Checked && !chkCallTypeAfter3Days.Checked && !chkCallTypeNoCall.Checked)
            {
                MessageBox.Show("Дуудлага өгсөн хэлбэрийг сонгоно уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkCallTypeImmediate.Focus();
                return false;
            }


            // Bank Info (if required for all cases)
            if (cmbBankAccountType.SelectedValue == null || cmbBankName.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtBankAccountHolderName.Text) || string.IsNullOrWhiteSpace(txtBankAccountNumber.Text))
            {
                MessageBox.Show("Банкны мэдээллийг бүрэн оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBankAccountType.Focus();
                return false;
            }

            // Declaration
            if (string.IsNullOrWhiteSpace(txtApplicantSignature.Text))
            {
                MessageBox.Show("Даатгуулагчийн гарын үсэг (нэр) оруулна уу.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApplicantSignature.Focus();
                return false;
            }
            if (!chkAgreeTerms.Checked || !chkAgreePolicy.Checked)
            {
                MessageBox.Show("Мэдүүлгийн нөхцлүүдийг зөвшөөрнө үү.", "Мэдээлэл дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkAgreeTerms.Focus();
                return false;
            }

            return true;
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            using (DB db = new DB())
            {
                SqlTransaction transaction = null;
                try
                {
                    transaction = db.con.BeginTransaction();
                    db.cmd.Transaction = transaction;

                    // 1. Insert Customer
                    db.cmd.CommandText = @"INSERT INTO Customer 
                                           (CustomerLastName, CustomerFirstName, CustomerRegisterNumber, 
                                            CustomerPhoneNumber1, CustomerPhoneNumber2, CommitteeID, CustomerAddress)
                                           OUTPUT INSERTED.CustomerID
                                           VALUES (@CustomerLastName, @CustomerFirstName, @CustomerRegisterNumber,
                                                   @CustomerPhoneNumber1, @CustomerPhoneNumber2, @CommitteeID, @CustomerAddress)";
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@CustomerLastName", txtCustomerLastName.Text);
                    db.cmd.Parameters.AddWithValue("@CustomerFirstName", txtCustomerFirstName.Text);
                    db.cmd.Parameters.AddWithValue("@CustomerRegisterNumber", string.IsNullOrWhiteSpace(txtCustomerRegisterNumber.Text) ? (object)DBNull.Value : txtCustomerRegisterNumber.Text);
                    db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber1", txtCustomerPhone1.Text);
                    db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber2", string.IsNullOrWhiteSpace(txtCustomerPhone2.Text) ? (object)DBNull.Value : txtCustomerPhone2.Text);
                    db.cmd.Parameters.AddWithValue("@CommitteeID", cmbCustomerCommittee.SelectedValue == null ? (object)DBNull.Value : (int)cmbCustomerCommittee.SelectedValue);
                    db.cmd.Parameters.AddWithValue("@CustomerAddress", string.IsNullOrWhiteSpace(txtCustomerAddressDetail.Text) ? (object)DBNull.Value : txtCustomerAddressDetail.Text);

                    currentCustomerId = (int)db.cmd.ExecuteScalar();

                    // 2. Insert Accident
                    int? callTimeTypeId = null;
                    if (chkCallTypeImmediate.Checked) callTimeTypeId = 1; // Assuming CallTimeTypeID 1 = Immediate, 2 = After 3 days, 3 = No Call
                    else if (chkCallTypeAfter3Days.Checked) callTimeTypeId = 2; // Adjust these IDs based on your CallTimeType table
                    else if (chkCallTypeNoCall.Checked) callTimeTypeId = 3;

                    // Parse AccidentTime
                    TimeSpan accidentTime;
                    TimeSpan.TryParse(txtAccidentTime.Text, out accidentTime);


                    db.cmd.CommandText = @"INSERT INTO Accident 
                                           (CustomerID, AccidentLocation, AccidentDate, AccidentTime, AccidentDetails, CallTimeTypeID, PropertyTypeID,
                                            ContactedInvestigator, ReasonForNotContactingInvestigator, InvestigatorName, InvestigatorPhoneNumber, InvestigatorOrganizationID,
                                            AppliedDate, ApplcationSignature, 
                                            OffenderName, OffenderPhoneNumber, OffenderRegisterNumber, OffenderCommitteeID, OffenderAddress,
                                            WitnessName, WitnessPhoneNumber, WitnessRegisterNumber, WitnessCommitteeID, WitnessAddress,
                                            BankAccountNumber, BankAccountTypeID, BankID, BankAccountOwnerName, BankAccountDetails)
                                           OUTPUT INSERTED.AccidentID
                                           VALUES (@CustomerID, @AccidentLocation, @AccidentDate, @AccidentTime, @AccidentDetails, @CallTimeTypeID, @PropertyTypeID,
                                                   @ContactedInvestigator, @ReasonForNotContactingInvestigator, @InvestigatorName, @InvestigatorPhoneNumber, @InvestigatorOrganizationID,
                                                   @AppliedDate, @ApplcationSignature,
                                                   @OffenderName, @OffenderPhoneNumber, @OffenderRegisterNumber, @OffenderCommitteeID, @OffenderAddress,
                                                   @WitnessName, @WitnessPhoneNumber, @WitnessRegisterNumber, @WitnessCommitteeID, @WitnessAddress,
                                                   @BankAccountNumber, @BankAccountTypeID, @BankID, @BankAccountOwnerName, @BankAccountDetails)";
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@CustomerID", currentCustomerId);
                    db.cmd.Parameters.AddWithValue("@AccidentLocation", txtAccidentLocation.Text);
                    db.cmd.Parameters.AddWithValue("@AccidentDate", dtpAccidentDate.Value.Date);
                    db.cmd.Parameters.AddWithValue("@AccidentTime", string.IsNullOrWhiteSpace(txtAccidentTime.Text) ? (object)DBNull.Value : accidentTime);
                    db.cmd.Parameters.AddWithValue("@AccidentDetails", txtAccidentDetails.Text);
                    db.cmd.Parameters.AddWithValue("@CallTimeTypeID", callTimeTypeId == null ? (object)DBNull.Value : callTimeTypeId);
                    // txtCallDelayReason.Text can be appended to AccidentDetails or stored in a new field if needed.
                    db.cmd.Parameters.AddWithValue("@PropertyTypeID", (int)cmbPropertyType.SelectedValue);

                    db.cmd.Parameters.AddWithValue("@ContactedInvestigator", chkContactedInvestigator.Checked);
                    if (chkContactedInvestigator.Checked)
                    {
                        db.cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", cmbInvestigatorOrganization.SelectedValue == null ? (object)DBNull.Value : (int)cmbInvestigatorOrganization.SelectedValue);
                        db.cmd.Parameters.AddWithValue("@InvestigatorName", string.IsNullOrWhiteSpace(txtInvestigatorName.Text) ? (object)DBNull.Value : txtInvestigatorName.Text);
                        db.cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", string.IsNullOrWhiteSpace(txtInvestigatorPhone.Text) ? (object)DBNull.Value : txtInvestigatorPhone.Text);
                        db.cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", (object)DBNull.Value);
                    }
                    else
                    {
                        db.cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", (object)DBNull.Value);
                        db.cmd.Parameters.AddWithValue("@InvestigatorName", (object)DBNull.Value);
                        db.cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", (object)DBNull.Value);
                        db.cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", string.IsNullOrWhiteSpace(txtReasonNotContactedInvestigator.Text) ? (object)DBNull.Value : txtReasonNotContactedInvestigator.Text);
                    }

                    db.cmd.Parameters.AddWithValue("@AppliedDate", dtpApplicationDate.Value.Date);
                    db.cmd.Parameters.AddWithValue("@ApplcationSignature", txtApplicantSignature.Text);

                    db.cmd.Parameters.AddWithValue("@OffenderName", string.IsNullOrWhiteSpace(txtOffenderName.Text) ? (object)DBNull.Value : txtOffenderName.Text);
                    db.cmd.Parameters.AddWithValue("@OffenderPhoneNumber", string.IsNullOrWhiteSpace(txtOffenderPhone.Text) ? (object)DBNull.Value : txtOffenderPhone.Text);
                    db.cmd.Parameters.AddWithValue("@OffenderRegisterNumber", string.IsNullOrWhiteSpace(txtOffenderRegisterNumber.Text) ? (object)DBNull.Value : txtOffenderRegisterNumber.Text);
                    db.cmd.Parameters.AddWithValue("@OffenderCommitteeID", cmbOffenderCommittee.SelectedValue == null ? (object)DBNull.Value : (int)cmbOffenderCommittee.SelectedValue);
                    db.cmd.Parameters.AddWithValue("@OffenderAddress", string.IsNullOrWhiteSpace(txtOffenderAddressDetail.Text) ? (object)DBNull.Value : txtOffenderAddressDetail.Text);

                    db.cmd.Parameters.AddWithValue("@WitnessName", string.IsNullOrWhiteSpace(txtWitnessName.Text) ? (object)DBNull.Value : txtWitnessName.Text);
                    db.cmd.Parameters.AddWithValue("@WitnessPhoneNumber", string.IsNullOrWhiteSpace(txtWitnessPhone.Text) ? (object)DBNull.Value : txtWitnessPhone.Text);
                    db.cmd.Parameters.AddWithValue("@WitnessRegisterNumber", string.IsNullOrWhiteSpace(txtWitnessRegisterNumber.Text) ? (object)DBNull.Value : txtWitnessRegisterNumber.Text);
                    db.cmd.Parameters.AddWithValue("@WitnessCommitteeID", cmbWitnessCommittee.SelectedValue == null ? (object)DBNull.Value : (int)cmbWitnessCommittee.SelectedValue);
                    db.cmd.Parameters.AddWithValue("@WitnessAddress", string.IsNullOrWhiteSpace(txtWitnessAddressDetail.Text) ? (object)DBNull.Value : txtWitnessAddressDetail.Text);

                    db.cmd.Parameters.AddWithValue("@BankAccountNumber", txtBankAccountNumber.Text);
                    db.cmd.Parameters.AddWithValue("@BankAccountTypeID", (int)cmbBankAccountType.SelectedValue);
                    db.cmd.Parameters.AddWithValue("@BankID", (int)cmbBankName.SelectedValue);
                    db.cmd.Parameters.AddWithValue("@BankAccountOwnerName", txtBankAccountHolderName.Text);
                    db.cmd.Parameters.AddWithValue("@BankAccountDetails", string.IsNullOrWhiteSpace(txtBankAdditionalInfo.Text) ? (object)DBNull.Value : txtBankAdditionalInfo.Text);

                    currentAccidentId = (int)db.cmd.ExecuteScalar();

                    Dictionary<CheckBox, int> documentCheckboxes = new Dictionary<CheckBox, int>()
                    {
                        { chkDoc1, 1 }, { chkDoc2, 2 }, { chkDoc3, 3 },
                        { chkDoc4, 4 }, { chkDoc5, 5 }, { chkDoc6, 6 },
                        { chkDoc7, 7 }, { chkDoc8, 8 }, { chkDoc9, 9 }
                    };

                    foreach (var entry in documentCheckboxes)
                    {
                        if (entry.Key.Checked)
                        {
                            db.cmd.CommandText = @"INSERT INTO AccidentDocuments 
                                                   (AccidentID, DocumentID, IsSubmitted, SubmittedAt)
                                                   VALUES (@AccidentID, @DocumentID, @IsSubmitted, @SubmittedAt)";
                            db.cmd.Parameters.Clear();
                            db.cmd.Parameters.AddWithValue("@AccidentID", currentAccidentId);
                            db.cmd.Parameters.AddWithValue("@DocumentID", entry.Value); // Assumed DocumentID
                            db.cmd.Parameters.AddWithValue("@IsSubmitted", true);
                            db.cmd.Parameters.AddWithValue("@SubmittedAt", DateTime.Now);
                            db.cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Өргөдөл амжилттай бүртгэгдлээ!", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(); // Optionally clear the form
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    MessageBox.Show($"Өргөдөл бүртгэхэд алдаа гарлаа: {ex.Message}\n{ex.StackTrace}", "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            // Customer Info
            txtCustomerLastName.Clear();
            txtCustomerFirstName.Clear();
            txtCustomerRegisterNumber.Clear();
            txtCustomerPhone1.Clear();
            txtCustomerPhone2.Clear();
            cmbCustomerProvince.SelectedIndex = -1;
            cmbCustomerCommittee.DataSource = null;
            txtCustomerAddressDetail.Clear();
            cmbPropertyType.SelectedIndex = -1;

            // Accident Info
            txtAccidentLocation.Clear();
            dtpAccidentDate.Value = DateTime.Today;
            txtAccidentTime.Clear();
            chkCallTypeImmediate.Checked = false;
            chkCallTypeAfter3Days.Checked = false;
            chkCallTypeNoCall.Checked = false;
            txtCallDelayReason.Clear();
            txtAccidentDetails.Clear();

            // Investigator Info
            chkContactedInvestigator.Checked = false; // This will also trigger its event handler
            cmbInvestigatorOrganization.SelectedIndex = -1;
            txtInvestigatorName.Clear();
            txtInvestigatorPhone.Clear();
            txtReasonNotContactedInvestigator.Clear();

            // Witness Info
            txtWitnessName.Clear();
            txtWitnessRegisterNumber.Clear();
            cmbWitnessProvince.SelectedIndex = -1;
            cmbWitnessCommittee.DataSource = null;
            txtWitnessAddressDetail.Clear();
            txtWitnessPhone.Clear();

            // Offender Info
            txtOffenderName.Clear();
            txtOffenderRegisterNumber.Clear();
            cmbOffenderProvince.SelectedIndex = -1;
            cmbOffenderCommittee.DataSource = null;
            txtOffenderAddressDetail.Clear();
            txtOffenderPhone.Clear();

            // Bank Info
            cmbBankAccountType.SelectedIndex = -1;
            cmbBankName.SelectedIndex = -1;
            txtBankAccountHolderName.Clear();
            txtBankAccountNumber.Clear();
            txtBankAdditionalInfo.Clear();

            // Submitted Documents
            chkDoc1.Checked = false; chkDoc2.Checked = false; chkDoc3.Checked = false;
            chkDoc4.Checked = false; chkDoc5.Checked = false; chkDoc6.Checked = false;
            chkDoc7.Checked = false; chkDoc8.Checked = false; chkDoc9.Checked = false;

            // Declaration
            txtApplicantSignature.Clear();
            dtpApplicationDate.Value = DateTime.Today;
            chkAgreeTerms.Checked = false;
            chkAgreePolicy.Checked = false;

            currentCustomerId = 0;
            currentAccidentId = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Next button clicked - functionality to be defined.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Original button1 clicked - functionality to be defined.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}