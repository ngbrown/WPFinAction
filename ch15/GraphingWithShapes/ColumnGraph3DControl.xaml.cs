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

            if (dataPoints != null)
            {
                double spaceToUseY = 5;
                double spaceToUseX = 5;
                double barWidth = spaceToUseX / dataPoints.Count;
                double largestValue = GetLargestValue();
                double unitHeight = spaceToUseY / largestValue;

                double bottom = -spaceToUseY;
                double left = -spaceToUseX;
                double height;
                int nIndex = 0;

                foreach (NameValuePair nvp in dataPoints)
                {
                    height = (nvp.Value * unitHeight);
                    Color col = columnColors[nIndex % columnColors.Count];

                    Model3D column = CreateColumn(left, bottom, height, barWidth, 0, barWidth, col);
                    ModelVisual3D model = new ModelVisual3D();
                    model.Content = column;
                    main.Children.Add(model);

                    AxisAngleRotation3D angleRot = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
                    RotateTransform3D rot = new RotateTransform3D(angleRot, new Point3D(-spaceToUseX + (spaceToUseX / 2), 0, -(barWidth / 2)));

                    Binding rotBind = new Binding("Value");
                    rotBind.Source = rotateSlider;
                    BindingOperations.SetBinding(angleRot, AxisAngleRotation3D.AngleProperty, rotBind);
                    model.Transform = rot;

                    left += barWidth;
                    nIndex++;
                }
            }
        }

        private Model3D CreateColumn(double left, double bottom, double height, double width, int front, double depth, Color col)
        {
            Model3DGroup modelGroup = new Model3DGroup();

            Point3D p0 = new Point3D(left, bottom, front);
            Point3D p1 = new Point3D(left + width, bottom, front);
            Point3D p2 = new Point3D(left, bottom + height, front);
            Point3D p3 = new Point3D(left + width, bottom + height, front);
            Point3D p4 = new Point3D(left, bottom, front - depth);
            Point3D p5 = new Point3D(left + width, bottom, front - depth);
            Point3D p6 = new Point3D(left, bottom + height, front - depth);
            Point3D p7 = new Point3D(left + width, bottom + height, front - depth);

            modelGroup.Children.Add(CreateSide(p0, p1, p2, p3, col)); // Front
            modelGroup.Children.Add(CreateSide(p0, p4, p2, p6, col)); // Left
            modelGroup.Children.Add(CreateSide(p4, p5, p6, p7, col)); // Back
            modelGroup.Children.Add(CreateSide(p1, p5, p3, p7, col)); // Right
            modelGroup.Children.Add(CreateSide(p2, p3, p6, p7, col)); // Top
            modelGroup.Children.Add(CreateSide(p0, p1, p4, p5, col)); // Bottom

            return modelGroup;
        }

        private Model3D CreateSide(Point3D pA, Point3D pB, Point3D pC, Point3D pD, Color col)
        {
            GeometryModel3D model = new GeometryModel3D();
            model.Material = new DiffuseMaterial(new SolidColorBrush(col));
            model.BackMaterial = model.Material;

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(pA);
            mesh.Positions.Add(pB);
            mesh.Positions.Add(pC);
            mesh.Positions.Add(pD);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(2);

            model.Geometry = mesh;

            return model;
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
