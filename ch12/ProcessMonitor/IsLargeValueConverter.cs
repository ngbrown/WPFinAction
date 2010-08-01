using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ProcessMonitor
{
    public class IsLargeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Int64 convertedValue = System.Convert.ToInt64(value);

            Int64 threshold = 1000;
            if (parameter != null)
                threshold = System.Convert.ToInt64(parameter);

            return (convertedValue > threshold);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
