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
    public partial class LoggedIn : Form
    {
        public LoggedIn()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GreenCertification greenForm = new GreenCertification();
            greenForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CarbonOffset carbonForm = new CarbonOffset();
            carbonForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SupplyChainTransparency supplyChainTransparency = new SupplyChainTransparency();
            supplyChainTransparency.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Engagement engagementForm = new Engagement();
            engagementForm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Analytics analyticsForm = new Analytics();
            analyticsForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu menuForm = new Menu();
            menuForm.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            LoggedInTwo loggedInTwo = new LoggedInTwo();
            loggedInTwo.Show();
            this.Hide();
        }
    }
}
