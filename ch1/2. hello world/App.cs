using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _2.hello_world
{
    public class EntryPoint
    {
        // All WPF applications should execute on a single-threaded apartment (STA) thread
        [STAThread]
        public static void Main()
        {
            App app = new App();
            app.Run();
        }
    }

    public class App : Application
    {
        Window window1;
        Page page1;
        StackPanel stackPanel1;
        Label label1;

        public void Procedural()
        {
            window1 = new Window();
            page1 = new Page();
            stackPanel1 = new StackPanel();

            label1 = new Label();
            label1.Content = "Hello, World!";

            stackPanel1.Children.Add(label1);
            page1.Content = stackPanel1;
            window1.Content = page1;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.Procedural();
            
            window1.Show();
        }
    }
}
