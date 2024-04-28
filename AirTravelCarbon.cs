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
    public partial class AirTravelCarbon : Form
    {
        public AirTravelCarbon()
        {
            InitializeComponent();
        }

        private double CalculateAirTravelEmissions(string flightType, string classOfService, int numberOfFlights)
        {
            double emissionFactor = GetEmissionFactor(flightType);
            double serviceMultiplier = GetServiceMultiplier(classOfService);
            return numberOfFlights * emissionFactor * serviceMultiplier;
        }

        private double GetEmissionFactor(string flightType)
        {
            // Hypothetical average flight distances and emissions per km
            switch (flightType)
            {
                case "Short-haul":
                    return 500 * 0.15; // 500 km at 0.15 kg CO2 per km
                case "Medium-haul":
                    return 2000 * 0.15; // 2000 km at 0.15 kg CO2 per km
                case "Long-haul":
                    return 5000 * 0.15; // 5000 km at 0.15 kg CO2 per km
                default:
                    return 0;
            }
        }

        private double GetServiceMultiplier(string classOfService)
        {
            // Multipliers based on the space and service level in different classes
            switch (classOfService)
            {
                case "Economy":
                    return 1.0;
                case "Business":
                    return 1.5; // 50% more emissions due to increased space and weight
                case "First Class":
                    return 2.0; // Double emissions due to maximum space and weight
                default:
                    return 1.0;
            }
        }

        private void AirTravelCarbon_Load(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            string flightType = comboType.SelectedItem.ToString();
            string classOfService = comboClass.SelectedItem.ToString();
            int numberOfFlights;

            if (!int.TryParse(txtFlights.Text, out numberOfFlights) || numberOfFlights <= 0)
            {
                MessageBox.Show("Please enter a valid number of flights.");
                return;
            }

            double totalEmissions = CalculateAirTravelEmissions(flightType, classOfService, numberOfFlights);
            MessageBox.Show($"Estimated Carbon Emissions from Air Travel: {totalEmissions} kg CO2", "Carbon Offset Calculator");
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
