using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace EcoLogiX_New
{
    public partial class ViewSupplyChain : Form
    {
        public ViewSupplyChain()
        {
            InitializeComponent();
        }

        public static class UserSession
        {
            public static int UserID { get; set; }
            // Other session-related properties can be added here
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Retrieve the user's ID from the session
            int userId = UserSession.UserID;

            // Retrieve the connection string from App.config or Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define SQL query to select data from the database for the logged-in user
                string query = "SELECT * FROM SupplyChainData WHERE UserID = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for UserID
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Open the connection
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Create a DataTable to store the retrieved data
                        DataTable dataTable = new DataTable();

                        // Load the data from the reader into the DataTable
                        dataTable.Load(reader);

                        // Bind the DataTable to the dataGridView
                        dataViewSupply.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("No data found for the logged-in user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ViewSupplyChain_Load(object sender, EventArgs e)
        {

        }
    }
}
