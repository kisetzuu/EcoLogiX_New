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
    public partial class EnvironmentalImpact : Form
    {
        public EnvironmentalImpact()
        {
            InitializeComponent();
            ConfigureEnvironmentalImpactChart(); // Set up bar and line charts
            ConfigureHistogramChart(); // Set up histogram chart
        }

        private void EnvironmentalImpact_Load(object sender, EventArgs e)
        {

        }

        private void ConfigureEnvironmentalImpactChart()
        {
            chartEnvironment.Series.Clear(); // Clears any existing series
            chartEnvironment.ChartAreas.Clear(); // Clears any existing chart areas

            // Create and configure the chart area
            ChartArea chartArea = new ChartArea("EnvironmentImpact");
            chartEnvironment.ChartAreas.Add(chartArea);

            // Create and configure series for Total Energy Consumption
            Series energySeries = new Series("TotalEnergyConsumption")
            {
                ChartType = SeriesChartType.Column, // Bar chart
                BorderWidth = 2,
                Color = Color.Blue
            };
            chartEnvironment.Series.Add(energySeries);

            // Create and configure series for Total Greenhouse Emission
            Series emissionSeries = new Series("TotalGreenhouseEmission")
            {
                ChartType = SeriesChartType.Line, // Line chart
                BorderWidth = 3,
                Color = Color.Green,
                YAxisType = AxisType.Secondary // Use secondary Y axis for better scale comparison
            };
            chartEnvironment.Series.Add(emissionSeries);

            // Fetch data and bind to the chart
            FetchAndDisplayEnvironmentalData();
        }
        private void FetchAndDisplayEnvironmentalData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT SupplierName, TotalEnergyConsumption, TotalGreenhouseEmission FROM dbo.SupplyChainData";

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
                        double energyConsumption = Convert.ToDouble(reader["TotalEnergyConsumption"]);
                        double greenhouseEmission = Convert.ToDouble(reader["TotalGreenhouseEmission"]);

                        chartEnvironment.Series["TotalEnergyConsumption"].Points.AddXY(supplierName, energyConsumption);
                        chartEnvironment.Series["TotalGreenhouseEmission"].Points.AddXY(supplierName, greenhouseEmission);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load environmental data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureHistogramChart()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            // Adjusted SQL query to categorize data
            string query = @"
        SELECT 
            CASE 
                WHEN TotalEnergyConsumption BETWEEN 0 AND 2000 THEN '0-2000'
                WHEN TotalEnergyConsumption BETWEEN 2001 AND 4000 THEN '2001-4000'
                WHEN TotalEnergyConsumption BETWEEN 4001 AND 6000 THEN '4001-6000'
                ELSE 'Above 6000'
            END AS EnergyBinRange,
            COUNT(*) AS Count
        FROM dbo.SupplyChainData
        GROUP BY 
            CASE 
                WHEN TotalEnergyConsumption BETWEEN 0 AND 2000 THEN '0-2000'
                WHEN TotalEnergyConsumption BETWEEN 2001 AND 4000 THEN '2001-4000'
                WHEN TotalEnergyConsumption BETWEEN 4001 AND 6000 THEN '4001-6000'
                ELSE 'Above 6000'
            END";

            chartHisto.Series.Clear();
            chartHisto.ChartAreas.Clear();

            ChartArea histogramArea = new ChartArea("Histogram");
            chartHisto.ChartAreas.Add(histogramArea);

            Series histogramSeries = new Series("EnergyHistogram")
            {
                ChartType = SeriesChartType.Column,
                BorderWidth = 2,
                Color = Color.Teal
            };
            chartHisto.Series.Add(histogramSeries);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string energyBinRange = reader["EnergyBinRange"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        histogramSeries.Points.AddXY(energyBinRange, count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load histogram data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
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

        private void button12_Click(object sender, EventArgs e)
        {
            TraceabilityAndEthics traceabilityAndEthics = new TraceabilityAndEthics();
            traceabilityAndEthics.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DocumentationStatus documentationStatus = new DocumentationStatus();
            documentationStatus.Show();
            this.Hide();
        }
    }
}
