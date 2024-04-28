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
    }
}
