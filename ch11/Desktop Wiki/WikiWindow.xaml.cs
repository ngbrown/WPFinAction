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
using System.Windows.Shapes;

namespace Desktop_Wiki
{
    /// <summary>
    /// Interaction logic for WikiWindow.xaml
    /// </summary>
    public partial class WikiWindow : Window
    {
        public static RoutedCommand About = new RoutedCommand();

        static WikiWindow()
        {
            About.InputGestures.Add(new KeyGesture(Key.F3));
            About.InputGestures.Add(new MouseGesture(MouseAction.RightDoubleClick, ModifierKeys.Control));
        }

        public WikiWindow()
        {
            InitializeComponent();
        }

        private void AboutExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            MessageBox.Show("Welcome to WikiInAction");
        }

        private void CreateLinkFromSelectionExecuted(object sender, ExecutedRoutedEventArgs args)
        {

        }

        private void CreateLinkFromSelectionCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            RichTextBox wikiEditor = sender as RichTextBox;
            args.CanExecute = !wikiEditor.Selection.IsEmpty;
            args.ContinueRouting = false;
            args.Handled = true;
        }

        private void activePage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox wikiEditor = sender as RichTextBox;

            WikiPage page = e.NewValue as WikiPage;
            if (page != null)
            {
                wikiEditor.Document = page.Document;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Wiki wiki = (Wiki)FindResource("wiki");
            wiki.AddPage();
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Wiki wiki = (Wiki)FindResource("wiki");
            wiki.Save();
        }
    }
}
