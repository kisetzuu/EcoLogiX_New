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
            // Adjusted example distances and emissions per km for realism
            switch (flightType)
            {
                case "Short-haul":
                    return 500 * 0.2; // Example: Short-haul flights are less fuel-efficient per km
                case "Medium-haul":
                    return 2000 * 0.16; // Example: Medium efficiency
                case "Long-haul":
                    return 5000 * 0.14; // Example: Long-haul flights are more fuel-efficient per km
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
            if (comboType.SelectedItem == null || comboClass.SelectedItem == null)
            {
                MessageBox.Show("Please select both flight type and class of service.");
                return;
            }

            string flightType = comboType.SelectedItem.ToString();
            string classOfService = comboClass.SelectedItem.ToString();
            int numberOfFlights;

            if (!int.TryParse(txtFlights.Text, out numberOfFlights) || numberOfFlights <= 0)
            {
                MessageBox.Show("Please enter a valid number of flights.");
                return;
            }

            double totalEmissions = CalculateAirTravelEmissions(flightType, classOfService, numberOfFlights);
            MessageBox.Show($"Estimated Carbon Emissions from Air Travel: {totalEmissions:N2} kg CO2", "Carbon Offset Calculator");
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

        private void comboClass_SelectedIndexChanged(object sender, EventArgs e)
        {

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
    }
}
