﻿using System;
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
    public partial class LoggedIn : Form
    {
        public LoggedIn()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GreenCertification greenForm = new GreenCertification();
            greenForm.Show();
            this.Hide();
        }
    }
}
