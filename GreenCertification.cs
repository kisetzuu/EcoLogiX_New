using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EcoLogiX_New
{
    public partial class GreenCertification : Form
    {
        public GreenCertification()
        {
            InitializeComponent();
        }

        public static class UserSession
        {
            public static int UserID { get; set; }
            // Other session-related properties can be added here
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of the specified file
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Please select a valid file path.");
                return;
            }

            string fileName = Path.GetFileName(filePath);
            byte[] fileBytes;

            try
            {
                fileBytes = File.ReadAllBytes(filePath); // Consider using a more memory-efficient method for large files
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
                return;
            }

            int userId = UserSession.UserID; // Retrieve the user's ID from the session

            string connectionString = ConfigurationManager.ConnectionStrings["CertificationsDb"].ConnectionString;

            string query = @"
INSERT INTO dbo.Certifications (UserID, CertificationName, CertificationBody, DocumentName, DocumentType, DocumentBlob) 
VALUES (@UserID, @CertificationName, @CertificationBody, @DocumentName, 'PDF', @DocumentBlob)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@CertificationName", certificationName.Text);
                    cmd.Parameters.AddWithValue("@CertificationBody", certificationBody.Text);
                    cmd.Parameters.AddWithValue("@DocumentName", fileName);
                    cmd.Parameters.Add("@DocumentBlob", SqlDbType.VarBinary).Value = fileBytes;

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("File uploaded successfully.");
                        }
                        else
                        {
                            MessageBox.Show("File was not uploaded. Please try again.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            viewDocuments.Rows.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings["CertificationsDb"].ConnectionString;
            int userId = UserSession.UserID; // Retrieve the user's ID from the session

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DocumentName, DocumentType FROM dbo.Certifications WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string docName = reader["DocumentName"].ToString();
                                string docType = reader["DocumentType"].ToString();

                                // Add a new row to DataGridView for each document
                                viewDocuments.Rows.Add(docName, docType);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load documents: " + ex.Message);
                    }
                }
            }
        }
    }
}
