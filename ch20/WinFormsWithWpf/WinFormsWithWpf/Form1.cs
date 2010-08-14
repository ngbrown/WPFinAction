using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Interop;

namespace WinFormsWithWpf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyWPFControlLibrary.CalculatorWindow calc = new MyWPFControlLibrary.CalculatorWindow();
            WindowInteropHelper helper = new WindowInteropHelper(calc);
            helper.Owner = this.Handle;
            calc.Show();
        }
    }
}
