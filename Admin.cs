using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLogiX_New
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Check if the password is 'admin'
            if (txtPassword.Text == "admin")
            {
                // Proceed to the AdminFunctions form
                Form adminFunctions = new AdminFunctions(); // Assuming 'AdminFunctions' is the name of the next form
                adminFunctions.Show();
                this.Hide(); // Optionally hide the current form
            }
            else
            {
                // Show an error message if the password is not correct
                MessageBox.Show("Invalid password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
