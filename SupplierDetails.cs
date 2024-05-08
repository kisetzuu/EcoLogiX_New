using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace EcoLogiX_New
{
    public partial class SupplierDetails : Form
    {
        public SupplierDetails()
        {
            InitializeComponent();
        }

        private void LoadSupplierData()
        {
            // Connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;

            // SQL query to fetch supplier data
            string query = "SELECT SupplierName, ContactPerson, EmailAddress, PhoneNumber FROM dbo.SupplyChainData;";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create a SqlCommand to execute the SQL query
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Execute the query and load the results into the DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dataTable);
                        }
                    }

                    // Set the DataGridView source to the DataTable
                    dataGridSupplier.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void dataGridSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            LoadSupplierData();
            SupplierDetails supplierDetails = new SupplierDetails();
            supplierDetails.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Assuming the original DataTable is stored in a class-level variable called 'dataTable'
            // If not, you should retrieve or store the DataTable used to populate dataGridSupplier initially.

            string searchValue = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchValue))
            {
                // Perform the search and filter on all relevant columns
                // Adjust column names as needed
                (dataGridSupplier.DataSource as DataTable).DefaultView.RowFilter = string.Format(
                    "SupplierName LIKE '%{0}%' OR " +
                    "ContactPerson LIKE '%{0}%' OR " +
                    "EmailAddress LIKE '%{0}%' OR " +
                    "PhoneNumber LIKE '%{0}%'",
                    searchValue);
            }
            else
            {
                // Reset the filter if the search box is empty
                (dataGridSupplier.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
            }
        }

        private void SupplierDetails_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            GeographicalDistribution geographicalDistribution = new GeographicalDistribution();
            geographicalDistribution.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProductInformation productInformation = new ProductInformation();
            productInformation.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

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
    }
}
