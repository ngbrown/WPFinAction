using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GraphingWithShapes
{
    /// <summary>
    /// Interaction logic for GraphHolder.xaml
    /// </summary>
    public partial class GraphHolder : UserControl
    {
        ObservableCollection<NameValuePair> dataPoints = new ObservableCollection<NameValuePair>();

        public GraphHolder()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding();
            binding.Source = dataPoints;
            valueList.SetBinding(ListBox.ItemsSourceProperty, binding);

            graphCtrl.SetData(dataPoints);
        }

        private void addValueBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = addValueNameTextBox.Text.Trim();
            string valueAsString = addValueValueTextBox.Text.Trim();
            double valueAsDouble = Convert.ToDouble(valueAsString);

            NameValuePair nvp = new NameValuePair(name, valueAsDouble);
            dataPoints.Add(nvp);

            addValueNameTextBox.Text = "";
            addValueValueTextBox.Text = "";
        }

        private void valueList_KeyDown(object sender, KeyEventArgs e)
        {
            // TODO: Remove and edit the items.
        }
    }
}
