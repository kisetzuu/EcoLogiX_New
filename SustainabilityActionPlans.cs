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

namespace EcoLogiX_New
{
    public partial class SustainabilityActionPlans : Form
    {
        public SustainabilityActionPlans()
        {
            InitializeComponent();
        }
        public static class UserSession
        {
            public static int UserID { get; set; }
            // Other session-related properties can be added here
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Check if priority level is selected
            if (comboPrio.SelectedItem == null)
            {
                MessageBox.Show("Please select a priority level.");
                return;
            }

            // Check if goal description is provided
            string goalDescription = txtGoal.Text.Trim();
            if (string.IsNullOrEmpty(goalDescription))
            {
                MessageBox.Show("Please enter a goal description.");
                return;
            }

            // Get the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;

            // Prepare the query
            string query = @"
    INSERT INTO dbo.SustainabilityGoals (UserID, GoalDescription, TargetDate, PriorityLevel, Notes) 
    VALUES (@UserID, @GoalDescription, @TargetDate, @PriorityLevel, @Notes)";

            // Execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the parameters
                    cmd.Parameters.AddWithValue("@UserID", UserSession.UserID);
                    cmd.Parameters.AddWithValue("@GoalDescription", goalDescription);
                    cmd.Parameters.AddWithValue("@TargetDate", dateTarget.Value.Date);
                    cmd.Parameters.AddWithValue("@PriorityLevel", comboPrio.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Notes", textNotes.Text.Trim());

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data added successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add data. Please try again.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on a valid cell
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string selectedGoalDescription = dataGridSustainability.Rows[e.RowIndex].Cells["GoalDescription"].Value.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT GoalDescription, TargetDate, PriorityLevel, Notes 
                        FROM dbo.SustainabilityGoals 
                        WHERE UserID = @UserID AND GoalDescription = @GoalDescription";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", UserSession.UserID);
                        cmd.Parameters.AddWithValue("@GoalDescription", selectedGoalDescription);

                        try
                        {
                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string goalDescription = reader["GoalDescription"].ToString();
                                    DateTime targetDate = Convert.ToDateTime(reader["TargetDate"]);
                                    string priorityLevel = reader["PriorityLevel"].ToString();
                                    string notes = reader["Notes"].ToString();

                                    // Display the goal details
                                    MessageBox.Show($"Goal: {goalDescription}\nTarget Date: {targetDate}\nPriority: {priorityLevel}\nNotes: {notes}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to load goal details: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void ConfigureSustainabilityDataGridView()
        {
            dataGridSustainability.Columns.Clear();  // Clear existing columns if any

            // Adding a column for Goal Description
            DataGridViewTextBoxColumn goalDescriptionColumn = new DataGridViewTextBoxColumn();
            goalDescriptionColumn.Name = "GoalDescription";
            goalDescriptionColumn.HeaderText = "Goal Description";
            goalDescriptionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridSustainability.Columns.Add(goalDescriptionColumn);

            // Adding a column for Target Date
            DataGridViewTextBoxColumn targetDateColumn = new DataGridViewTextBoxColumn();
            targetDateColumn.Name = "TargetDate";
            targetDateColumn.HeaderText = "Target Date";
            targetDateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridSustainability.Columns.Add(targetDateColumn);

            // Adding a column for Priority Level
            DataGridViewTextBoxColumn priorityLevelColumn = new DataGridViewTextBoxColumn();
            priorityLevelColumn.Name = "PriorityLevel";
            priorityLevelColumn.HeaderText = "Priority Level";
            priorityLevelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridSustainability.Columns.Add(priorityLevelColumn);

            // Adding a column for Notes
            DataGridViewTextBoxColumn notesColumn = new DataGridViewTextBoxColumn();
            notesColumn.Name = "Notes";
            notesColumn.HeaderText = "Notes";
            notesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridSustainability.Columns.Add(notesColumn);

            // Styling
            dataGridSustainability.DefaultCellStyle.Font = new Font("Arial", 10);  // Set default font
            dataGridSustainability.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  // Alternating row colors for better readability
            dataGridSustainability.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);  // Header style
            dataGridSustainability.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;  // Header background color
            dataGridSustainability.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Header text color
            dataGridSustainability.EnableHeadersVisualStyles = false;  // Apply custom style to headers

            // Grid behavior settings
            dataGridSustainability.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridSustainability.MultiSelect = false;  // Prevent multi-row selection
        }


        private void SustainabilityActionPlans_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            // Configure the DataGridView
            ConfigureSustainabilityDataGridView();

            // Get the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;

            // Prepare the query
            string query = @"
    SELECT GoalDescription, TargetDate, PriorityLevel, Notes 
    FROM dbo.SustainabilityGoals 
    WHERE UserID = @UserID";

            // Execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add the parameters
                    cmd.Parameters.AddWithValue("@UserID", UserSession.UserID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string goalDescription = reader["GoalDescription"].ToString();
                                DateTime targetDate = Convert.ToDateTime(reader["TargetDate"]);
                                string priorityLevel = reader["PriorityLevel"].ToString();
                                string notes = reader["Notes"].ToString();

                                // Add a new row to DataGridView for each goal
                                dataGridSustainability.Rows.Add(goalDescription, targetDate, priorityLevel, notes);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to load goals: " + ex.Message);
                    }
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
    }
}
