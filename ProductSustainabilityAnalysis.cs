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
using static EcoLogiX_New.GoalTrackingProgressAnalysis;

namespace EcoLogiX_New
{
    public partial class ProductSustainabilityAnalysis : Form
    {
        public ProductSustainabilityAnalysis()
        {
            InitializeComponent();
            ConfigureGoalDataGrid(new List<Goal>());
            FetchBarChartData();
        }

        private void FetchBarChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT [ProductName], [ProductDescription], [ProductCategory] FROM dbo.SupplyChainData";

            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dataTable.Load(reader); // Load data into the DataTable
                    dataGridProduct.DataSource = dataTable; // Bind DataTable to DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data into DataGridView: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureGoalDataGrid(List<Goal> goals)
        {
            dataGridProduct.Columns.Clear();  // Clear existing columns if any

            // Adding a column for Product Name
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName", // Bind to the ProductName property of the Goal object
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Adjust size to fill part of the grid
            };
            dataGridProduct.Columns.Add(productNameColumn);

            // Adding a column for Product Description
            DataGridViewTextBoxColumn productDescriptionColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductDescription",
                HeaderText = "Product Description",
                DataPropertyName = "ProductDescription", // Bind to the ProductDescription property of the Goal object
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Adjust size to fill part of the grid
            };
            dataGridProduct.Columns.Add(productDescriptionColumn);

            // Adding a column for Product Category
            DataGridViewTextBoxColumn productCategoryColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductCategory",
                HeaderText = "Product Category",
                DataPropertyName = "ProductCategory", // Bind to the ProductCategory property of the Goal object
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill // Adjust size to fill part of the grid
            };
            dataGridProduct.Columns.Add(productCategoryColumn);

            // Bind data
            dataGridProduct.DataSource = goals;

            // Styling
            dataGridProduct.DefaultCellStyle.Font = new Font("Arial", 10);  // Set default font
            dataGridProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  // Alternating row colors for better readability
            dataGridProduct.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);  // Header style
            dataGridProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;  // Header background color
            dataGridProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Header text color
            dataGridProduct.EnableHeadersVisualStyles = false;  // Apply custom style to headers

            // Grid behavior settings
            dataGridProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridProduct.MultiSelect = false;  // Prevent multi-row selection

            // Handle CellClick event
            dataGridProduct.CellClick += DataGridGoals_CellClick;
        }

        private void DataGridGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is valid and not a header cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell clickedCell = dataGridProduct.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellValue = clickedCell.Value?.ToString(); // Get cell value

                // Display cell value
                MessageBox.Show(cellValue, "Cell Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            LoggedInTwo loggedIn = new LoggedInTwo();
            loggedIn.Show();
            this.Hide();
        }

        private void barChartSupply_Click(object sender, EventArgs e)
        {

        }

        private void dataGridProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
