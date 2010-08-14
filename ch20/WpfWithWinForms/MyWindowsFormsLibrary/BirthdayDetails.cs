using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyWindowsFormsLibrary
{
    public partial class BirthdayDetails : Form
    {
        public BirthdayDetails()
        {
            InitializeComponent();
        }

        public void SetDetails(string details)
        {
            this.label2.Text = details;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
