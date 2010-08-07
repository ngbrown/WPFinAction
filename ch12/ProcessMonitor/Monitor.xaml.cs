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
using System.ComponentModel;

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
        }

        private void sortOrderCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetNewSortOrder();
        }

        private void SetNewSortOrder()
        {
            string newSortOrder = ((ComboBoxItem)sortOrderCombo.SelectedItem).Content.ToString();
            SortDescription sortDesc = new SortDescription(newSortOrder, ListSortDirection.Ascending);

            CollectionViewSource src = (CollectionViewSource)FindResource("processesView");
            src.SortDescriptions.Clear();
            src.SortDescriptions.Add(sortDesc);
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Process p = e.Item as Process;

            int mode = (priorityFilterCombo != null) ? priorityFilterCombo.SelectedIndex : 0;

            switch (mode)
            {
                case 1:
                    e.Accepted = (p.BasePriority > 12);
                    break;
                case 2:
                    e.Accepted = (p.BasePriority >= 8 && p.BasePriority <= 12);
                    break;
                case 3:
                    e.Accepted = (p.BasePriority < 8);
                    break;
                default:
                    e.Accepted = true;
                    break;
            }
        }

        private void priorityFilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetNewSortOrder();
        }
    }
}
