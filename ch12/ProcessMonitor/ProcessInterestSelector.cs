using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessInterestSelector : DataTemplateSelector
    {
        public Int64 Threshold { get; set; }
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate InterestingTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Process process = (Process)item;

            if (process.WorkingSet64 > Threshold)
                return InterestingTemplate;

            return NormalTemplate;
        }
    }
}
