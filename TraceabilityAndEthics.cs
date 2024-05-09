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
using System.Windows.Forms.DataVisualization.Charting;

namespace EcoLogiX_New
{
    public partial class TraceabilityAndEthics : Form
    {
        public TraceabilityAndEthics()
        {
            InitializeComponent();
            ConfigureTraceabilityChart();
            ConfigureDataGridTraceability();
        }

        private void ConfigureTraceabilityChart()
        {
            chartTraceability.Series.Clear();
            chartTraceability.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chartTraceability.ChartAreas.Add(chartArea);

            Series series = new Series("Traceability")
            {
                ChartType = SeriesChartType.Pie
            };
            chartTraceability.Series.Add(series);

            FetchTraceabilityChartData();
        }

        private void FetchTraceabilityChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT TraceabilityRawMaterials, COUNT(*) AS Count FROM SupplyChainData GROUP BY TraceabilityRawMaterials";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string materialSource = reader["TraceabilityRawMaterials"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        chartTraceability.Series["Traceability"].Points.AddXY(materialSource, count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load traceability chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureDataGridTraceability()
        {
            // Clear existing columns if any
            dataGridTraceability.Columns.Clear();

            // Adding columns with specified names and headers
            dataGridTraceability.Columns.Add("Supplier", "Supplier");
            dataGridTraceability.Columns.Add("EthicalCertifications", "Ethical Certifications");
            dataGridTraceability.Columns.Add("RelevantCertifications", "Relevant Certifications");

            // Apply styling and configurations
            dataGridTraceability.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridTraceability.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridTraceability.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridTraceability.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridTraceability.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridTraceability.EnableHeadersVisualStyles = false;
            dataGridTraceability.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridTraceability.MultiSelect = false;

            dataGridTraceability.CellClick += DataGridTraceability_CellClick;
            // Fetch data for the DataGridView
            FetchDataGridTraceabilityData();
        }
        private void DataGridTraceability_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) // Check if the clicked cell is within the data bounds
            {
                DataGridViewCell clickedCell = dataGridTraceability.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellValue = clickedCell.Value != null ? clickedCell.Value.ToString() : "No value";
                MessageBox.Show($"Clicked cell value: {cellValue}", "Cell Content", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FetchDataGridTraceabilityData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT SupplierName, EthicalCertifications, RelevantCertifications FROM SupplyChainData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string supplierName = reader["SupplierName"].ToString();
                        string ethical = reader["EthicalCertifications"].ToString();
                        string relevant = reader["RelevantCertifications"].ToString();

                        int rowIndex = dataGridTraceability.Rows.Add();
                        dataGridTraceability.Rows[rowIndex].Cells["Supplier"].Value = supplierName;
                        dataGridTraceability.Rows[rowIndex].Cells["EthicalCertifications"].Value = ethical;  // Modify this to use images if needed
                        dataGridTraceability.Rows[rowIndex].Cells["RelevantCertifications"].Value = relevant;  // Modify this to use images if needed
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data grid traceability data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void TraceabilityAndEthics_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {
            Engagement engagementForm = new Engagement();
            engagementForm.Show();
            this.Hide();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            LoggedIn loggedIn = new LoggedIn();
            loggedIn.Show();
            this.Hide();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
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

        private void btnProduct_Click(object sender, EventArgs e)
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

        private void button11_Click(object sender, EventArgs e)
        {
            ComplianceAndSafety complianceAndSafety = new ComplianceAndSafety();
            complianceAndSafety.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DocumentationStatus documentationStatus = new DocumentationStatus();
            documentationStatus.Show();
            this.Hide();
        }

        private void dataGridTraceability_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
