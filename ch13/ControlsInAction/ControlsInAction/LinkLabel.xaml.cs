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

namespace ControlsInAction
{
    /// <summary>
    /// Interaction logic for LinkLabel.xaml
    /// </summary>
    public partial class LinkLabel : UserControl
    {
        public LinkLabel()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register(
            "Text", typeof(string), typeof(LinkLabel), 
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChanged)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register(
            "Uri", typeof(string), typeof(LinkLabel),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnUriChanged)));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LinkLabel label = (LinkLabel)sender;
            label.webLink.Inlines.Clear();
            label.webLink.Inlines.Add(new Run(e.NewValue.ToString()));
        }

        private static void OnUriChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LinkLabel label = (LinkLabel)sender;
            Uri newUri;
            try
            {
                newUri = new Uri(label.Uri);
                label.webLink.NavigateUri = newUri;
                label.webLink.ToolTip = String.Format("Open a new browser to {0}", label.Uri);
            }
            catch (UriFormatException ex)
            {
                label.webLink.ToolTip = String.Format("{0} ({1})", ex.Message, label.Uri);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
