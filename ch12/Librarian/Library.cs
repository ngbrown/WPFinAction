using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace Librarian
{
    public class Library : INotifyPropertyChanged
    {
        private DataSet library;
        private int bookmarkIdentity = 0;
        private string libraryFilename = "bookmarks.library";

        public Library()
        {
            CreateDataSource();

            if (!File.Exists(libraryFilename))
            {
                CreateDefaultBookmarks();
                Save();
            }
            else
            {
                Load();
            }
        }

        private void CreateDataSource()
        {
            library = new DataSet("Librarian");

            DataTable bookmarks = new DataTable("Bookmarks");
            bookmarks.Columns.Add("Id", typeof(Int32));
            bookmarks.Columns.Add("Title", typeof(string));
            bookmarks.Columns.Add("Uri", typeof(String));
            bookmarks.Columns.Add("Category", typeof(string));
            bookmarks.Columns.Add("LastMod", typeof(DateTime));
            bookmarks.Constraints.Add("UniqueTitle", bookmarks.Columns[1], false);

            DataTable identity = new DataTable("Ident");
            identity.Columns.Add("Name", typeof(string));
            identity.Columns.Add("Count", typeof(Int32));

            library.Tables.Add(bookmarks);
            library.Tables.Add(identity);
        }

        public DataTable Bookmarks
        {
            get { return library.Tables["Bookmarks"]; }
        }

        public void AddBookmark(string name, string url, string category)
        {
            Bookmarks.Rows.Add(new object[] { bookmarkIdentity++, name, url, category, DateTime.Now });
            library.AcceptChanges();
            NotifyPropertyChanged("Bookmarks");
        }

        #region DataStore operations

        private void CreateDefaultBookmarks()
        {
            library.Tables["Ident"].Rows.Add(new object[] { "bookmarks", bookmarkIdentity });

            AddBookmark("Manning", "http://www.manning.com/", "Books");
            AddBookmark("Cherwell", "http://www.cherwellsoft.com/", "Sites");
            AddBookmark("Exotribe", "http://www.exotribe.com/", "Sites");

            library.AcceptChanges();
        }

        public void Load()
        {
            library.ReadXml(libraryFilename, XmlReadMode.ReadSchema);
        }

        public void Save()
        {
            library.AcceptChanges();
            library.WriteXml(libraryFilename, XmlWriteMode.WriteSchema);
        }

        #endregion

        #region INotifyPropertyChanged Membes

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
