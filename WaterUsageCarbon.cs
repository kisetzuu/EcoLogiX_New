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
    public partial class WaterUsageCarbon : Form
    {
        public WaterUsageCarbon()
        {
            InitializeComponent();
        }

        private double CalculateWaterUsageEmissions(double waterConsumption, string waterSource, string waterPurpose)
        {
            double emissionFactor = GetEmissionFactor(waterSource, waterPurpose);
            return waterConsumption * emissionFactor;
        }

        private double GetEmissionFactor(string waterSource, string waterPurpose)
        {
            // Emission factors might depend on the source and purpose. Here are hypothetical values:
            double baseFactor = 0.0; // Base emission factor per liter or gallon

            if (waterPurpose == "Heating")
            {
                baseFactor += 0.02;  // Adding additional factor for heating water
            }

            switch (waterSource)
            {
                case "Municipal":
                    baseFactor += 0.01;  // Base emissions for municipal water treatment and supply
                    break;
                case "Well":
                    baseFactor += 0.005; // Lower emissions, mainly from pumping
                    break;
                case "Rainwater":
                    baseFactor += 0.001; // Minimal emissions, mostly from collection
                    break;
                case "Bottled":
                    baseFactor += 0.03;  // High emissions due to bottling and transportation
                    break;
            }

            return baseFactor;
        }

        private void WaterUsageCarbon_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtConsumption.Text, out double waterConsumption) || waterConsumption < 0)
            {
                MessageBox.Show("Please enter a valid amount of water consumption.");
                return;
            }

            string waterSource = comboSource.SelectedItem.ToString();
            string waterPurpose = comboPurpose.SelectedItem.ToString();

            double totalEmissions = CalculateWaterUsageEmissions(waterConsumption, waterSource, waterPurpose);
            MessageBox.Show($"Estimated Carbon Emissions from Water Usage: {totalEmissions} kg CO2", "Carbon Offset Calculator");
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
    }
}
