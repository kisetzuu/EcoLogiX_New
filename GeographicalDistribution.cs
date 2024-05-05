using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using System.Configuration;

namespace EcoLogiX_New
{
    public partial class GeographicalDistribution : Form
    {
        public GeographicalDistribution()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            gmapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmapControl.Position = new GMap.NET.PointLatLng(14.5995, 120.9842); // Central location in Manila
            gmapControl.MinZoom = 5;
            gmapControl.MaxZoom = 15;
            gmapControl.Zoom = 10;
            gmapControl.ShowCenter = false;

            PlotCities(); // If you're ready to plot cities at initialization
        }

        private void PlotCities()
        {
            var markers = new GMapOverlay("markers");
            var fetchedCityNames = FetchCityNames();
            var cityCoordinates = new Dictionary<string, Tuple<double, double>>()
            {
                {"Metro Manila", Tuple.Create(14.6091, 121.0223)},
                {"Quezon City", Tuple.Create(14.6760, 121.0437)},
                {"Manila", Tuple.Create(14.5995, 120.9842)},
                {"Caloocan", Tuple.Create(14.7566, 120.9919)},
                {"Davao City", Tuple.Create(7.1907, 125.4553)},
                {"Cebu City", Tuple.Create(10.3157, 123.8854)},
                {"Zamboanga City", Tuple.Create(6.9214, 122.0790)},
                {"Taguig", Tuple.Create(14.5176, 121.0509)},
                {"Pasig", Tuple.Create(14.5764, 121.0851)},
                {"Antipolo", Tuple.Create(14.6255, 121.1245)},
                {"Cagayan de Oro", Tuple.Create(8.4542, 124.6319)},
                {"Bacolod", Tuple.Create(10.6760, 122.9509)},
                {"Pasay", Tuple.Create(14.5378, 121.0014)},
                {"Cainta", Tuple.Create(14.5786, 121.1222)},
                {"Parañaque", Tuple.Create(14.4793, 121.0198)},
                {"Mandaluyong", Tuple.Create(14.5794, 121.0359)},
                {"General Santos", Tuple.Create(6.1164, 125.1716)},
                {"Makati", Tuple.Create(14.5547, 121.0244)},
                {"Iligan", Tuple.Create(8.2280, 124.2452)},
                {"Marikina", Tuple.Create(14.6507, 121.1029)}
            };

            foreach (var cityName in fetchedCityNames)
            {
                if (cityCoordinates.ContainsKey(cityName))
                {
                    var coords = cityCoordinates[cityName];
                    var marker = new GMarkerGoogle(
                        new PointLatLng(coords.Item1, coords.Item2),
                        GMarkerGoogleType.red_dot);
                    marker.ToolTipText = cityName; // Show city name when hovering over the marker
                    markers.Markers.Add(marker);
                }
            }

            gmapControl.Overlays.Add(markers);
        }

        private void GeographicalDistribution_Load(object sender, EventArgs e)
        {
            InitializeMap(); // Call the same initialization method

        }

        public List<string> FetchCityNames()
        {
            List<string> cityNames = new List<string>();
            // Retrieve connection string from configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;

            string query = "SELECT LocationInformation FROM dbo.SupplyChainData";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cityNames.Add(reader["LocationInformation"].ToString());
                        }
                    } // Reader is automatically closed here
                } // Connection is automatically closed here
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load city names: " + ex.Message);
            }

            return cityNames;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            SupplierDetails supplierDetails = new SupplierDetails();
            supplierDetails.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GeographicalDistribution geographicalDistribution = new GeographicalDistribution();
            geographicalDistribution.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ProductInformation productInformation = new ProductInformation();
            productInformation.Show();
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
    }
}
