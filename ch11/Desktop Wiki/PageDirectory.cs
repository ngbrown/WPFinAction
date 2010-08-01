using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Desktop_Wiki
{
    [Serializable]
    public class PageDirectory
    {
        public PageDirectory() { }
        public PageDirectory(string name)
        {
            Name = name;
            pages = new ObservableCollection<WikiPage>();
        }

        private string collectionName;
        public string Name
        {
            get { return collectionName; }
            set { collectionName = value; }
        }

        private ObservableCollection<WikiPage> pages;
        public ObservableCollection<WikiPage> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
    }
}
