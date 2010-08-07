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
using System.Data;

namespace Librarian
{
    /// <summary>
    /// Interaction logic for BookmarkLibrary.xaml
    /// </summary>
    public partial class BookmarkLibrary : Window
    {
        public BookmarkLibrary()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Library library = (Library)FindResource("library");
            library.Save();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)bookmarks.SelectedItem;
            row.Delete();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Library library = (Library)FindResource("library");
            library.AddBookmark("New Bookmark", "url", "");
        }
    }
}
