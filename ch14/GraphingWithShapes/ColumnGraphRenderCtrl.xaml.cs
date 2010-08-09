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
    /// Interaction logic for ColumnGraphRenderCtrl.xaml
    /// </summary>
    public partial class ColumnGraphRenderCtrl : UserControl
    {
        private ObservableCollection<NameValuePair> dataPoints = null;
        private List<Color> columnColors = new List<Color>() { Colors.Blue, Colors.Red, Colors.Green };

        public ColumnGraphRenderCtrl()
        {
            InitializeComponent();
        }

        public void SetData(ObservableCollection<NameValuePair> data)
        {
            dataPoints = data;
            dataPoints.CollectionChanged +=new NotifyCollectionChangedEventHandler(dataPoints_CollectionChanged);
            InvalidateVisual();
        }

        void  dataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }

        public double GetLargestValue()
        {
            double value = 0;
            foreach (NameValuePair nvp in dataPoints)
            {
                value = Math.Max(value, nvp.Value);
            }

            return value;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (dataPoints != null)
            {
                double spaceToUseY = ActualHeight * 0.8;
                double spaceToUseX = ActualWidth * 0.8;
                double barWidth = spaceToUseX / dataPoints.Count;
                double largestValue = GetLargestValue();
                double unitHeight = spaceToUseY / largestValue;

                double bottom = ActualHeight * 0.9;
                double left = ActualWidth * 0.1;

                Brush fillBrush;
                Pen outlinePen = new Pen(Brushes.Black, 1);
                int nIndex = 0;
                Rect rect;
                double height;

                foreach (NameValuePair nvp in dataPoints)
                {
                    fillBrush = new SolidColorBrush(columnColors[nIndex % columnColors.Count]);

                    height = (nvp.Value * unitHeight);
                    rect = new Rect(left, bottom - height, barWidth, height);
                    drawingContext.DrawRectangle(fillBrush, outlinePen, rect);

                    left += rect.Width;
                    nIndex++;
                }
            }
        }
    }
}
