using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for ColumnGraphCtrl.xaml
    /// </summary>
    public partial class ColumnGraphCtrl : UserControl
    {
        private ObservableCollection<NameValuePair> dataPoints = null;
        private List<Color> columnColors = new List<Color>() { Colors.Blue, Colors.Red, Colors.Green };

        public ColumnGraphCtrl()
        {
            InitializeComponent();
        }

        public void SetData(ObservableCollection<NameValuePair> data)
        {
            dataPoints = data;
            dataPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(DataChanged);
            Update();
        }

        void DataChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            Rectangle rect;
            foreach (NameValuePair nvp in dataPoints)
            {
                if (nvp.Tag == null)
                {
                    rect = new Rectangle();
                    rect.Stroke = Brushes.Black;
                    rect.StrokeThickness = 1;
                    main.Children.Add(rect);
                    nvp.Tag = rect;
                }
            }

            CalculatePositionsAndSizes();
        }

        public void CalculatePositionsAndSizes()
        {
            if (dataPoints == null)
                return;

            double spaceToUseY = main.ActualHeight * 0.8;
            double spaceToUseX = main.ActualWidth * 0.8;

            double barWidth = spaceToUseX / dataPoints.Count;
            double largestValue = GetLargestValue();
            double unitHeight = spaceToUseY / largestValue;

            double bottom = main.ActualHeight * 0.1;
            double left = main.ActualWidth * 0.1;

            Rectangle rect;
            int nIndex = 0;
            foreach (NameValuePair nvp in dataPoints)
            {
                rect = nvp.Tag as Rectangle;
                rect.Fill = new SolidColorBrush(columnColors[nIndex++ % columnColors.Count]);

                rect.Width = barWidth;
                rect.Height = nvp.Value * unitHeight;
                Canvas.SetLeft(rect, left);
                Canvas.SetBottom(rect, bottom);
                left += rect.Width;
            }
        }

        private double GetLargestValue()
        {
            double value = 0;
            foreach (NameValuePair nvp in dataPoints)
            {
                value = Math.Max(value, nvp.Value);
            }

            return value;
        }

        private void main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalculatePositionsAndSizes();
        }
    }
}
