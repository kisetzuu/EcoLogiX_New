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
    public partial class SupplyChainTransparency : Form
    {
        public SupplyChainTransparency()
        {
            InitializeComponent();
        }

        public static class UserSession
        {
            public static int UserID { get; set; }
            // Additional session-related properties can be added here if needed
        }

        // Properties to hold data from form fields
        public string SupplierName { get; private set; }
        public string ContactPerson { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public string LocationInformation { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public string ProductCategory { get; private set; }


        private void btnNext_Click(object sender, EventArgs e)
        {
            string supplierName = txtName.Text;
            string contactPerson = txtContact.Text;
            string emailAddress = txtEmail.Text;
            string phoneNumber = txtPhone.Text;
            string locationInformation = comboLoc.Text;
            string productName = txtProd.Text;
            string productDescription = txtDesc.Text;
            string productCategory = comboCat.Text;
            int userId = UserSession.UserID;

            // Create an instance of SupplyChainPageTwo and pass the data
            SupplyChainPageTwo form2 = new SupplyChainPageTwo(userId, txtName.Text, txtContact.Text, txtEmail.Text, txtPhone.Text, comboLoc.Text, txtProd.Text, txtDesc.Text, comboCat.Text);
            form2.Show();
            this.Hide(); // Hide the current form
        }

        private void SupplyChainTransparency_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewSupplyChain viewSupplyChain = new ViewSupplyChain();
            viewSupplyChain.Show();
            this.Hide();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SupplierDetails supplierDetails = new SupplierDetails();
            supplierDetails.Show();
            this.Hide();
        }

        private void btnGeographical_Click(object sender, EventArgs e)
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

        private void button10_Click(object sender, EventArgs e)
        {
            EnvironmentalImpact environmentalImpact = new EnvironmentalImpact();
            environmentalImpact.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ComplianceAndSafety complianceAndSafety = new ComplianceAndSafety();
            complianceAndSafety.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            TraceabilityAndEthics traceabilityAndEthics = new TraceabilityAndEthics();
            traceabilityAndEthics.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DocumentationStatus documentationStatus = new DocumentationStatus();
            documentationStatus.Show();
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
    }
}
