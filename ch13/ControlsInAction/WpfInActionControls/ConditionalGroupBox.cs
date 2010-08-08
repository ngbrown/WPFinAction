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

namespace WpfInActionControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfInActionControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfInActionControls;assembly=WpfInActionControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ConditionalGroupBox/>
    ///
    /// </summary>
    public class ConditionalGroupBox : HeaderedContentControl
    {
        static ConditionalGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConditionalGroupBox), new FrameworkPropertyMetadata(typeof(ConditionalGroupBox)));
        }

        public static readonly DependencyProperty IsContentEnabledProperty =
            DependencyProperty.Register(
            "IsContentEnabled",
            typeof(bool),
            typeof(ConditionalGroupBox),
            new PropertyMetadata(true, new PropertyChangedCallback(OnIsContentEnabledChanged)));

        public bool IsContentEnabled
        {
            get { return (bool)GetValue(IsContentEnabledProperty); }
            set { SetValue(IsContentEnabledProperty, value); }
        }

        private static void OnIsContentEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            bool enabled = (bool)e.NewValue;
            ConditionalGroupBox groupBox = (ConditionalGroupBox)sender;

            UIElement content = groupBox.Content as UIElement;
            if (content != null)
                content.IsEnabled = enabled;
        }
    }
}
