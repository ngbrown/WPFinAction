using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WorldBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DictionaryLookup Lookup { get; private set; }

        public static new App Current
        {
            [System.Diagnostics.DebuggerStepThrough]
            get { return (App)Application.Current; }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Lookup = new DictionaryLookup();
        }
    }
}
