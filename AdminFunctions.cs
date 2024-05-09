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

namespace EcoLogiX_New
{
    public partial class AdminFunctions : Form
    {
        public AdminFunctions()
        {
            InitializeComponent();
        }

        private void btnCertifications_Click(object sender, EventArgs e)
        {
            // Define the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["CertificationsDb"].ConnectionString;
            // SQL query to fetch all data from the dbo.Certifications table
            string query = "SELECT [CertificationName], [CertificationBody], [DocumentName], [DocumentType], [UserID] FROM dbo.Certifications";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridAdmin DataGridView
                    dataGridAdmin.DataSource = dt;

                    // Optionally, handle the columns visibility or formatting here
                    dataGridAdmin.Columns["UserID"].Visible = false; // Example: Hide UserID column
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void bnSupplyChainData_Click(object sender, EventArgs e)
        {
            // Define the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            // SQL query to fetch all data from the dbo.SupplyChainData table
            string query = "SELECT [UserId], [SupplierName], [ContactPerson], [EmailAddress], [PhoneNumber], [LocationInformation], [ProductName], [ProductDescription], [ProductCategory], [TotalEnergyConsumption], [TotalGreenhouseEmission], [ComplianceLaborLaws], [HealthSafetyMeasures], [TraceabilityRawMaterials], [EthicalCertifications], [RelevantCertifications], [ComplianceDocumentationStatus] FROM dbo.SupplyChainData";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridAdmin DataGridView
                    dataGridAdmin.DataSource = dt;

                    // Optional: Adjust column settings such as visibility or headers if necessary
                    dataGridAdmin.AutoResizeColumns();  // Adjust column widths for better readability
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void btnSustainability_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            // SQL query to fetch specified data from the dbo.SustainabilityGoals table
            string query = "SELECT [GoalDescription], [TargetDate], [PriorityLevel], [Notes], [UserID] FROM dbo.SustainabilityGoals";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridAdmin DataGridView
                    dataGridAdmin.DataSource = dt;

                    // Optional: Adjust column settings for readability and usability
                    dataGridAdmin.AutoResizeColumns(); // Adjust column widths for better readability
                    dataGridAdmin.Columns["UserID"].Visible = false; // Hide UserID if not needed visibly
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDb"].ConnectionString;
            // SQL query to fetch specified data from the dbo.Users table
            string query = "SELECT [Name], [Email], [Password], [CompanyName], [Role], [Contact] FROM dbo.Users";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridAdmin DataGridView
                    dataGridAdmin.DataSource = dt;

                    // Optional: Adjust column settings for readability and usability
                    dataGridAdmin.AutoResizeColumns();  // Adjust column widths for better readability
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }
    }
}
