﻿using System;
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
    public partial class ComplianceAndSafety : Form
    {
        public ComplianceAndSafety()
        {
            InitializeComponent();
            ConfigureCompliancePieChart();
            ConfigureComplianceStackedBarChart();
        }

        private void ConfigureCompliancePieChart()
        {
            pieCompliance.Series.Clear();
            pieCompliance.ChartAreas.Clear();

            ChartArea pieArea = new ChartArea("CompliancePieArea");
            pieCompliance.ChartAreas.Add(pieArea);

            Series pieSeries = new Series("Compliance")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };
            pieCompliance.Series.Add(pieSeries);

            FetchPieChartData(); // This method will now fetch and add textual data
        }

        private void ConfigureComplianceStackedBarChart()
        {
            stackedCompliance.Series.Clear();
            stackedCompliance.ChartAreas.Clear();

            ChartArea stackedBarArea = new ChartArea("ComplianceStackedBarArea");
            stackedCompliance.ChartAreas.Add(stackedBarArea);
            stackedBarArea.AxisX.Interval = 1;  // Ensure every label is shown
            stackedBarArea.AxisX.LabelStyle.Angle = -45;  // Angle labels for better fit
            stackedBarArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);  // Use a smaller font if needed

            // Configure AxisY
            stackedBarArea.AxisY.Minimum = 0;
            stackedBarArea.AxisY.Maximum = 100;  // Assuming the values are percentages
            stackedBarArea.AxisY.Interval = 20;  // Adjust based on your data scale

            Series seriesLabor = new Series("Labor Law Compliance")
            {
                ChartType = SeriesChartType.StackedBar,
                Color = Color.Green
            };
            stackedCompliance.Series.Add(seriesLabor);

            Series seriesSafety = new Series("Health Safety Compliance")
            {
                ChartType = SeriesChartType.StackedBar,
                Color = Color.Blue
            };
            stackedCompliance.Series.Add(seriesSafety);

            FetchStackedBarData(); // This method needs to populate these series with data
        }

        private void FetchPieChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = @"
        SELECT ComplianceLaborLaws, COUNT(*) AS Count FROM SupplyChainData GROUP BY ComplianceLaborLaws
        UNION ALL
        SELECT HealthSafetyMeasures, COUNT(*) FROM SupplyChainData GROUP BY HealthSafetyMeasures";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string status = reader[0].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        pieCompliance.Series["Compliance"].Points.AddXY(status, count);
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

        private void FetchStackedBarData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT SupplierName, ComplianceLaborLaws, HealthSafetyMeasures FROM SupplyChainData";

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
                        string laborCompliance = reader["ComplianceLaborLaws"].ToString();
                        string safetyCompliance = reader["HealthSafetyMeasures"].ToString();

                        // Add points to the series
                        DataPoint laborPoint = new DataPoint();
                        laborPoint.SetValueXY(supplierName, 100);  // Use full bar length for visibility
                        laborPoint.ToolTip = laborCompliance;  // Use tooltips instead of labels

                        DataPoint safetyPoint = new DataPoint();
                        safetyPoint.SetValueXY(supplierName, 100);  // Use full bar length for visibility
                        safetyPoint.ToolTip = safetyCompliance;  // Use tooltips instead of labels

                        stackedCompliance.Series["Labor Law Compliance"].Points.Add(laborPoint);
                        stackedCompliance.Series["Health Safety Compliance"].Points.Add(safetyPoint);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load stacked bar chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        private void ComplianceAndSafety_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ComplianceAndSafety complianceAndSafety = new ComplianceAndSafety();
            complianceAndSafety.Show();
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

        private void pieCompliance_Click(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            LoggedIn loggedIn = new LoggedIn();
            loggedIn.Show();
            this.Hide();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {

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
