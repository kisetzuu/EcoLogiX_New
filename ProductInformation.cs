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
    public partial class ProductInformation : Form
    {
        public ProductInformation()
        {
            InitializeComponent();
            ConfigurePieChart();
        }

        private void ConfigurePieChart()
        {
            productChart.Series.Clear(); // Clears any existing series
            productChart.ChartAreas.Clear(); // Clears any existing chart areas

            // Create and configure the chart area
            ChartArea chartArea = new ChartArea("PieArea");
            productChart.ChartAreas.Add(chartArea);

            // Remove grid lines, axis lines, and labels to clean up the background
            chartArea.AxisX.LineWidth = 0;
            chartArea.AxisY.LineWidth = 0;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisX.LabelStyle.Enabled = false;
            chartArea.AxisY.LabelStyle.Enabled = false;
            chartArea.BackColor = Color.Transparent; // Set background color to transparent

            // Create and configure the series for Pie chart
            Series series = new Series("ProductCategories")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true, // To show labels on the pie slices
                BorderColor = Color.Transparent, // Optional: set border color of pie slices to transparent
            };
            productChart.Series.Add(series);

            // Optionally, set more properties, like colors, labels, etc.
            productChart.Series["ProductCategories"].Label = "#PERCENT{P2}";
            productChart.Series["ProductCategories"].LegendText = "#VALX (#PERCENT)";

            // Center the pie chart and adjust the size
            productChart.Series["ProductCategories"]["PieLabelStyle"] = "Outside"; // Position labels outside the pie
            productChart.Series["ProductCategories"]["PieStartAngle"] = "270"; // Start angle to rotate the pie

            // Dynamically adjust to a bigger size
            chartArea.Position = new ElementPosition(5, 10, 90, 85); // Adjust these values to resize and reposition the chart area
            chartArea.InnerPlotPosition = new ElementPosition(0, 0, 100, 100); // Use this to adjust the plotting area within the chart area

            // Fetch data and bind to the chart
            FetchAndDisplayProductCategories();
        }

        private void FetchAndDisplayProductCategories()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT ProductCategory, COUNT(*) AS CategoryCount FROM dbo.SupplyChainData GROUP BY ProductCategory";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string category = reader["ProductCategory"].ToString();
                            int count = Convert.ToInt32(reader["CategoryCount"]);
                            productChart.Series["ProductCategories"].Points.AddXY(category, count);
                        }
                    } // Automatically closes the reader
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load product categories: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Ensure the connection is closed
                }
            }
        }

        private void productChart_Click(object sender, EventArgs e)
        {

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
    }
}
