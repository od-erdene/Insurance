using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Insurance
{
    public partial class Document : Form
    {
        private List<DocumentInfo> uploadedFiles = new List<DocumentInfo>();
        private readonly string[] requiredDocumentTypes = new string[]
        {
            "Компанийн албан бичиг /гэрээ компанийн нэр дээр бол/\r\n",
            "Даатгалын гэрээт баталгаа /эх хувь/\r\n",
            "Иргэний үнэмлэхний хуулбар\r\n",
            "Эрх бүхий байгууллагын акт, тодорхойлолт /эх хувиараа/",
            "Орон сууцны контор, СӨХ бусад байгууллагын акт, тодорхойлолт\r\n",
            "Гэмтэж, эвдэрсэн эд хөрөнгийн фото зураг\r\n",
            "Буруутай талын иргэний үнэмлэх /баталгаажсан хуулбар/",
            "Хохирлын үнэлгээ /эх хувиараа/",
            "Нэхэмжлэх/зардалын баримт /эх хувиараа/\r\n"
        };

        private DB db; // Instance of your DB class

        public Document()
        {
            InitializeComponent();
            db = new DB(); // Initialize the DB instance
            InitializeRequiredDocuments();
            InitializeComboBox();
            UpdateNavigationButtons();
        }

        private void InitializeComboBox()
        {
            cmbFileLabel.Items.Clear();
            cmbFileLabel.Items.AddRange(requiredDocumentTypes);
            cmbFileLabel.SelectedIndex = 0;
        }

        private void InitializeRequiredDocuments()
        {
            // Clear existing items
            lvFiles.Items.Clear();
            uploadedFiles.Clear();

            // Add all required document types to the list view
            foreach (string docType in requiredDocumentTypes)
            {
                var item = new ListViewItem("Байхгүй");
                item.SubItems.Add(docType);
                item.SubItems.Add("Хүлээгдэж байна");
                lvFiles.Items.Add(item);
            }
        }

        private void UpdateNavigationButtons()
        {
            // Show Next button only if all documents are uploaded
            btnNext.Visible = uploadedFiles.Count == requiredDocumentTypes.Length;
            btnBack.Visible = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (cmbFileLabel.SelectedItem == null)
            {
                MessageBox.Show("Баримтын төрлийг сонгоно уу.", "Баримтын төрөл шаардлагатай", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedLabel = cmbFileLabel.SelectedItem.ToString();

            // Check if this document type is already uploaded
            if (uploadedFiles.Any(f => f.FileLabel == selectedLabel))
            {
                MessageBox.Show($"'{selectedLabel}' гэсэн төрлийн файл аль хэдийн оруулсан байна.",
                    "Давхардсан төрөл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(filePath);

                var documentInfo = new DocumentInfo
                {
                    FileName = fileName,
                    FileLabel = selectedLabel,
                    FilePath = filePath
                };

                uploadedFiles.Add(documentInfo);
                UpdateFileInListView(documentInfo);

                // Remove the uploaded document type from the combo box
                cmbFileLabel.Items.Remove(selectedLabel);
                if (cmbFileLabel.Items.Count > 0)
                {
                    cmbFileLabel.SelectedIndex = 0;
                }

                // Update navigation buttons
                UpdateNavigationButtons();
            }
        }

        private void UpdateFileInListView(DocumentInfo documentInfo)
        {
            // Find the item with matching label
            foreach (ListViewItem item in lvFiles.Items)
            {
                if (item.SubItems[1].Text == documentInfo.FileLabel)
                {
                    item.SubItems[0].Text = documentInfo.FileName;
                    item.SubItems[2].Text = documentInfo.FilePath;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (uploadedFiles.Count != requiredDocumentTypes.Length)
            {
                var missingDocs = requiredDocumentTypes.Except(uploadedFiles.Select(f => f.FileLabel));
                MessageBox.Show($"Бүх шаардлагатай баримтуудыг оруулна уу. Дутуу байгаа баримтууд:\n{string.Join("\n", missingDocs)}",
                    "Дутуу баримтууд", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Use the existing connection from the DB class instance
                SqlConnection connection = db.con;

                // SQL query to insert into AccidentDocuments table
                string sql = "INSERT INTO AccidentDocuments (DocumentID, IsSubmitted, IsVerified, SubmittedAt, EmployeeID, AccidentID) VALUES (@DocumentID, @IsSubmitted, @IsVerified, @SubmittedAt, @EmployeeID, @AccidentID)";

                foreach (var document in uploadedFiles)
                {
                    // Get the actual DocumentID from the Documents table based on DocumentName (document.FileLabel)
                    int documentId = GetDocumentIdByLabel(document.FileLabel, connection);

                    if (documentId > 0)
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@DocumentID", documentId);
                            command.Parameters.AddWithValue("@IsSubmitted", true);
                            command.Parameters.AddWithValue("@IsVerified", false);
                            command.Parameters.AddWithValue("@SubmittedAt", DateTime.Now);
                            // TODO: Replace with the actual EmployeeID and AccidentID
                            command.Parameters.AddWithValue("@EmployeeID", 1); // Placeholder
                            command.Parameters.AddWithValue("@AccidentID", 1); // Placeholder

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Could not find DocumentID for label: {document.FileLabel}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Optionally break or continue depending on how you want to handle missing DocumentIDs
                    }
                }

                MessageBox.Show("Бүх шаардлагатай баримтууд амжилттай хадгалагдлаа!", "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to get DocumentID from the Documents table
        private int GetDocumentIdByLabel(string label, SqlConnection connection)
        {
            int documentId = 0;
            string query = "SELECT DocumentsID FROM Documents WHERE DocumentsName = @DocumentsName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DocumentsName", label);
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    documentId = Convert.ToInt32(result);
                }
            }
            return documentId;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to the previous form
            Call call = new Call();
            call.Show();
            this.Hide();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (uploadedFiles.Count != requiredDocumentTypes.Length)
            {
                var missingDocs = requiredDocumentTypes.Except(uploadedFiles.Select(f => f.FileLabel));
                MessageBox.Show($"Дараах алхам руу шилжихийн өмнө бүх шаардлагатай баримтуудыг оруулна уу. Дутуу байгаа баримтууд:\n{string.Join("\n", missingDocs)}",
                    "Дутуу баримтууд", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: Navigate to the next form
            // For now, just show a message
            MessageBox.Show("Бүх баримтууд амжилттай оруулагдлаа! Дараах алхам руу шилжихэд бэлэн байна.",
                "Амжилттай", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class DocumentInfo
    {
        public string FileName { get; set; }
        public string FileLabel { get; set; }
        public string FilePath { get; set; }
    }
}
