using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Insurance
{
    public partial class RegisterForm : Form
    {
        // Store original document names and their IDs fetched from DB
        private Dictionary<int, string> documentIdToNameMap = new Dictionary<int, string>();

        public RegisterForm()
        {
            InitializeComponent();
            // Set the initial size of the form (Width, Height)
            // The mainPanel has AutoScroll = true, so if content overflows, scrollbars will appear.
            // The Height of mainPanel is increased in Designer to accommodate all group boxes.
            // Buttons are anchored to the bottom right of the Form, not the Panel.
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            LoadProvinces(cmbCustomerProvince);
            LoadProvinces(cmbWitnessProvince);
            LoadProvinces(cmbOffenderProvince);

            LoadPropertyTypes();
            LoadCallTimeTypes();
            LoadInvestigatorOrganizations();
            LoadBankNames();
            LoadBankAccountTypes();
            LoadCallTypesCompany();
            LoadEmployees();
            LoadDocuments(); // Load documents for the checklist

            // Set default states or values if necessary
            dtpAccidentDate.Value = DateTime.Today;
            dtpAccidentTime.Value = DateTime.Now;
            dtpAppliedDate.Value = DateTime.Today;
            dtpCallDateCompany.Value = DateTime.Today;
            dtpCallTimeCompany.Value = DateTime.Now;

            chkContactedInvestigator_CheckedChanged(null, null); // Initialize investigator fields state
        }

        private void LoadDocuments()
        {
            documentIdToNameMap.Clear();
            List<KeyValuePair<int, string>> documents = new List<KeyValuePair<int, string>>();
            using (DB db = new DB())
            {
                try
                {
                    db.cmd.CommandText = "SELECT DocumentsID, DocumentsName FROM Documents ORDER BY DocumentsID";
                    db.dr = db.cmd.ExecuteReader();
                    while (db.dr.Read())
                    {
                        int docId = Convert.ToInt32(db.dr["DocumentsID"]);
                        string docName = db.dr["DocumentsName"].ToString();
                        documents.Add(new KeyValuePair<int, string>(docId, docName));
                        documentIdToNameMap[docId] = docName; // Store for later reference
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Баримт бичгийн жагсаалтыг ачаалахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (db.dr != null && !db.dr.IsClosed) db.dr.Close();
                }
            }

            // Assuming chkDoc1 to chkDoc9 correspond to the first 9 documents in order
            CheckBox[] chkDocs = { chkDoc1, chkDoc2, chkDoc3, chkDoc4, chkDoc5, chkDoc6, chkDoc7, chkDoc8, chkDoc9 };
            for (int i = 0; i < chkDocs.Length; i++)
            {
                if (i < documents.Count)
                {
                    chkDocs[i].Text = $"{i + 1}. {documents[i].Value}";
                    chkDocs[i].Tag = documents[i].Key; // Store DocumentID in Tag
                }
                else
                {
                    chkDocs[i].Visible = false; // Hide checkboxes if not enough documents in DB
                }
            }
        }


        private void LoadProvinces(ComboBox comboBox)
        {
            try
            {
                using (DB db = new DB())
                {
                    db.cmd.CommandText = "SELECT ProvinceID, ProvinceName FROM Province ORDER BY ProvinceName";
                    DataTable dt = new DataTable();
                    db.ada.SelectCommand = db.cmd;
                    db.ada.Fill(dt);

                    comboBox.DisplayMember = "ProvinceName";
                    comboBox.ValueMember = "ProvinceID";
                    comboBox.DataSource = dt;
                    comboBox.SelectedIndex = -1; // No default selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Аймаг/хотын мэдээллийг ачаалахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCommittees(ComboBox provinceComboBox, ComboBox committeeComboBox)
        {
            if (provinceComboBox.SelectedValue == null)
            {
                committeeComboBox.DataSource = null;
                committeeComboBox.Items.Clear();
                return;
            }

            try
            {
                using (DB db = new DB())
                {
                    int provinceId = Convert.ToInt32(provinceComboBox.SelectedValue);
                    db.cmd.CommandText = "SELECT CommitteeID, CommitteeName FROM Committee WHERE ProvinceID = @ProvinceID ORDER BY CommitteeName";
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@ProvinceID", provinceId);

                    DataTable dt = new DataTable();
                    db.ada.SelectCommand = db.cmd;
                    db.ada.Fill(dt);

                    committeeComboBox.DisplayMember = "CommitteeName";
                    committeeComboBox.ValueMember = "CommitteeID";
                    committeeComboBox.DataSource = dt;
                    committeeComboBox.SelectedIndex = -1; // No default selection
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сум/дүүргийн мэдээллийг ачаалахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPropertyTypes()
        {
            LoadComboBoxData(cmbPropertyType, "SELECT PropertyTypeID, PropertyTypeName FROM PropertyType ORDER BY PropertyTypeName", "PropertyTypeName", "PropertyTypeID", "Эд хөрөнгийн төрөл");
        }

        private void LoadCallTimeTypes()
        {
            LoadComboBoxData(cmbCallTimeType, "SELECT CallTimeTypeID, CallTimeTypeName FROM CallTimeType ORDER BY CallTimeTypeID", "CallTimeTypeName", "CallTimeTypeID", "Дуудлага өгсөн хэлбэр");
        }

        private void LoadInvestigatorOrganizations()
        {
            LoadComboBoxData(cmbInvestigatorOrganization, "SELECT InvestigationOrganizationID, InvestigationOrganizationName FROM InvestigationOrganization ORDER BY InvestigationOrganizationName", "InvestigationOrganizationName", "InvestigationOrganizationID", "Мөрдөн шалгах байгууллага");
        }

        private void LoadBankNames()
        {
            LoadComboBoxData(cmbBank, "SELECT BankID, BankName FROM Bank ORDER BY BankName", "BankName", "BankID", "Банк");
        }

        private void LoadBankAccountTypes()
        {
            LoadComboBoxData(cmbBankAccountType, "SELECT BankAccountTypeID, BankAccountTypeName FROM BankAccountType ORDER BY BankAccountTypeID", "BankAccountTypeName", "BankAccountTypeID", "Дансны төрөл");
        }

        private void LoadCallTypesCompany()
        {
            LoadComboBoxData(cmbCallTypeCompany, "SELECT CallTypeID, CallTypeName FROM CallType ORDER BY CallTypeID", "CallTypeName", "CallTypeID", "Дуудлага шилжүүлсэн хэлбэр");
        }
        private void LoadEmployees()
        {
            LoadComboBoxData(cmbCallEmployee, "SELECT EmployeeID, EmployeeLastName + ' ' + EmployeeFirstName AS FullName FROM Employee ORDER BY FullName", "FullName", "EmployeeID", "Ажилтан");
        }


        private void LoadComboBoxData(ComboBox comboBox, string query, string displayMember, string valueMember, string entityName)
        {
            try
            {
                using (DB db = new DB())
                {
                    db.cmd.CommandText = query;
                    DataTable dt = new DataTable();
                    db.ada.SelectCommand = db.cmd;
                    db.ada.Fill(dt);

                    DataRow dr = dt.NewRow();
                    dr[displayMember] = "--- Сонгоно уу ---";
                    dr[valueMember] = DBNull.Value; // Or 0 or -1 if your ID is not nullable and 0/-1 is an invalid ID
                    dt.Rows.InsertAt(dr, 0);


                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.DataSource = dt;
                    comboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{entityName} мэдээллийг ачаалахад алдаа гарлаа: " + ex.Message, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void chkContactedInvestigator_CheckedChanged(object sender, EventArgs e)
        {
            bool contacted = chkContactedInvestigator.Checked;
            cmbInvestigatorOrganization.Enabled = contacted;
            txtInvestigatorName.Enabled = contacted;
            txtInvestigatorPhoneNumber.Enabled = contacted;
            txtReasonForNotContactingInvestigator.Enabled = !contacted;
            if (contacted)
            {
                txtReasonForNotContactingInvestigator.Clear();
            }
            else
            {
                cmbInvestigatorOrganization.SelectedIndex = -1;
                txtInvestigatorName.Clear();
                txtInvestigatorPhoneNumber.Clear();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerLastName.Text))
            {
                MessageBox.Show("Даатгуулагчийн овгийг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerFirstName.Text))
            {
                MessageBox.Show("Даатгуулагчийн нэрийг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerFirstName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerRegisterNumber.Text))
            {
                MessageBox.Show("Даатгуулагчийн регистрийн дугаарыг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerRegisterNumber.Focus();
                return false;
            }
            if (cmbPropertyType.SelectedValue == null || cmbPropertyType.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Эд хөрөнгийн төрлийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPropertyType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCustomerPhoneNumber1.Text))
            {
                MessageBox.Show("Даатгуулагчийн утасны дугаар 1-г оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerPhoneNumber1.Focus();
                return false;
            }
            if (cmbCustomerProvince.SelectedValue == null || cmbCustomerProvince.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Даатгуулагчийн аймгийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomerProvince.Focus();
                return false;
            }
            if (cmbCustomerCommittee.SelectedValue == null || cmbCustomerCommittee.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Даатгуулагчийн сумыг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomerCommittee.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtAccidentName.Text))
            {
                MessageBox.Show("Тохиолдлын нэрийг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAccidentLocation.Text))
            {
                MessageBox.Show("Тохиолдол болсон газрыг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccidentLocation.Focus();
                return false;
            }
            if (cmbCallTimeType.SelectedValue == null || cmbCallTimeType.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Дуудлага өгсөн хэлбэрийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCallTimeType.Focus();
                return false;
            }


            if (chkContactedInvestigator.Checked)
            {
                if (cmbInvestigatorOrganization.SelectedValue == null || cmbInvestigatorOrganization.SelectedValue == DBNull.Value)
                {
                    MessageBox.Show("Мэдэгдсэн байгууллагыг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbInvestigatorOrganization.Focus();
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtReasonForNotContactingInvestigator.Text))
                {
                    MessageBox.Show("Эрх бүхий байгууллагад хандаагүй шалтгааныг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReasonForNotContactingInvestigator.Focus();
                    return false;
                }
            }

            if (cmbBankAccountType.SelectedValue == null || cmbBankAccountType.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Банкны дансны төрлийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBankAccountType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBankAccountOwnerName.Text))
            {
                MessageBox.Show("Данс эзэмшигчийн нэрийг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankAccountOwnerName.Focus();
                return false;
            }
            if (cmbBank.SelectedValue == null || cmbBank.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Банкны нэрийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBank.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBankAccountNumber.Text))
            {
                MessageBox.Show("Дансны дугаарыг оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankAccountNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApplicationSignature.Text))
            {
                MessageBox.Show("Мэдүүлэг хэсэгт гарын үсгийг (нэр) оруулна уу.", "Оруулга дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApplicationSignature.Focus();
                return false;
            }

            // Company Use Validation
            if (cmbCallTypeCompany.SelectedValue == null || cmbCallTypeCompany.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Компанийн хэрэгцээнд: Дуудлага шилжүүлсэн хэлбэрийг сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCallTypeCompany.Focus();
                return false;
            }
            if (cmbCallEmployee.SelectedValue == null || cmbCallEmployee.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Компанийн хэрэгцээнд: Материал хүлээн авсан ХҮМ-г сонгоно уу.", "Сонголт дутуу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCallEmployee.Focus();
                return false;
            }


            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            using (DB db = new DB())
            {
                // For operations spanning multiple tables, transactions are good.
                // However, SqlCommand by default doesn't enlist in a transaction unless one is explicitly started on the connection.
                // For simplicity here, we'll execute commands sequentially. If any fails, prior ones are not auto-rolled back without a transaction.
                // Consider adding transaction handling if atomicity is critical.
                // SqlTransaction transaction = db.con.BeginTransaction();
                // db.cmd.Transaction = transaction;

                try
                {
                    // 1. Insert Customer
                    db.cmd.CommandText = @"INSERT INTO Customer (CustomerLastName, CustomerFirstName, CustomerRegisterNumber, CustomerPhoneNumber1, CustomerPhoneNumber2, CommitteeID, CustomerAddress)
                                           OUTPUT INSERTED.CustomerID
                                           VALUES (@CustomerLastName, @CustomerFirstName, @CustomerRegisterNumber, @CustomerPhoneNumber1, @CustomerPhoneNumber2, @CommitteeID, @CustomerAddress)";
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@CustomerLastName", txtCustomerLastName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CustomerFirstName", txtCustomerFirstName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CustomerRegisterNumber", txtCustomerRegisterNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber1", txtCustomerPhoneNumber1.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CustomerPhoneNumber2", string.IsNullOrWhiteSpace(txtCustomerPhoneNumber2.Text) ? (object)DBNull.Value : txtCustomerPhoneNumber2.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CommitteeID", Convert.ToInt32(cmbCustomerCommittee.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@CustomerAddress", string.IsNullOrWhiteSpace(txtCustomerAddress.Text) ? (object)DBNull.Value : txtCustomerAddress.Text.Trim());

                    int customerId = Convert.ToInt32(db.cmd.ExecuteScalar());

                    // 2. Insert Accident
                    db.cmd.CommandText = @"INSERT INTO Accident (
                                               AccidentName, AccidentLocation, AccidentDate, AccidentTime, AccidentDetails,
                                               CallTimeTypeID, PropertyTypeID, ContactedInvestigator, ReasonForNotContactingInvestigator,
                                               InvestigatorName, InvestigatorPhoneNumber, InvestigatorOrganizationID,
                                               AppliedDate, ApplcationSignature,
                                               OffenderName, OffenderPhoneNumber, OffenderRegisterNumber, OffenderCommitteeID, OffenderAddress,
                                               WitnessName, WitnessPhoneNumber, WitnessRegisterNumber, WitnessCommitteeID, WitnessAddress,
                                               BankAccountNumber, BankAccountTypeID, BankID, BankAccountOwnerName, BankAccountDetails,
                                               CallTypeID, CallDate, CallTime, CallEmployeeID, PropertyDetails, CustomerID
                                           ) OUTPUT INSERTED.AccidentID VALUES (
                                               @AccidentName, @AccidentLocation, @AccidentDate, @AccidentTime, @AccidentDetails,
                                               @CallTimeTypeID, @PropertyTypeID, @ContactedInvestigator, @ReasonForNotContactingInvestigator,
                                               @InvestigatorName, @InvestigatorPhoneNumber, @InvestigatorOrganizationID,
                                               @AppliedDate, @ApplcationSignature,
                                               @OffenderName, @OffenderPhoneNumber, @OffenderRegisterNumber, @OffenderCommitteeID, @OffenderAddress,
                                               @WitnessName, @WitnessPhoneNumber, @WitnessRegisterNumber, @WitnessCommitteeID, @WitnessAddress,
                                               @BankAccountNumber, @BankAccountTypeID, @BankID, @BankAccountOwnerName, @BankAccountDetails,
                                               @CallTypeID, @CallDate, @CallTime, @CallEmployeeID, @PropertyDetails, @CustomerID
                                           )";
                    db.cmd.Parameters.Clear();
                    db.cmd.Parameters.AddWithValue("@AccidentName", txtAccidentName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@AccidentLocation", txtAccidentLocation.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@AccidentDate", dtpAccidentDate.Value.Date);
                    db.cmd.Parameters.AddWithValue("@AccidentTime", dtpAccidentTime.Value.TimeOfDay);
                    db.cmd.Parameters.AddWithValue("@AccidentDetails", string.IsNullOrWhiteSpace(txtAccidentDetails.Text) ? (object)DBNull.Value : txtAccidentDetails.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CallTimeTypeID", Convert.ToInt32(cmbCallTimeType.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@PropertyTypeID", Convert.ToInt32(cmbPropertyType.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@ContactedInvestigator", chkContactedInvestigator.Checked);
                    db.cmd.Parameters.AddWithValue("@ReasonForNotContactingInvestigator", chkContactedInvestigator.Checked ? (object)DBNull.Value : txtReasonForNotContactingInvestigator.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorName", !chkContactedInvestigator.Checked || string.IsNullOrWhiteSpace(txtInvestigatorName.Text) ? (object)DBNull.Value : txtInvestigatorName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorPhoneNumber", !chkContactedInvestigator.Checked || string.IsNullOrWhiteSpace(txtInvestigatorPhoneNumber.Text) ? (object)DBNull.Value : txtInvestigatorPhoneNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@InvestigatorOrganizationID", !chkContactedInvestigator.Checked || cmbInvestigatorOrganization.SelectedValue == null || cmbInvestigatorOrganization.SelectedValue == DBNull.Value ? (object)DBNull.Value : Convert.ToInt32(cmbInvestigatorOrganization.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@AppliedDate", dtpAppliedDate.Value.Date);
                    db.cmd.Parameters.AddWithValue("@ApplcationSignature", txtApplicationSignature.Text.Trim());

                    db.cmd.Parameters.AddWithValue("@OffenderName", string.IsNullOrWhiteSpace(txtOffenderName.Text) ? (object)DBNull.Value : txtOffenderName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@OffenderPhoneNumber", string.IsNullOrWhiteSpace(txtOffenderPhoneNumber.Text) ? (object)DBNull.Value : txtOffenderPhoneNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@OffenderRegisterNumber", string.IsNullOrWhiteSpace(txtOffenderRegisterNumber.Text) ? (object)DBNull.Value : txtOffenderRegisterNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@OffenderCommitteeID", cmbOffenderCommittee.SelectedValue == null || cmbOffenderCommittee.SelectedValue == DBNull.Value ? (object)DBNull.Value : Convert.ToInt32(cmbOffenderCommittee.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@OffenderAddress", string.IsNullOrWhiteSpace(txtOffenderAddress.Text) ? (object)DBNull.Value : txtOffenderAddress.Text.Trim());

                    db.cmd.Parameters.AddWithValue("@WitnessName", string.IsNullOrWhiteSpace(txtWitnessName.Text) ? (object)DBNull.Value : txtWitnessName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@WitnessPhoneNumber", string.IsNullOrWhiteSpace(txtWitnessPhoneNumber.Text) ? (object)DBNull.Value : txtWitnessPhoneNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@WitnessRegisterNumber", string.IsNullOrWhiteSpace(txtWitnessRegisterNumber.Text) ? (object)DBNull.Value : txtWitnessRegisterNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@WitnessCommitteeID", cmbWitnessCommittee.SelectedValue == null || cmbWitnessCommittee.SelectedValue == DBNull.Value ? (object)DBNull.Value : Convert.ToInt32(cmbWitnessCommittee.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@WitnessAddress", string.IsNullOrWhiteSpace(txtWitnessAddress.Text) ? (object)DBNull.Value : txtWitnessAddress.Text.Trim());

                    db.cmd.Parameters.AddWithValue("@BankAccountNumber", txtBankAccountNumber.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@BankAccountTypeID", Convert.ToInt32(cmbBankAccountType.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@BankID", Convert.ToInt32(cmbBank.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@BankAccountOwnerName", txtBankAccountOwnerName.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@BankAccountDetails", string.IsNullOrWhiteSpace(txtBankAccountDetails.Text) ? (object)DBNull.Value : txtBankAccountDetails.Text.Trim());

                    db.cmd.Parameters.AddWithValue("@CallTypeID", Convert.ToInt32(cmbCallTypeCompany.SelectedValue));
                    db.cmd.Parameters.AddWithValue("@CallDate", dtpCallDateCompany.Value.Date);
                    db.cmd.Parameters.AddWithValue("@CallTime", dtpCallTimeCompany.Value.TimeOfDay);
                    db.cmd.Parameters.AddWithValue("@CallEmployeeID", Convert.ToInt32(cmbCallEmployee.SelectedValue)); // Assuming EmployeeID for logged in user, or selected
                    db.cmd.Parameters.AddWithValue("@PropertyDetails", string.IsNullOrWhiteSpace(txtPropertyDetails.Text) ? (object)DBNull.Value : txtPropertyDetails.Text.Trim());
                    db.cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    int accidentId = Convert.ToInt32(db.cmd.ExecuteScalar());

                    // 3. Insert AccidentDocuments
                    CheckBox[] chkDocs = { chkDoc1, chkDoc2, chkDoc3, chkDoc4, chkDoc5, chkDoc6, chkDoc7, chkDoc8, chkDoc9 };
                    db.cmd.CommandText = @"INSERT INTO AccidentDocuments (DocumentID, IsSubmitted, IsVerified, SubmittedAt, EmployeeID, AccidentID)
                                           VALUES (@DocumentID, @IsSubmitted, @IsVerified, @SubmittedAt, @EmployeeID, @AccidentID)";

                    // For EmployeeID in AccidentDocuments, if it's the same as CallEmployeeID or another specific employee
                    int? docEmployeeId = (cmbCallEmployee.SelectedValue == null || cmbCallEmployee.SelectedValue == DBNull.Value) ? (int?)null : Convert.ToInt32(cmbCallEmployee.SelectedValue);


                    foreach (var chk in chkDocs)
                    {
                        if (chk.Checked && chk.Tag != null) // chk.Tag stores DocumentID
                        {
                            db.cmd.Parameters.Clear();
                            db.cmd.Parameters.AddWithValue("@DocumentID", Convert.ToInt32(chk.Tag));
                            db.cmd.Parameters.AddWithValue("@IsSubmitted", true);
                            db.cmd.Parameters.AddWithValue("@IsVerified", false); // Default to not verified
                            db.cmd.Parameters.AddWithValue("@SubmittedAt", DateTime.Now);
                            if (docEmployeeId.HasValue)
                                db.cmd.Parameters.AddWithValue("@EmployeeID", docEmployeeId.Value);
                            else
                                db.cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value); // Or handle as required

                            db.cmd.Parameters.AddWithValue("@AccidentID", accidentId);
                            db.cmd.ExecuteNonQuery();
                        }
                    }

                    // transaction.Commit(); // If transaction was used
                    MessageBox.Show("Мэдээлэл амжилттай хадгалагдлаа!", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    // if (transaction != null) transaction.Rollback(); // If transaction was used
                    MessageBox.Show("Мэдээлэл хадгалахад алдаа гарлаа: " + ex.Message + "\nStack Trace: " + ex.StackTrace, "Алдаа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}