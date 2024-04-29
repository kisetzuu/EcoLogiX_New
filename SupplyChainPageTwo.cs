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

namespace EcoLogiX_New
{
    public partial class SupplyChainPageTwo : Form
    {

        private SupplyChainTransparency previousForm;

        // Properties to hold data from SupplyChainTransparency
        public string TotalEnergyConsumption { get; set; }
        public string TotalGreenhouseEmission { get; set; }
        public string HealthSafetyMeasures { get; set; }
        public string EthicalCertifications { get; set; }
        public string RelevantCertifications { get; set; }

        public SupplyChainPageTwo()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        public SupplyChainPageTwo(SupplyChainTransparency previousForm) : this()
        {
            this.previousForm = previousForm;

            // Display data received from Form 1
            txtTotalEnergy.Text = previousForm.TotalEnergyConsumption;
            txtTotalGreenhouse.Text = previousForm.TotalGreenhouseEmission;
            txtHealth.Text = previousForm.HealthSafetyMeasures;
            txtCertifications.Text = previousForm.EthicalCertifications;
            txtRelev.Text = previousForm.RelevantCertifications;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Establish connection to the database
            string connectionString = "SupplyChainDataDb"; // Replace "SupplyChainDataDb" with your actual connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define SQL query to insert data into the database
                string query = "INSERT INTO SupplyChainData (TotalEnergyConsumption, TotalGreenhouseEmission, ComplianceLaborLaws, HealthSafetyMeasures, TraceabilityRawMaterials, EthicalCertifications, RelevantCertifications, ComplianceDocumentationStatus) " +
                               "VALUES (@TotalEnergyConsumption, @TotalGreenhouseEmission, @ComplianceLaborLaws, @HealthSafetyMeasures, @TraceabilityRawMaterials, @EthicalCertifications, @RelevantCertifications, @ComplianceDocumentationStatus)";

                // Create SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@TotalEnergyConsumption", txtTotalEnergy.Text);
                    command.Parameters.AddWithValue("@TotalGreenhouseEmission", txtTotalGreenhouse.Text);
                    command.Parameters.AddWithValue("@ComplianceLaborLaws", comboCompliance.Text);
                    command.Parameters.AddWithValue("@HealthSafetyMeasures", txtHealth.Text);
                    command.Parameters.AddWithValue("@TraceabilityRawMaterials", comboRaw.Text);
                    command.Parameters.AddWithValue("@EthicalCertifications", txtCertifications.Text);
                    command.Parameters.AddWithValue("@RelevantCertifications", txtRelev.Text);
                    command.Parameters.AddWithValue("@ComplianceDocumentationStatus", comboDoc.Text);

                    // Open the connection
                    connection.Open();

                    // Execute the SQL command
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if the insertion was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully into the database.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert data into the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SupplyChainPageTwo_Load(object sender, EventArgs e)
        {

        }
    }
}
