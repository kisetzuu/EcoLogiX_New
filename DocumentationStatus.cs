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

namespace EcoLogiX_New
{
    public partial class DocumentationStatus : Form
    {
        public DocumentationStatus()
        {
            InitializeComponent();
            ConfigureDocumentationDataGridView();
        }
        private void ConfigureDocumentationDataGridView()
        {
            dataGridDoc.Columns.Clear();  // Clear existing columns if any

            // Adding a column for Product Name
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();
            productNameColumn.Name = "ProductName";
            productNameColumn.HeaderText = "Product Name";
            productNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridDoc.Columns.Add(productNameColumn);

            // Adding a column for Compliance Status
            DataGridViewTextBoxColumn complianceStatusColumn = new DataGridViewTextBoxColumn();
            complianceStatusColumn.Name = "ComplianceStatus";
            complianceStatusColumn.HeaderText = "Compliance Status";
            complianceStatusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;  // Adjust size to fill part of the grid
            dataGridDoc.Columns.Add(complianceStatusColumn);

            // Styling
            dataGridDoc.DefaultCellStyle.Font = new Font("Arial", 10);  // Set default font
            dataGridDoc.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  // Alternating row colors for better readability
            dataGridDoc.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);  // Header style
            dataGridDoc.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;  // Header background color
            dataGridDoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Header text color
            dataGridDoc.EnableHeadersVisualStyles = false;  // Apply custom style to headers

            // Grid behavior settings
            dataGridDoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridDoc.MultiSelect = false;  // Prevent multi-row selection

            FetchDocumentationData();  // Load data from database
        }

        public class DataGridViewProgressBarColumn : DataGridViewColumn
        {
            public DataGridViewProgressBarColumn()
            {
                this.CellTemplate = new DataGridViewProgressBarCell();
            }
        }

        public class DataGridViewProgressBarCell : DataGridViewCell
        {
            protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                // Paint the default cell background
                base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

                // Check if the value is a valid integer
                if (int.TryParse(value?.ToString(), out int progressVal))
                {
                    float percentage = ((float)progressVal / 100.0f);
                    // Paint the progress bar if the value is a valid integer
                    g.FillRectangle(new SolidBrush(Color.Blue), cellBounds.X + 2, cellBounds.Y + 2,
                        Convert.ToInt32((cellBounds.Width - 4) * percentage), cellBounds.Height - 4);
                }
                else
                {
                    // Optional: Paint some default or error indication if needed
                    g.DrawString(value?.ToString() ?? "N/A", cellStyle.Font, new SolidBrush(cellStyle.ForeColor), cellBounds, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }
        }
        private void FetchDocumentationData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SupplyChainDataDb"].ConnectionString;
            string query = "SELECT ProductName, ComplianceDocumentationStatus FROM SupplyChainData";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string productName = reader["ProductName"].ToString();
                        string complianceStatus = reader["ComplianceDocumentationStatus"].ToString();
                        dataGridDoc.Rows.Add(productName, complianceStatus);  // Add both product name and compliance status
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load documentation status: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void DocumentationStatus_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            DocumentationStatus documentationStatus = new DocumentationStatus();
            documentationStatus.Show();
            this.Hide();
        }
    }
}
