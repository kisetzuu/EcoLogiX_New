using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EcoLogiX_New
{
    public partial class TraceabilityAndEthics : Form
    {
        public TraceabilityAndEthics()
        {
            InitializeComponent();
            ConfigureTraceabilityChart();
            ConfigureDataGridTraceability();
        }

        private void ConfigureTraceabilityChart()
        {
            chartTraceability.Series.Clear();
            chartTraceability.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea();
            chartTraceability.ChartAreas.Add(chartArea);

            Series series = new Series("Traceability")
            {
                ChartType = SeriesChartType.Pie
            };
            chartTraceability.Series.Add(series);

            FetchTraceabilityChartData();
        }

        private void FetchTraceabilityChartData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT TraceabilityRawMaterials, COUNT(*) AS Count FROM SupplyChainData GROUP BY TraceabilityRawMaterials";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string materialSource = reader["TraceabilityRawMaterials"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);
                        chartTraceability.Series["Traceability"].Points.AddXY(materialSource, count);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load traceability chart data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConfigureDataGridTraceability()
        {
            // Assuming columns are added via the designer or here in code
            dataGridTraceability.Columns.Clear();
            dataGridTraceability.Columns.Add("Supplier", "Supplier");
            dataGridTraceability.Columns.Add("EthicalCertifications", "Ethical Certifications");
            dataGridTraceability.Columns.Add("RelevantCertifications", "Relevant Certifications");

            FetchDataGridTraceabilityData();
        }

        private void FetchDataGridTraceabilityData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT SupplierName, EthicalCertifications, RelevantCertifications FROM SupplyChainData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string supplierName = reader["SupplierName"].ToString();
                        string ethical = reader["EthicalCertifications"].ToString();
                        string relevant = reader["RelevantCertifications"].ToString();

                        int rowIndex = dataGridTraceability.Rows.Add();
                        dataGridTraceability.Rows[rowIndex].Cells["Supplier"].Value = supplierName;
                        dataGridTraceability.Rows[rowIndex].Cells["EthicalCertifications"].Value = ethical;  // Modify this to use images if needed
                        dataGridTraceability.Rows[rowIndex].Cells["RelevantCertifications"].Value = relevant;  // Modify this to use images if needed
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data grid traceability data: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void TraceabilityAndEthics_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            TraceabilityAndEthics traceabilityAndEthics = new TraceabilityAndEthics();
            traceabilityAndEthics.Show();
            this.Hide();
        }
    }
}
