using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Markup;
using System.IO;

namespace Desktop_Wiki
{
    [Serializable]
    public class WikiPage
    {
        public WikiPage()
        {
            Document = new FlowDocument();
        }

        public WikiPage(string name)
        {
            Name = name;
            Document = new FlowDocument();
        }

        private string pageName;
        public string Name
        {
            get { return pageName; }
            set { pageName = value; }
        }

        private FlowDocument document;
        internal FlowDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        public string XamlDocument
        {
            get { return XamlWriter.Save(Document); }
            set
            {
                using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
                {
                    Document = (FlowDocument)XamlReader.Load(stream);
                }
            }
        }
    }
}
