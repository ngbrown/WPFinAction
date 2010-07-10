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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Layouts
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Button button1 = null;
        Button button2 = null;
        Button button3 = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            button1 = new Button { Content = "Button", Width = 70, Height = 23 };
            Canvas.SetLeft(button1, 119);
            Canvas.SetTop(button1, 24);
            canvas1.Children.Add(button1);

            button2 = new Button { Content = "Button2 is quite wide" };
            Canvas.SetLeft(button2, 44);
            Canvas.SetTop(button2, 69);
            canvas1.Children.Add(button2);

            button3 = new Button { Content = "Button" };
            Canvas.SetLeft(button3, 78);
            Canvas.SetTop(button3, 119);
            button3.Padding = new Thickness(10, 2, 10, 2);
            canvas1.Children.Add(button3);
        }
    }
}
