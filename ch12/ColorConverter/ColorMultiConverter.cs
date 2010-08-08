using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorConverter
{
    class ColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte R = System.Convert.ToByte((double)values[0]);
            byte G = System.Convert.ToByte((double)values[1]);
            byte B = System.Convert.ToByte((double)values[2]);

            Color newColor = Color.FromRgb(R, G, B);

            return newColor;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
