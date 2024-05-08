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
    public partial class HomeEnergyCarbon : Form
    {
        public HomeEnergyCarbon()
        {
            InitializeComponent();
        }

        private double CalculateAnnualEmissions(string energyType, double energyConsumed, string period)
        {
            double emissionFactor = GetEmissionFactor(energyType);
            double periodMultiplier = GetPeriodMultiplier(period);
            return energyConsumed * emissionFactor * periodMultiplier;
        }

        private double GetEmissionFactor(string energyType)
        {
            switch (energyType)
            {
                case "Electricity":
                    return 0.4;  // kg CO2 per kWh
                case "Natural Gas":
                    return 0.2;  // kg CO2 per cubic meter
                case "Heating Oil":
                    return 2.5;  // kg CO2 per liter
                case "Propane":
                    return 1.5;  // kg CO2 per liter
                case "Wood":
                    return 0.025;  // kg CO2 per kg
                default:
                    return 0;
            }
        }

        private double GetPeriodMultiplier(string period)
        {
            switch (period)
            {
                case "Monthly":
                    return 12;  // Monthly consumption to annual
                case "Quarterly":
                    return 4;   // Quarterly consumption to annual
                case "Annually":
                    return 1;   // Already annual
                default:
                    return 0;   // Error case or incorrect input
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string energyType = comboType.SelectedItem.ToString();
            double energyConsumed;
            string period = comboConsumption.SelectedItem.ToString();

            if (!double.TryParse(txtEnergy.Text, out energyConsumed))
            {
                MessageBox.Show("Please enter valid energy consumption.");
                return;
            }

            double annualEmissions = CalculateAnnualEmissions(energyType, energyConsumed, period);
            MessageBox.Show($"Estimated Annual Carbon Emissions: {annualEmissions} kg CO2", "Carbon Offset Calculator");
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
