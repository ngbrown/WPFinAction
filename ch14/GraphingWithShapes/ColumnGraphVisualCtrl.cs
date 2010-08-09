﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphingWithShapes
{
    public class ColumnGraphVisualCtrl : FrameworkElement
    {
        private VisualCollection visuals;

        private ObservableCollection<NameValuePair> dataPoints = null;

        private List<Color> columnColors = new List<Color>() { Colors.Blue, Colors.Red, Colors.Green };

        public ColumnGraphVisualCtrl()
        {
            visuals = new VisualCollection(this);
            SizeChanged += new SizeChangedEventHandler(OnSizeChanged);
        }

        void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Update();
        }

        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        public void SetData(ObservableCollection<NameValuePair> data)
        {
            dataPoints = data;
            dataPoints.CollectionChanged += new NotifyCollectionChangedEventHandler(dataPoints_CollectionChanged);
            Update();
        }

        void dataPoints_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Update();
        }

        protected void Update()
        {
            visuals.Clear();

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
                DrawingVisual visual;
                foreach (NameValuePair nvp in dataPoints)
                {
                    visual = new DrawingVisual();
                    using (DrawingContext drawingContext = visual.RenderOpen())
                    {
                        fillBrush = new SolidColorBrush(columnColors[nIndex % columnColors.Count]);

                        height = (nvp.Value * unitHeight);
                        rect = new Rect(left, bottom - height, barWidth, height);
                        drawingContext.DrawRectangle(fillBrush, outlinePen, rect);
                    }

                    visuals.Add(visual);
                    nvp.Tag = visual;

                    left += rect.Width;
                    nIndex++;
                }
            }
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
    }
}
