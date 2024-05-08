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
    public partial class FAQsPtwo : Form
    {
        public FAQsPtwo()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FAQsPthree fAQsPthree = new FAQsPthree();
            fAQsPthree.Show();
            this.Hide();
        }
    }
}
