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
    }
}
