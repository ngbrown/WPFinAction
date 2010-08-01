using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace ProcessMonitor
{
    public class NumberToFormattedTextValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Int64 size = System.Convert.ToInt64(value);
            string units = (parameter != null) ? parameter.ToString() : "IEC";

            switch(units)
            {
                case "IEC":
                    size = size / 1024;

                    if (size < 1024)
                        return size.ToString() + " KiB";
                    else
                        return (size / 1024).ToString() + " MiB";

                case "BINARYSI":
                    size = size / 1024;

                    if (size < 1024)
                        return size.ToString() + " KB";
                    else
                        return (size / 1024).ToString() + " MB";

                case "SI":
                    size = size / 1000;

                    if (size < 1000)
                        return size.ToString() + " KB";
                    else
                        return (size / 1024).ToString() + " MB";
            }

            return "Bad Param";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
