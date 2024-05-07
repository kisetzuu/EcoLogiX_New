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
    public partial class SustainabilityReporting : Form
    {
        
        public SustainabilityReporting()
        {
            InitializeComponent();
            ConfigureBarChart();
            ConfigureLineChart();
            ConfigurePieChart();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SustainabilityReporting_Load(object sender, EventArgs e)
        {
           
        }

        private void FetchBarChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT SupplierName, TotalEnergyConsumption, TotalGreenhouseEmission, ComplianceLaborLaws FROM dbo.SupplyChainData";

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

                        // Add points to the series
                        DataPoint energyPoint = new DataPoint();
                        energyPoint.SetValueXY(supplierName, energyConsumption);
                        energyPoint.ToolTip = $"Energy Consumption: {energyConsumption}";

                        DataPoint emissionPoint = new DataPoint();
                        emissionPoint.SetValueXY(supplierName, greenhouseEmission);
                        emissionPoint.ToolTip = $"Greenhouse Emission: {greenhouseEmission}";

                        barChartSupply.Series["Energy Consumption"].Points.Add(energyPoint);
                        barChartSupply.Series["Greenhouse Emission"].Points.Add(emissionPoint);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load bar chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureBarChart()
        {
            barChartSupply.Series.Clear();
            barChartSupply.ChartAreas.Clear();

            ChartArea barChartArea = new ChartArea("BarChartArea");
            barChartSupply.ChartAreas.Add(barChartArea);
            barChartArea.AxisX.Interval = 1;  // Ensure every label is shown
            barChartArea.AxisX.LabelStyle.Angle = -45;  // Angle labels for better fit
            barChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);  // Use a smaller font if needed

            // Configure AxisY
            barChartArea.AxisY.Title = "Value";
            barChartArea.AxisY.Minimum = 0;
            // Adjust maximum and interval based on your data scale

            Series seriesEnergy = new Series("Energy Consumption");
            seriesEnergy.ChartType = SeriesChartType.Bar;
            seriesEnergy.Color = Color.Green;
            barChartSupply.Series.Add(seriesEnergy);

            Series seriesEmission = new Series("Greenhouse Emission");
            seriesEmission.ChartType = SeriesChartType.Bar;
            seriesEmission.Color = Color.Blue;
            barChartSupply.Series.Add(seriesEmission);

            FetchBarChartData(); // This method needs to populate the series with data
        }

        private void FetchLineChartData()
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

                        // Add points to the series
                        DataPoint energyPoint = new DataPoint();
                        energyPoint.SetValueXY(supplierName, energyConsumption);
                        energyPoint.ToolTip = $"Energy Consumption: {energyConsumption}";

                        DataPoint emissionPoint = new DataPoint();
                        emissionPoint.SetValueXY(supplierName, greenhouseEmission);
                        emissionPoint.ToolTip = $"Greenhouse Emission: {greenhouseEmission}";

                        lineChartSupply.Series["Energy Consumption"].Points.Add(energyPoint);
                        lineChartSupply.Series["Greenhouse Emission"].Points.Add(emissionPoint);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load line chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureLineChart()
        {
            lineChartSupply.Series.Clear();
            lineChartSupply.ChartAreas.Clear();

            ChartArea lineChartArea = new ChartArea("LineChartArea");
            lineChartSupply.ChartAreas.Add(lineChartArea);
            lineChartArea.AxisX.Interval = 1;  // Ensure every label is shown
            lineChartArea.AxisX.LabelStyle.Angle = -45;  // Angle labels for better fit
            lineChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);  // Use a smaller font if needed

            // Configure AxisY
            lineChartArea.AxisY.Title = "Value";
            lineChartArea.AxisY.Minimum = 0;
            // Adjust maximum and interval based on your data scale

            Series seriesEnergy = new Series("Energy Consumption");
            seriesEnergy.ChartType = SeriesChartType.Line;
            seriesEnergy.Color = Color.Green;
            lineChartSupply.Series.Add(seriesEnergy);

            Series seriesEmission = new Series("Greenhouse Emission");
            seriesEmission.ChartType = SeriesChartType.Line;
            seriesEmission.Color = Color.Blue;
            lineChartSupply.Series.Add(seriesEmission);

            FetchLineChartData(); // This method needs to populate the series with data
        }
        private void FetchPieChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT EthicalCertifications, COUNT(*) AS CertificationCount FROM dbo.SupplyChainData GROUP BY EthicalCertifications";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string certificationType = reader["EthicalCertifications"].ToString();
                        int certificationCount = Convert.ToInt32(reader["CertificationCount"]);

                        // Add data point to the pie chart series
                        pieChartSupply.Series["EthicalCertifications"].Points.AddXY(certificationType, certificationCount);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load pie chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        private void ConfigurePieChart()
        {
            pieChartSupply.Series.Clear();
            pieChartSupply.ChartAreas.Clear();

            ChartArea pieChartArea = new ChartArea("PieChartArea");
            pieChartSupply.ChartAreas.Add(pieChartArea);

            Series pieSeries = new Series("EthicalCertifications")
            {
                ChartType = SeriesChartType.Pie
            };
            pieChartSupply.Series.Add(pieSeries);

            FetchPieChartData(); // This method needs to populate the series with data
        }

        private void barChartSupply_Click(object sender, EventArgs e)
        {

        }

        private void lineChartSupply_Click(object sender, EventArgs e)
        {

        }

        private void pieChartSupply_Click(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductSustainabilityAnalysis product = new ProductSustainabilityAnalysis();
            product.Show();
            this.Hide();
        }

        private void btnGoals_Click(object sender, EventArgs e)
        {
            GoalTrackingProgressAnalysis goalTrackingProgressAnalysis = new GoalTrackingProgressAnalysis();
            goalTrackingProgressAnalysis.Show();
            this.Hide();
        }
    }
}
