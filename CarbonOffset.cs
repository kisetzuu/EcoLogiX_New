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
    public partial class CarbonOffset : Form
    {
        public CarbonOffset()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private Dictionary<string, double> emissionFactors = new Dictionary<string, double>()
        {
            {"Car-Gasoline (Petrol)", 0.24},
            {"Car-Diesel", 0.26},
            {"Motorcycle-Gasoline (Petrol)", 0.21},
            {"Motorcycle-Diesel", 0.22},
            {"SUV-Gasoline (Petrol)", 0.28},
            {"SUV-Diesel", 0.30},
            {"Truck-Diesel", 0.32},
            {"Bus-Diesel", 0.38},
            {"Bicycle-Electric", 0.00},
            {"Electric Scooter-Electric", 0.01},
            {"Hybrid Vehicle-Hybrid/Electric-Gasoline", 0.18},
            {"Electric Vehicle (EV)-Electric", 0.12},
            {"Van-Gasoline (Petrol)", 0.27},
            {"Van-Diesel", 0.29},
            {"Car-CNG (Compressed Natural Gas)", 0.20},
            {"Car-LPG (Liquefied Petroleum Gas)", 0.22},
            {"Car-Ethanol (E85)", 0.18},
            {"Car-Biodiesel", 0.19},
            {"Car-Hydrogen", 0.00},
            {"Car-Biofuel", 0.21}
        };

        private double CalculateTransportationEmissions(string vehicleType, string fuelType, double mileage, string frequency)
        {
            double emissionFactor = GetEmissionFactor(vehicleType, fuelType);
            double frequencyMultiplier = GetFrequencyMultiplier(frequency);
            double emissions = mileage * emissionFactor * frequencyMultiplier;
            return emissions;
        }

        private double GetEmissionFactor(string vehicleType, string fuelType)
        {
            string key = $"{vehicleType}-{fuelType}";
            if (emissionFactors.TryGetValue(key, out double factor))
            {
                return factor;
            }
            return 0; // Return 0 if no matching key found, or handle it differently as needed
        }

        private double GetFrequencyMultiplier(string frequency)
        {
            switch (frequency)
            {
                case "Daily": return 365;
                case "Weekly": return 52;
                case "Monthly": return 12;
                case "Yearly": return 1;
                case "Occasionally": return 0.5; // Assuming twice a year
                default: return 0; // Handle unexpected cases
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string vehicleType = comboVehicle.SelectedItem.ToString();
            string fuelType = comboFuel.SelectedItem.ToString();
            string frequency = comboFrequency.SelectedItem.ToString();
            double mileage;

            // Try to parse the mileage input safely
            if (!double.TryParse(txtMileage.Text, out mileage))
            {
                MessageBox.Show("Please enter a valid mileage.");
                return;
            }

            // Calculate emissions
            double emissions = CalculateTransportationEmissions(vehicleType, fuelType, mileage, frequency);

            // Display the result
            MessageBox.Show($"Estimated Carbon Emissions: {emissions} kg CO2 per year");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
