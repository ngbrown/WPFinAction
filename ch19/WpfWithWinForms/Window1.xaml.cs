using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWithWinForms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            DateTime born = birthday.Value;

            MyWindowsFormsLibrary.BirthdayDetails dlg = new MyWindowsFormsLibrary.BirthdayDetails();
            dlg.SetDetails(name + " was born on " + born.ToLongDateString());

            dlg.Show();
        }
    }
}
