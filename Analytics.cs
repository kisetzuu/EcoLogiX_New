using System;
using System.Windows.Forms;
using LiveCharts; // Core LiveCharts functionalities
using LiveCharts.WinForms; // For Windows Forms integration
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using LiveCharts.Wpf;
using System.Drawing;


namespace EcoLogiX_New
{
    public partial class Analytics : Form
    {
        private HttpClient client = new HttpClient();
        private LiveCharts.WinForms.CartesianChart carbonChart;
        public Analytics()
        {
            InitializeComponent();
            InitializeChart();
            this.Load += Analytics_Load;
            AddDescriptionLabel(); // Adding the description label
        }

        private void InitializeChart()
        {
            carbonChart = new LiveCharts.WinForms.CartesianChart
            {
                Dock = DockStyle.Fill,
                Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "Carbon Intensity",
                Values = new ChartValues<double>(),
                Stroke = System.Windows.Media.Brushes.Green,
                Fill = System.Windows.Media.Brushes.Transparent
            }
        },
                AxisX = new AxesCollection
        {
            new Axis
            {
                Title = "Time",
                Labels = new List<string>(),
                Separator = new Separator { Step = 1, IsEnabled = false }
            }
        },
                AxisY = new AxesCollection
        {
            new Axis
            {
                Title = "Intensity (gCO2eq/kWh)"
            }
        },
                LegendLocation = LegendLocation.Top
            };

            Label chartTitle = new Label
            {
                Text = "Live Carbon Intensity in the Philippines",
                Dock = DockStyle.Top,
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30
            };

            this.Controls.Add(carbonChart);
            this.Controls.Add(chartTitle);
        }

        private void AddDescriptionLabel()
        {
            Label descriptionLabel = new Label
            {
                Text = "Carbon Intensity (gCO2eq/kWh) represents the amount of CO2 emitted per kilowatt-hour of electricity consumed.",
                Dock = DockStyle.Bottom, // Positioning at the bottom of the form
                AutoSize = true,  // Enable auto-sizing to fit the text
                Font = new Font("Arial", 10), // Set font here if needed
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10) // Add some padding for aesthetics
            };

            this.Controls.Add(descriptionLabel);
        }

        private async Task UpdateCarbonIntensityAsync()
        {
            while (true)
            {
                var carbonIntensity = await FetchCarbonIntensityAsync("PH"); // Fetch the latest data for the Philippines
                if (carbonIntensity != null)
                {
                    carbonChart.Series[0].Values.Add(carbonIntensity);
                    carbonChart.AxisX[0].Labels.Add(DateTime.Now.ToString("HH:mm:ss"));

                    if (carbonChart.Series[0].Values.Count > 30) // Keep last 30 data points
                    {
                        carbonChart.Series[0].Values.RemoveAt(0);
                        carbonChart.AxisX[0].Labels.RemoveAt(0);
                    }
                }

                await Task.Delay(60000); // Update every minute
            }
        }

        private async Task<double?> FetchCarbonIntensityAsync(string zoneKey)
        {
            try
            {
                string url = $"https://api.electricitymap.org/v3/carbon-intensity/latest?zone={zoneKey}";
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("auth-token", "fep67zBhHDnpb");

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    return data.carbonIntensity;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching carbon intensity: {ex.Message}");
            }
            return null;
        }
        private async Task<string> FetchZonesDataAsync()
        {
            string apiUrl = "https://api.electricitymap.org/v3/zones";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("auth-token", "fep67zBhHDnpb"); // Use the actual API token

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                // Logging or handling non-successful response
                string responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"API request failed with status code: {response.StatusCode}, Response content: {responseContent}");
            }
        }
        private void ProcessZonesData(string jsonData)
        {
            var zones = JsonConvert.DeserializeObject<List<Zone>>(jsonData);
            // Here you could update a UI element or log the data
            foreach (var zone in zones)
            {
                Console.WriteLine($"Zone ID: {zone.ZoneId}, Latitude: {zone.Latitude}, Longitude: {zone.Longitude}");
            }
        }

        private async void Analytics_Load(object sender, EventArgs e)
        {
            await UpdateCarbonIntensityAsync();
        }

        private void dataZones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    public class Zone
    {
        [JsonProperty("zone")]
        public string ZoneId { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
