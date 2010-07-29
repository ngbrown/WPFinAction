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
using System.Diagnostics;

namespace ProcessMonitor
{
    /// <summary>
    /// Interaction logic for Monitor.xaml
    /// </summary>
    public partial class Monitor : Window
    {
        public Monitor()
        {
            InitializeComponent();
            BindProcessesToListView();
        }

        private void BindProcessesToListView()
        {
            ObjectDataProvider provider = new ObjectDataProvider();
            provider.ObjectType = typeof(Process);
            provider.MethodName = "GetProcesses";

            Binding binding = new Binding();
            binding.Source = provider;
            binding.Mode = BindingMode.OneWay;

            PresentationTraceSources.SetTraceLevel(binding, PresentationTraceLevel.High);

            listView1.SetBinding(ListView.ItemsSourceProperty, binding);
        }
    }
}
