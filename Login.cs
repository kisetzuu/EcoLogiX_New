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
using static EcoLogiX_New.GreenCertification;

namespace EcoLogiX_New
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
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

        private byte[] HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            int userId = AuthenticateUser(email, password);
            if (userId > 0)  // Check if authentication was successful
            {
                UserSession.UserID = userId; // Store the user ID in the session
                MessageBox.Show("Login successful!");

                LoggedIn loggedInForm = new LoggedIn();
                loggedInForm.Show();
                this.Hide();  // Optionally hide the login form, or you might choose to close it
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private int AuthenticateUser(string email, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            int userId = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ID, Password FROM Users WHERE Email = @Email", conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && (string)reader["Password"] == password)
                        {
                            userId = reader.GetInt32(reader.GetOrdinal("ID")); // Fetch the user ID from the ID column
                        }
                    }
                }
            }
            return userId;  // Return the user ID, or -1 if authentication fails
        }
    }
}
