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

namespace Docking
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

        protected void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Button buttonRight = new Button { Content = "Right" };
            DockPanel.SetDock(buttonRight, Dock.Right);
            dockPanel1.Children.Insert(0, buttonRight);
        }
    }
}
