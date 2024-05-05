using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using static EcoLogiX_New.GreenCertification;
using System.Net.Mail;

namespace EcoLogiX_New
{
    public partial class SupplyChainPageTwo : Form
    {

        // Properties to hold data from SupplyChainTransparency
        public string SupplierName { get; set; }
        public string ContactPerson { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationInformation { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string TotalEnergyConsumption { get; set; }
        public string TotalGreenhouseEmission { get; set; }
        public string HealthSafetyMeasures { get; set; }
        public string EthicalCertifications { get; set; }
        public string RelevantCertifications { get; set; }
        public int UserID { get; private set; }

        public SupplyChainPageTwo(int userId, string supplierName, string contactPerson, string emailAddress, string phoneNumber, string locationInformation, string productName, string productDescription, string productCategory)
        {
            InitializeComponent();

            this.UserID = userId;
            // Set values of controls based on parameters
            this.SupplierName = supplierName;
            this.ContactPerson = contactPerson;
            this.EmailAddress = emailAddress;
            this.PhoneNumber = phoneNumber;
            this.LocationInformation = locationInformation;
            this.ProductName = productName;
            this.ProductDescription = productDescription;
            this.ProductCategory = productCategory;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        public static class UserSession
        {
            public static int UserID { get; set; }
            // Other session-related properties can be added here
        }

        public SupplyChainPageTwo(SupplyChainTransparency previousForm) : this(
         UserSession.UserID,  // Include the UserID fetched from the session
         previousForm.SupplierName,
         previousForm.ContactPerson,
         previousForm.EmailAddress,
         previousForm.PhoneNumber,
         previousForm.LocationInformation,
         previousForm.ProductName,
         previousForm.ProductDescription,
         previousForm.ProductCategory)
        {
            // This constructor is now correctly passing all required arguments to the main constructor
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Retrieve the user's ID from the session
            int userId = UserSession.UserID;

            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;

            // Extend the SQL query to include data from both forms
            string query = @"
                 INSERT INTO SupplyChainData (
                     UserId, 
                     SupplierName, ContactPerson, EmailAddress, PhoneNumber, LocationInformation, ProductName, ProductDescription, ProductCategory,
                     TotalEnergyConsumption, TotalGreenhouseEmission, ComplianceLaborLaws, HealthSafetyMeasures, TraceabilityRawMaterials, EthicalCertifications, RelevantCertifications, ComplianceDocumentationStatus
                 ) VALUES (
                     @UserId, 
                     @SupplierName, @ContactPerson, @EmailAddress, @PhoneNumber, @LocationInformation, @ProductName, @ProductDescription, @ProductCategory,
                     @TotalEnergyConsumption, @TotalGreenhouseEmission, @ComplianceLaborLaws, @HealthSafetyMeasures, @TraceabilityRawMaterials, @EthicalCertifications, @RelevantCertifications, @ComplianceDocumentationStatus
                 )";

            // Create SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameters for data from SupplyChainTransparency
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@SupplierName", SupplierName);
                command.Parameters.AddWithValue("@ContactPerson", ContactPerson);
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                command.Parameters.AddWithValue("@LocationInformation", LocationInformation);
                command.Parameters.AddWithValue("@ProductName", ProductName);
                command.Parameters.AddWithValue("@ProductDescription", ProductDescription);
                command.Parameters.AddWithValue("@ProductCategory", ProductCategory);

                // Add parameters for data specific to SupplyChainPageTwo
                command.Parameters.AddWithValue("@TotalEnergyConsumption", txtTotalEnergy.Text);
                command.Parameters.AddWithValue("@TotalGreenhouseEmission", txtTotalGreenhouse.Text);
                command.Parameters.AddWithValue("@ComplianceLaborLaws", comboCompliance.Text);
                command.Parameters.AddWithValue("@HealthSafetyMeasures", txtHealth.Text);
                command.Parameters.AddWithValue("@TraceabilityRawMaterials", comboRaw.Text);
                command.Parameters.AddWithValue("@EthicalCertifications", txtCertifications.Text);
                command.Parameters.AddWithValue("@RelevantCertifications", txtRelev.Text);
                command.Parameters.AddWithValue("@ComplianceDocumentationStatus", comboDoc.Text);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        MessageBox.Show("Data inserted successfully into the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to insert data into the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while inserting data into the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SupplyChainPageTwo_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SupplierDetails supplierDetails = new SupplierDetails();
            supplierDetails.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GeographicalDistribution geographicalDistribution = new GeographicalDistribution();
            geographicalDistribution.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProductInformation productInformation = new ProductInformation();
            productInformation.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            EnvironmentalImpact environmentalImpact = new EnvironmentalImpact();
            environmentalImpact.Show();
            this.Hide();
        }

        private void btnTraceability_Click(object sender, EventArgs e)
        {
            TraceabilityAndEthics traceabilityAndEthics = new TraceabilityAndEthics();
            traceabilityAndEthics.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu menuForm = new Menu();
            menuForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Analytics analyticsForm = new Analytics();
            analyticsForm.Show();
            this.Hide();
        }
    }
}
