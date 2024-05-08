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
    public partial class ContentLibrary : Form
    {
        public ContentLibrary()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bcg.com/publications/2021/steps-to-a-sustainability-transformation");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://sievo.com/blog/carbon-analytics");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnTech_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.mckinsey.com/capabilities/mckinsey-digital/our-insights/cloud-powered-technologies-for-sustainability");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnIntegration_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.myjoyonline.com/the-crucial-role-of-data-analytics-in-sustainable-business-transformation/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnHarvard_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://online.hbs.edu/blog/post/business-case-for-sustainability");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnBerkeley_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://cases.haas.berkeley.edu/cases/esg/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnPurpose_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://repurpose.global/blog/post/3-sustainability-initiatives-and-why-they-worked");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnPWC_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.pwc.com/gx/en/services/sustainability/publications/casestudy.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.bsr.org");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnSteps_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.bcg.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
            }
        }

        private void btnBuilding_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.gartner.com/en");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link: " + ex.Message);
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
