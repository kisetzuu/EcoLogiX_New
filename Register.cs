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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Get the connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;

            // Define the SQL query to insert the user data
            string query = @"
            INSERT INTO dbo.Users (Name, Email, Password, CompanyName, Role, Contact) 
            VALUES (@Name, @Email, @Password, @CompanyName, @Role, @Contact)";

            // Create a new SqlConnection within a using block for automatic disposal
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Create a new SqlCommand using the query and connection
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Replace the parameters with the values from the textboxes
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text); // Make sure to hash the password
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                    cmd.Parameters.AddWithValue("@Role", txtRole.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);

                    // Open the connection
                    conn.Open();

                    // Execute the query and get the number of affected rows
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if the insert was successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("The data has been saved successfully.");
                    }
                    else
                    {
                        MessageBox.Show("The data was not saved. Please try again.");
                    }

                    // The connection will be closed automatically when exiting the using block
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
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

        }
    }
}
