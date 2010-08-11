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

namespace DictionaryPages
{
    /// <summary>
    /// Interaction logic for SettingsPageFunction.xaml
    /// </summary>
    public partial class SettingsPageFunction : PageFunction<String>
    {
        private string currentDictionary = "";

        public SettingsPageFunction()
        {
            InitializeComponent();
        }

        public SettingsPageFunction(string strCurrent)
        {
            currentDictionary = strCurrent;
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            dictionaryCombo.Items.Add("A11");
            dictionaryCombo.Items.Add("moby-thes");
            dictionaryCombo.Items.Add("vera");
            dictionaryCombo.Items.Add("jargon");
            dictionaryCombo.Items.Add("easton");
            dictionaryCombo.Items.Add("bouvier");
            dictionaryCombo.Items.Add("devils");
            dictionaryCombo.Items.Add("world02");

            if (currentDictionary.Length > 0)
                dictionaryCombo.SelectedItem = currentDictionary;
            else
                dictionaryCombo.SelectedIndex = 0;
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            string value = dictionaryCombo.SelectedItem.ToString();
            OnReturn(new ReturnEventArgs<string>(value));
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<string>(null));
        }
    }
}
