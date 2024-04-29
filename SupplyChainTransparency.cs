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

        public string SupplierName { get; private set; }
        public string ContactPerson { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public string LocationInformation { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public string ProductCategory { get; private set; }
        public string TotalEnergyConsumption { get; set; }
        public string TotalGreenhouseEmission { get; set; }
        public string HealthSafetyMeasures { get; set; }
        public string EthicalCertifications { get; set; }
        public string RelevantCertifications { get; set; }


        private void btnNext_Click(object sender, EventArgs e)
        {
            // Store data temporarily
            SupplierName = txtName.Text;
            ContactPerson = txtContact.Text;
            EmailAddress = txtEmail.Text;
            PhoneNumber = txtPhone.Text;
            LocationInformation = comboLoc.Text;
            ProductName = txtProd.Text;
            ProductDescription = txtDesc.Text;
            ProductCategory = comboCat.Text;

            // Navigate to Form 2
            SupplyChainPageTwo form2 = new SupplyChainPageTwo();
            form2.TotalEnergyConsumption = TotalEnergyConsumption; // Assign TotalEnergyConsumption property from Form 1 to Form 2
            form2.TotalGreenhouseEmission = TotalGreenhouseEmission; // Assign TotalGreenhouseEmission property from Form 1 to Form 2
            form2.HealthSafetyMeasures = HealthSafetyMeasures; // Assign HealthSafetyMeasures property from Form 1 to Form 2
            form2.EthicalCertifications = EthicalCertifications; // Assign EthicalCertifications property from Form 1 to Form 2
            form2.RelevantCertifications = RelevantCertifications; // Assign RelevantCertifications property from Form 1 to Form 2

            form2.Show();
            this.Hide(); // Hide Form 1 if needed
        }

        private void SupplyChainTransparency_Load(object sender, EventArgs e)
        {

        }
    }
}
