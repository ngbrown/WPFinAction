using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Desktop_Wiki
{
    [Serializable]
    public class Wiki : INotifyPropertyChanged
    {
        private string wikiDataFile = "wikiPages.xml";

        public Wiki()
        {
            if (!File.Exists(wikiDataFile))
            {
                CreateDefaultDirectory();
                Save();
            }
            else
            {
                Load();
            }
        }

        private void CreateDefaultDirectory()
        {
            wikiDirectories = new ObservableCollection<PageDirectory>();
            wikiDirectories.Add(new PageDirectory("Home"));
        }

        private ObservableCollection<PageDirectory> wikiDirectories;
        public ObservableCollection<PageDirectory> Directories
        {
            get { return wikiDirectories; }
        }

        private PageDirectory currentDirectory;
        public PageDirectory CurrentDirectory
        {
            get { return currentDirectory; }
            set
            {
                currentDirectory = value;
                NotifyPropertyChanged("CurrentDirectory");
            }
        }

        public void AddPage()
        {
            CurrentDirectory.Pages.Add(new WikiPage("New Page"));
        }

        public void AddDirectory()
        {
            PageDirectory newDirectory = new PageDirectory(Guid.NewGuid().ToString());
            Directories.Add(newDirectory);
            CurrentDirectory = newDirectory;
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PageDirectory>));
            using (FileStream stream = new FileStream(wikiDataFile, FileMode.Open))
            {
                if (stream.CanRead)
                {
                    wikiDirectories = (ObservableCollection<PageDirectory>)serializer.Deserialize(stream);
                    NotifyPropertyChanged("Directories");
                }
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<PageDirectory>));
            using (FileStream stream = new FileStream(wikiDataFile, FileMode.Create))
            {
                serializer.Serialize(stream, wikiDirectories);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
