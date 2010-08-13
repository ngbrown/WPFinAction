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

namespace WorldBrowser
{
    /// <summary>
    /// Interaction logic for WorldListView.xaml
    /// </summary>
    public partial class WorldListView : UserControl
    {
        public WorldListView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadContinents();
        }

        private void LoadContinents()
        {
            AddContinent("Africa", false, new string[] { "Algeria", "Angola", "Benin", "Botswana", "Burkina", "Burundi", "Cameroon", "Cape Verde", "Central African", "Republic", "Chad", "Comoros", "Congo", "Djibouti", "Egypt", "Equatorial Guinea", "Eritrea", "Ethiopia", "Gabon", "Gambia", "Ghana", "Guinea", "Guinea-Bissau", "Ivory Coast", "Kenya", "Lesotho", "Liberia", "Libya", "Madagascar", "Malawi", "Mali", "Mauritania", "Mauritius", "Morocco", "Mozambique", "Namibia", "Niger", "Nigeria", "Rwanda", "Sao Tome", "and Principe", "Senegal", "Seychelles", "Sierra Leone", "Somalia", "South Africa", "Sudan", "Swaziland", "Tanzania", "Togo", "Tunisia", "Uganda", "Zambia", "Zimbabwe" });
            AddContinent("Asia", false, new string[] { "Afghanistan", "Bahrain", "Bangladesh", "Bhutan", "Brunei", "Burma (Myanmar)", "Cambodia", "China", "East Timor", "India", "Indonesia", "Iran", "Iraq", "Israel", "Japan", "Jordan", "Kazakhstan", "North Korea", "South Korea", "Kuwait", "Kyrgyzstan", "Laos", "Lebanon", "Malaysia", "Maldives", "Mongolia", "Nepal", "Oman", "Pakistan", "Philippines", "Qatar", "Russian Federation", "Saudi Arabia", "Singapore", "Sri Lanka", "Syria", "Tajikistan", "Thailand", "Turkey", "Turkmenistan", "United Arab Emirates", "Uzbekistan", "Vietnam", "Yemen" });
            AddContinent("Europe", true, new string[] { "Albania", "Andorra", "Armenia", "Austria", "Azerbaijan", "Belarus", "Belgium", "Bosnia and Herzegovina", "Bulgaria", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Georgia", "Germany", "Greece", "Hungary", "Iceland", "Ireland", "Italy", "Latvia", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Malta", "Moldova", "Monaco", "Montenegro", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "San Marino", "Serbia", "Slovakia", "Slovenia", "Spain", "Sweden", "Switzerland", "Ukraine", "United Kingdom", "Vatican City" });
            AddContinent("North America", false, new string[] { "Antigua and Barbuda", "Bahamas", "Barbados", "Belize", "Canada", "Costa Rica", "Cuba", "Dominica", "Dominican Rep.", "El Salvador", "Grenada", "Guatemala", "Haiti", "Honduras", "Jamaica", "Mexico", "Nicaragua", "Panama", "St. Kitts & Nevis", "St. Lucia", "St. Vincent &", "the Grenadines", "Trinidad & Tobago", "United States" });
            AddContinent("Oceana", false, new string[] { "Australia", "Fiji", "Kiribati", "Marshall Islands", "Micronesia", "Nauru", "New Zealand", "Palau", "Papua New Guinea", "Samoa", "Solomon Islands", "Tonga", "Tuvalu", "Vanuatu" });
            AddContinent("South America", false, new string[] { "Argentina", "Bolivia", "Brazil", "Chile", "Colombia", "Ecuador", "Guyana", "Paraguay", "Peru", "Suriname", "Uruguay", "Venezuela" });
        }

        private void AddContinent(string continent, bool open, string[] countryList)
        {
            Expander exp = new Expander();
            exp.Header = continent;
            exp.IsExpanded = open;

            ListBox lb = new ListBox();
            lb.BorderThickness = new Thickness(0);
            lb.MouseDoubleClick += new MouseButtonEventHandler(lb_MouseDoubleClick);
            foreach (string country in countryList)
                lb.Items.Add(country);
            exp.Content = lb;

            continentStackPanel.Children.Add(exp);
        }

        void lb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                ListBox lb = sender as ListBox;
                if (lb.SelectedItem != null)
                {
                    string country = lb.SelectedItem.ToString();
                    FlowDocument doc = App.Current.Lookup.DefineWord(country);

                    docReaderA.Document = doc;
                    doc.Background = docReaderA.Background;
                }
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }
}
