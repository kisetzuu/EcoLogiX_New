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
    public partial class LoggedInTwo : Form
    {
        public LoggedInTwo()
        {
            InitializeComponent();
        }

        private void LoggedInTwo_Load(object sender, EventArgs e)
        {

        }

        private void btnSustainability_Click(object sender, EventArgs e)
        {
            SustainabilityActionPlans plan = new SustainabilityActionPlans();
            plan.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            SustainabilityReporting report = new SustainabilityReporting();
            report.Show();
            this.Hide();
        }
    }
}
