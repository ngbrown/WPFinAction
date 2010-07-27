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
using System.Windows.Shapes;

namespace Desktop_Wiki
{
    /// <summary>
    /// Interaction logic for WikiWindow.xaml
    /// </summary>
    public partial class WikiWindow : Window
    {
        public static RoutedCommand About = new RoutedCommand();

        public WikiWindow()
        {
            InitializeComponent();
        }

        private void AboutExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Welcome to WikiInAction");
        }
    }
}
