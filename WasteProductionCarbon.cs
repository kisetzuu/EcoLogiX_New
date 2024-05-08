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
    public partial class WasteProductionCarbon : Form
    {
        public WasteProductionCarbon()
        {
            InitializeComponent();
        }

        private double CalculateWasteEmissions(double nonRecycledWaste, string disposalMethod)
        {
            double emissionFactor = GetEmissionFactorForWaste(disposalMethod);
            return nonRecycledWaste * emissionFactor;
        }

        private double GetEmissionFactorForWaste(string disposalMethod)
        {
            // Hypothetical emission factors for different waste disposal methods in kg CO2 per kg of waste
            switch (disposalMethod)
            {
                case "Landfill":
                    return 0.41;  // Example factor
                case "Incineration":
                    return 1.2;   // Incineration typically generates more emissions
                case "Composting":
                    return 0.16;  // Generally lower, especially for organic waste
                default:
                    return 0;     // In case of an unexpected input
            }
        }

        private void WasteProductionCarbon_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtRecycled.Text, out double recycledWaste) || recycledWaste < 0)
            {
                MessageBox.Show("Please enter a valid amount of recycled waste.");
                return;
            }

            if (!double.TryParse(txtNon.Text, out double nonRecycledWaste) || nonRecycledWaste < 0)
            {
                MessageBox.Show("Please enter a valid amount of non-recycled waste.");
                return;
            }

            string disposalMethod = comboWaste.SelectedItem.ToString();

            double totalEmissions = CalculateWasteEmissions(nonRecycledWaste, disposalMethod);
            MessageBox.Show($"Estimated Carbon Emissions from Waste Production: {totalEmissions} kg CO2", "Carbon Offset Calculator");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CarbonOffset carbonForm = new CarbonOffset();
            carbonForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HomeEnergyCarbon homeEnergyForm = new HomeEnergyCarbon();
            homeEnergyForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AirTravelCarbon airTravelCarbon = new AirTravelCarbon();
            airTravelCarbon.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            WasteProductionCarbon wasteProductionCarbon = new WasteProductionCarbon();
            wasteProductionCarbon.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            WaterUsageCarbon waterUsageCarbon = new WaterUsageCarbon();
            waterUsageCarbon.Show();
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            LoggedIn loggedIn = new LoggedIn();
            loggedIn.Show();
            this.Hide();
        }
    }
}
