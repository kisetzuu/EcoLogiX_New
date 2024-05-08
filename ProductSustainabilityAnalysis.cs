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
    public partial class ProductSustainabilityAnalysis : Form
    {
        public ProductSustainabilityAnalysis()
        {
            InitializeComponent();
        }

        private void FetchBarChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT [ProductName], [ProductDescription], [ProductCategory] FROM dbo.SupplyChainData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string productName = reader["ProductName"].ToString();
                        string productDescription = reader["ProductDescription"].ToString();
                        string productCategory = reader["ProductCategory"].ToString();

                        // Add points to the series
                        DataPoint productNamePoint = new DataPoint();
                        productNamePoint.SetValueXY(productName, 0);  // Placeholder value for bar chart
                        productNamePoint.ToolTip = $"Product Description: {productDescription}\nProduct Category: {productCategory}";

                        barChartSupply.Series["Product Name"].Points.Add(productNamePoint);

                        // Add points to the series
                        DataPoint productDescriptionPoint = new DataPoint();
                        productDescriptionPoint.SetValueXY(productName, 0);  // Placeholder value for bar chart
                        productDescriptionPoint.ToolTip = $"Product Name: {productName}\nProduct Category: {productCategory}";

                        barChartSupply.Series["Product Description"].Points.Add(productDescriptionPoint);

                        // Add points to the series
                        DataPoint productCategoryPoint = new DataPoint();
                        productCategoryPoint.SetValueXY(productName, 0);  // Placeholder value for bar chart
                        productCategoryPoint.ToolTip = $"Product Name: {productName}\nProduct Description: {productDescription}";

                        barChartSupply.Series["Product Category"].Points.Add(productCategoryPoint);
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

            Series seriesProductName = new Series("Product Name")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.Green
            };
            barChartSupply.Series.Add(seriesProductName);

            Series seriesProductDescription = new Series("Product Description")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.Blue
            };
            barChartSupply.Series.Add(seriesProductDescription);

            Series seriesProductCategory = new Series("Product Category")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.Red
            };
            barChartSupply.Series.Add(seriesProductCategory);

            // Call the method to fetch data
            FetchBarChartData();
        }

        private void FetchLineChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT TotalEnergyConsumption, TotalGreenhouseEmission FROM dbo.SupplyChainData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        float energyConsumption = Convert.ToSingle(reader["TotalEnergyConsumption"]);
                        float greenhouseEmission = Convert.ToSingle(reader["TotalGreenhouseEmission"]);

                        // Add points to the series
                        lineChartSupply.Series["Total Energy Consumption"].Points.AddY(energyConsumption);
                        lineChartSupply.Series["Total Greenhouse Emission"].Points.AddY(greenhouseEmission);
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

            Series seriesEnergyConsumption = new Series("Total Energy Consumption")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green
            };
            lineChartSupply.Series.Add(seriesEnergyConsumption);

            Series seriesGreenhouseEmission = new Series("Total Greenhouse Emission")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue
            };
            lineChartSupply.Series.Add(seriesGreenhouseEmission);

            // Call the method to fetch data
            FetchLineChartData();
        }

        private void FetchComplianceData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT ComplianceDocumentationStatus, COUNT(*) AS StatusCount FROM dbo.SupplyChainData GROUP BY ComplianceDocumentationStatus";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string status = reader["ComplianceDocumentationStatus"].ToString();
                        int count = Convert.ToInt32(reader["StatusCount"]);

                        // Add data point to the pie chart series
                        pieChartSupply.Series["Compliance Documentation Status"].Points.AddXY(status, count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load compliance data: " + ex.Message);
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

            Series seriesCompliance = new Series("Compliance Documentation Status");
            seriesCompliance.ChartType = SeriesChartType.Pie;
            pieChartSupply.Series.Add(seriesCompliance);

            // Call the method to fetch data
            FetchComplianceData();

            // Customize the appearance of the pie chart
            foreach (Series series in pieChartSupply.Series)
            {
                series.Label = "#VALX: #PERCENT";
                series["PieLabelStyle"] = "Outside";
            }
        }






        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ProductSustainabilityAnalysis_Load(object sender, EventArgs e)
        {
            ConfigureBarChart();
            ConfigureLineChart();
            ConfigurePieChart();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            SustainabilityReporting sustainabilityReporting = new SustainabilityReporting();
            sustainabilityReporting.Show();
            this.Hide();
        }

        private void btnGoals_Click(object sender, EventArgs e)
        {
            GoalTrackingProgressAnalysis goalTrackingProgressAnalysis = new GoalTrackingProgressAnalysis();
            goalTrackingProgressAnalysis.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Engagement engagementForm = new Engagement();
            engagementForm.Show();
            this.Hide();
        }
    }
}
