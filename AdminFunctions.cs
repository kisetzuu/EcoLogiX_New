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
using System.Windows.Input;

namespace EcoLogiX_New
{
    public partial class AdminFunctions : Form
    {

        private string currentDatasetType;
        public AdminFunctions()
        {
            InitializeComponent();
        }

        private void btnCertifications_Click(object sender, EventArgs e)
        {
            // Define the connection string
            string connectionString = ConfigurationManager.ConnectionStrings["CertificationsDb"].ConnectionString;
            // SQL query to fetch all data from the dbo.Certifications table, including CertificationID
            string query = "SELECT [CertificationID], [CertificationName], [CertificationBody], [DocumentName], [DocumentType], [UserID] FROM dbo.Certifications";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridCertifications DataGridView
                    dataGridCertifications.DataSource = dt;

                    // Hide the CertificationID and UserID columns from user view
                    dataGridCertifications.Columns["CertificationID"].Visible = false;
                    dataGridCertifications.Columns["UserID"].Visible = false;

                    // Optionally, adjust column settings for readability and usability
                    dataGridCertifications.AutoResizeColumns();  // Adjust column widths for better readability
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void bnSupplyChainData_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            // SQL query to fetch all data from the dbo.SupplyChainData table, including the Id column
            string query = "SELECT [Id], [UserId], [SupplierName], [ContactPerson], [EmailAddress], [PhoneNumber], [LocationInformation], [ProductName], [ProductDescription], [ProductCategory], [TotalEnergyConsumption], [TotalGreenhouseEmission], [ComplianceLaborLaws], [HealthSafetyMeasures], [TraceabilityRawMaterials], [EthicalCertifications], [RelevantCertifications], [ComplianceDocumentationStatus] FROM dbo.SupplyChainData";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridSupply DataGridView
                    dataGridSupply.DataSource = dt;

                    // Hide the Id column from user view
                    dataGridSupply.Columns["Id"].Visible = false;

                    // Optional: Adjust column settings for readability and usability
                    dataGridSupply.AutoResizeColumns();  // Adjust column widths for better readability
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void btnSustainability_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            // SQL query to fetch specified data from the dbo.SustainabilityGoals table
            string query = "SELECT [ID], [GoalDescription], [TargetDate], [PriorityLevel], [Notes], [UserID] FROM dbo.SustainabilityGoals";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridSustainability DataGridView
                    dataGridSustainability.DataSource = dt;

                    // Optional: Adjust column settings for readability and usability
                    dataGridSustainability.AutoResizeColumns(); // Adjust column widths for better readability
                    dataGridSustainability.Columns["UserID"].Visible = false; // Hide UserID if not needed visibly
                    dataGridSustainability.Columns["ID"].Visible = false; // Hide ID from the user view
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDb"].ConnectionString;
            // SQL query to fetch specified data from the dbo.Users table
            string query = "SELECT [ID], [Name], [Email], [Password], [CompanyName], [Role], [Contact] FROM dbo.Users";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind the data to the dataGridUsers DataGridView
                    dataGridUsers.DataSource = dt;

                    // Optional: Adjust column settings for readability and usability
                    dataGridUsers.AutoResizeColumns();  // Adjust column widths for better readability
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
            }
        }



        private void dataGridAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridUsers.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected user?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = dataGridUsers.SelectedRows[0].Index;
                    string userId = dataGridUsers.SelectedRows[0].Cells["ID"].Value.ToString();

                    string connectionString = ConfigurationManager.ConnectionStrings["UsersDb"].ConnectionString;
                    string deleteCommand = "DELETE FROM dbo.Users WHERE ID = @UserID";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                dataGridUsers.Rows.RemoveAt(selectedIndex);
                                MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("User could not be deleted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemoveGoal_Click(object sender, EventArgs e)
        {
            if (dataGridSustainability.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected goal?", "Delete Goal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = dataGridSustainability.SelectedRows[0].Index;
                    string goalId = dataGridSustainability.SelectedRows[0].Cells["ID"].Value.ToString();

                    string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
                    string deleteCommand = "DELETE FROM dbo.SustainabilityGoals WHERE ID = @GoalID";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                        cmd.Parameters.AddWithValue("@GoalID", goalId);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                dataGridSustainability.Rows.RemoveAt(selectedIndex);
                                MessageBox.Show("Goal deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Goal could not be deleted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a goal to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemoveSupply_Click(object sender, EventArgs e)
        {
            if (dataGridSupply.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected supplier data?", "Delete Supplier Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = dataGridSupply.SelectedRows[0].Index;
                    string supplyId = dataGridSupply.SelectedRows[0].Cells["Id"].Value.ToString();

                    string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
                    string deleteCommand = "DELETE FROM dbo.SupplyChainData WHERE Id = @SupplyId";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                        cmd.Parameters.AddWithValue("@SupplyId", supplyId);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                dataGridSupply.Rows.RemoveAt(selectedIndex);
                                MessageBox.Show("Supplier data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Supplier data could not be deleted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a supplier data row to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemoveCert_Click(object sender, EventArgs e)
        {
            if (dataGridCertifications.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the selected certification?", "Delete Certification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int selectedIndex = dataGridCertifications.SelectedRows[0].Index;
                    string certificationId = dataGridCertifications.SelectedRows[0].Cells["CertificationID"].Value.ToString();

                    string connectionString = ConfigurationManager.ConnectionStrings["CertificationsDb"].ConnectionString;
                    string deleteCommand = "DELETE FROM dbo.Certifications WHERE CertificationID = @CertificationID";
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand(deleteCommand, conn);
                        cmd.Parameters.AddWithValue("@CertificationID", certificationId);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                dataGridCertifications.Rows.RemoveAt(selectedIndex);
                                MessageBox.Show("Certification deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Certification could not be deleted.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occurred: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a certification row to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetupDataGridView()
        {
            dataGridUsers.ReadOnly = false;  // Allow editing
            dataGridUsers.Columns["UserID"].ReadOnly = true;  // Prevent editing of UserID column
                                                              // Configure other properties
            dataGridUsers.AutoResizeColumns();
            dataGridUsers.AllowUserToAddRows = false;  // Disable adding new rows by the user
        }

        private DataTable userData;

        private void LoadUserData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, Name, Email, Role FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                userData = new DataTable();
                adapter.Fill(userData);
                dataGridUsers.DataSource = userData;
            }
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Define the update command that updates the specified fields
                    string updateCommand = "UPDATE Users SET Name = @Name, Email = @Email, Role = @Role WHERE UserID = @UserID";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand(updateCommand, conn);
                    adapter.UpdateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 50, "Name");
                    adapter.UpdateCommand.Parameters.Add("@Email", SqlDbType.VarChar, 50, "Email");
                    adapter.UpdateCommand.Parameters.Add("@Role", SqlDbType.VarChar, 50, "Role");
                    adapter.UpdateCommand.Parameters.Add("@UserID", SqlDbType.Int, 10, "UserID");

                    // Open connection and update the database
                    conn.Open();
                    adapter.Update(userData);
                    MessageBox.Show("User data updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating user data: " + ex.Message);
                }
            }

            // Optionally, reload or refresh data to show updated data
            LoadUserData();
        }

        private DataTable goalsData;

        private void LoadGoalsData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT GoalID, GoalDescription, TargetDate, PriorityLevel, Notes FROM SustainabilityGoals";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                goalsData = new DataTable();
                adapter.Fill(goalsData);
                dataGridSustainability.DataSource = goalsData;
            }
        }


        private void btnModifyGoal_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["SustainabilityGoalsDb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Define the update command
                    string updateCommand = "UPDATE SustainabilityGoals SET GoalDescription = @GoalDescription, TargetDate = @TargetDate, PriorityLevel = @PriorityLevel, Notes = @Notes WHERE GoalID = @GoalID";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand(updateCommand, conn);
                    adapter.UpdateCommand.Parameters.Add("@GoalDescription", SqlDbType.NVarChar, 255, "GoalDescription");
                    adapter.UpdateCommand.Parameters.Add("@TargetDate", SqlDbType.Date, 0, "TargetDate");
                    adapter.UpdateCommand.Parameters.Add("@PriorityLevel", SqlDbType.Int, 0, "PriorityLevel");
                    adapter.UpdateCommand.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000, "Notes");
                    adapter.UpdateCommand.Parameters.Add("@GoalID", SqlDbType.Int, 0, "GoalID");

                    // Open connection and update the database
                    conn.Open();
                    adapter.Update(goalsData);
                    MessageBox.Show("Sustainability goals updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating goals: " + ex.Message);
                } // Missing closing brace added here for the catch block
            }

            // Optionally, reload data to show updated data
            LoadGoalsData();
        }

        private void SetupDataGridViewForSupply()
        {
            dataGridSupply.ReadOnly = false;  // Enable editing
            dataGridSupply.Columns["Id"].ReadOnly = true;  // Prevent editing of Id column
            dataGridSupply.AllowUserToAddRows = false;  // Prevent user from adding rows directly
        }

        private DataTable supplyData;

        private void LoadSupplyData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, SupplierName, ContactPerson, EmailAddress, PhoneNumber, LocationInformation, ProductName, ProductDescription, ProductCategory, TotalEnergyConsumption, TotalGreenhouseEmission, ComplianceLaborLaws, HealthSafetyMeasures, TraceabilityRawMaterials, EthicalCertifications, RelevantCertifications, ComplianceDocumentationStatus FROM SupplyChainData";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                supplyData = new DataTable();
                adapter.Fill(supplyData);
                dataGridSupply.DataSource = supplyData;
            }
        }


        private void btnModifySupply_Click(object sender, EventArgs e)
        {
            // Define the connection string from your application settings
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Define the update command
                    string updateCommand = "UPDATE SupplyChainData SET SupplierName = @SupplierName, ContactPerson = @ContactPerson, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber, LocationInformation = @LocationInformation, ProductName = @ProductName, ProductDescription = @ProductDescription, ProductCategory = @ProductCategory, TotalEnergyConsumption = @TotalEnergyConsumption, TotalGreenhouseEmission = @TotalGreenhouseEmission, ComplianceLaborLaws = @ComplianceLaborLaws, HealthSafetyMeasures = @HealthSafetyMeasures, TraceabilityRawMaterials = @TraceabilityRawMaterials, EthicalCertifications = @EthicalCertifications, RelevantCertifications = @RelevantCertifications, ComplianceDocumentationStatus = @ComplianceDocumentationStatus WHERE Id = @Id";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand(updateCommand, conn);
                    adapter.UpdateCommand.Parameters.Add("@SupplierName", SqlDbType.NVarChar, 50, "SupplierName");
                    adapter.UpdateCommand.Parameters.Add("@ContactPerson", SqlDbType.NVarChar, 50, "ContactPerson");
                    adapter.UpdateCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 50, "EmailAddress");
                    adapter.UpdateCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15, "PhoneNumber");
                    adapter.UpdateCommand.Parameters.Add("@LocationInformation", SqlDbType.NVarChar, 100, "LocationInformation");
                    adapter.UpdateCommand.Parameters.Add("@ProductName", SqlDbType.NVarChar, 50, "ProductName");
                    adapter.UpdateCommand.Parameters.Add("@ProductDescription", SqlDbType.NVarChar, 255, "ProductDescription");
                    adapter.UpdateCommand.Parameters.Add("@ProductCategory", SqlDbType.NVarChar, 50, "ProductCategory");
                    adapter.UpdateCommand.Parameters.Add("@TotalEnergyConsumption", SqlDbType.Float, 0, "TotalEnergyConsumption");
                    adapter.UpdateCommand.Parameters.Add("@TotalGreenhouseEmission", SqlDbType.Float, 0, "TotalGreenhouseEmission");
                    adapter.UpdateCommand.Parameters.Add("@ComplianceLaborLaws", SqlDbType.NVarChar, 50, "ComplianceLaborLaws");
                    adapter.UpdateCommand.Parameters.Add("@HealthSafetyMeasures", SqlDbType.NVarChar, 50, "HealthSafetyMeasures");
                    adapter.UpdateCommand.Parameters.Add("@TraceabilityRawMaterials", SqlDbType.NVarChar, 50, "TraceabilityRawMaterials");
                    adapter.UpdateCommand.Parameters.Add("@EthicalCertifications", SqlDbType.NVarChar, 50, "EthicalCertifications");
                    adapter.UpdateCommand.Parameters.Add("@RelevantCertifications", SqlDbType.NVarChar, 50, "RelevantCertifications");
                    adapter.UpdateCommand.Parameters.Add("@ComplianceDocumentationStatus", SqlDbType.NVarChar, 50, "ComplianceDocumentationStatus");
                    adapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");

                    // Open connection and update the database
                    conn.Open();
                    adapter.Update(supplyData);
                    MessageBox.Show("Supply chain data updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating supply chain data: " + ex.Message);
                }
            }

            // Optionally, reload data to show updated data
            LoadSupplyData();
        }

        private void btnModifyCert_Click(object sender, EventArgs e)
        {

        }
    }
}
