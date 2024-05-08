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
    public partial class GoalTrackingProgressAnalysis : Form
    {
        public GoalTrackingProgressAnalysis()
        {
            InitializeComponent();
        }

        private List<Goal> FetchGoalData()
        {
            List<Goal> goals = new List<Goal>();

            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            string query = "SELECT [GoalDescription], [TargetDate], [Notes] FROM dbo.SustainabilityGoals";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string goalDescription = reader["GoalDescription"].ToString();
                        DateTime targetDate = Convert.ToDateTime(reader["TargetDate"]);
                        string notes = reader["Notes"].ToString();

                        goals.Add(new Goal { GoalDescription = goalDescription, TargetDate = targetDate, Notes = notes });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to fetch goal data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return goals;
        }

        private void ConfigureGoalDataGrid(List<Goal> goals)
        {
            dataGridGoals.Columns.Clear();  // Clear existing columns if any

            // Adding a column for Goal Description
            DataGridViewTextBoxColumn goalDescriptionColumn = new DataGridViewTextBoxColumn();
            goalDescriptionColumn.Name = "GoalDescription";
            goalDescriptionColumn.HeaderText = "Goal Description";
            goalDescriptionColumn.DataPropertyName = "GoalDescription"; // Bind to the GoalDescription property of the Goal object
            goalDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridGoals.Columns.Add(goalDescriptionColumn);

            // Adding a column for Target Date
            DataGridViewTextBoxColumn targetDateColumn = new DataGridViewTextBoxColumn();
            targetDateColumn.Name = "TargetDate";
            targetDateColumn.HeaderText = "Target Date";
            targetDateColumn.DataPropertyName = "TargetDate"; // Bind to the TargetDate property of the Goal object
            targetDateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridGoals.Columns.Add(targetDateColumn);

            // Adding a column for Notes
            DataGridViewTextBoxColumn notesColumn = new DataGridViewTextBoxColumn();
            notesColumn.Name = "Notes";
            notesColumn.HeaderText = "Notes";
            notesColumn.DataPropertyName = "Notes"; // Bind to the Notes property of the Goal object
            notesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridGoals.Columns.Add(notesColumn);

            // Styling
            dataGridGoals.DefaultCellStyle.Font = new Font("Arial", 10);  // Set default font
            dataGridGoals.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  // Alternating row colors for better readability
            dataGridGoals.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);  // Header style
            dataGridGoals.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;  // Header background color
            dataGridGoals.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Header text color
            dataGridGoals.EnableHeadersVisualStyles = false;  // Apply custom style to headers

            // Grid behavior settings
            dataGridGoals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridGoals.MultiSelect = false;  // Prevent multi-row selection

            // Bind data
            dataGridGoals.DataSource = goals;

            // Handle CellClick event
            dataGridGoals.CellClick += DataGridGoals_CellClick;
        }

        private void DataGridGoals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is valid and not a header cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell clickedCell = dataGridGoals.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string cellValue = clickedCell.Value?.ToString(); // Get cell value

                // Display cell value
                MessageBox.Show(cellValue, "Cell Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public class Goal
        {
            public string GoalDescription { get; set; }
            public DateTime TargetDate { get; set; }
            public string Notes { get; set; }
        }

        private void GoalTrackingProgressAnalysis_Load(object sender, EventArgs e)
        {
            // Configure priority level bar chart
            ConfigurePriorityLevelBarChart(FetchPriorityCounts());

            // Configure target date line chart
            ConfigureTargetDateLineChart(FetchTargetDates());

            // Fetch goal data and display in data grid
            List<Goal> goals = FetchGoalData();
            ConfigureGoalDataGrid(goals);
        }


        private Dictionary<string, int> FetchPriorityCounts()
        {
            Dictionary<string, int> priorityCounts = new Dictionary<string, int>();

            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            string query = "SELECT PriorityLevel, COUNT(*) AS PriorityCount FROM dbo.SustainabilityGoals GROUP BY PriorityLevel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string priorityLevel = reader["PriorityLevel"].ToString();
                        int priorityCount = Convert.ToInt32(reader["PriorityCount"]);
                        priorityCounts.Add(priorityLevel, priorityCount);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to fetch priority counts: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return priorityCounts;
        }

        private List<DateTime> FetchTargetDates()
        {
            List<DateTime> targetDates = new List<DateTime>();

            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            string query = "SELECT TargetDate FROM dbo.SustainabilityGoals";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime targetDate = Convert.ToDateTime(reader["TargetDate"]);
                        targetDates.Add(targetDate);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to fetch target dates: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return targetDates;
        }

        private void ConfigurePriorityLevelBarChart(Dictionary<string, int> priorityCounts)
        {
            barChartSupply.Series.Clear();
            barChartSupply.ChartAreas.Clear();

            ChartArea barChartArea = new ChartArea("BarChartArea");
            barChartSupply.ChartAreas.Add(barChartArea);
            barChartArea.AxisX.Interval = 1;
            barChartArea.AxisX.LabelStyle.Angle = -45;
            barChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);

            barChartArea.AxisY.Title = "Count";
            barChartArea.AxisY.Minimum = 0;

            Series seriesPriority = new Series("Priority Level");
            seriesPriority.ChartType = SeriesChartType.Column;
            barChartSupply.Series.Add(seriesPriority);

            foreach (var priority in priorityCounts)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(priority.Key, priority.Value);
                seriesPriority.Points.Add(dataPoint);
            }
        }

        private void ConfigureTargetDateLineChart(List<DateTime> targetDates)
        {
            lineChartSupply.Series.Clear();
            lineChartSupply.ChartAreas.Clear();

            ChartArea lineChartArea = new ChartArea("LineChartArea");
            lineChartSupply.ChartAreas.Add(lineChartArea);
            lineChartArea.AxisX.Interval = 1;
            lineChartArea.AxisX.LabelStyle.Angle = -45;
            lineChartArea.AxisX.LabelStyle.Font = new Font("Verdana", 8);

            lineChartArea.AxisY.Title = "Target Date Count";
            lineChartArea.AxisY.Minimum = 0;

            Series seriesTargetDate = new Series("Target Date");
            seriesTargetDate.ChartType = SeriesChartType.Line;
            lineChartSupply.Series.Add(seriesTargetDate);

            Dictionary<DateTime, int> dateCounts = new Dictionary<DateTime, int>();
            foreach (var date in targetDates)
            {
                if (dateCounts.ContainsKey(date))
                    dateCounts[date]++;
                else
                    dateCounts[date] = 1;
            }

            foreach (var dateCount in dateCounts)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.SetValueXY(dateCount.Key.ToShortDateString(), dateCount.Value);
                seriesTargetDate.Points.Add(dataPoint);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridGoals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            SustainabilityReporting sustainabilityReporting = new SustainabilityReporting();
            sustainabilityReporting.Show();
            this.Hide();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductSustainabilityAnalysis analysis = new ProductSustainabilityAnalysis();
            analysis.Show();
            this.Hide();
        }
    }
}
