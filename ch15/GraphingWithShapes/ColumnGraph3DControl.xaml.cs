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
using System.Windows.Media.Media3D;

namespace GraphingWithShapes
{
    /// <summary>
    /// Interaction logic for ColumnGraph3DControl.xaml
    /// </summary>
    public partial class ColumnGraph3DControl : UserControl
    {
        private ObservableCollection<NameValuePair> dataPoints = null;
        private List<Color> columnColors = new List<Color>() { Colors.LightBlue, Colors.Red, Colors.LightGreen, Colors.Yellow };

        public ColumnGraph3DControl()
        {
            InitializeComponent();
        }

        public void SetData(ObservableCollection<NameValuePair> data)
        {
            dataPoints = data;
            dataPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(dataPoints_CollectionChanged);
        }

        void dataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Update();
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

        protected void Update()
        {
            ClearModels();

            // Details to follow
        }

        private void ClearModels()
        {
            ModelVisual3D model;
            for (int i = main.Children.Count - 1; i >= 0; i--)
            {
                model = (ModelVisual3D)main.Children[i];
                if (!(model.Content is Light))
                    main.Children.RemoveAt(i);
            }
        }
    }
}
