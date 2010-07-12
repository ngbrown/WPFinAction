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

namespace Themes
{
    public enum ThemeTypes
    {
        None = 0,
        Aero,
        LunaDefault,
        LunaMetalic,
        LunaHomestead,
        Royale,
        Classic,
        Zune,
    }

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void OnSelectTheme(object sender, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)sender;

            if (btn.Tag != null)
            {
                ThemeTypes selectedTheme = (ThemeTypes)btn.Tag;

                string pathToTheme = null;

                switch (selectedTheme)
                {
                    case ThemeTypes.Aero:
                        pathToTheme = "/PresentationFramework.Aero, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/aero.normalcolor.xaml";
                        break;
                    case ThemeTypes.LunaDefault:
                        pathToTheme = "/PresentationFramework.Luna, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/luna.normalcolor.xaml";
                        break;
                    case ThemeTypes.LunaMetalic:
                        pathToTheme = "/PresentationFramework.Luna, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/luna.metallic.xaml";
                        break;
                    case ThemeTypes.LunaHomestead:
                        pathToTheme = "/PresentationFramework.Luna, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/luna.homestead.xaml";
                        break;
                    case ThemeTypes.Royale:
                        pathToTheme = "/PresentationFramework.Royale, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/royale.normalcolor.xaml";
                        break;
                    case ThemeTypes.Zune:
                    // Zune theme doesn't exist
                    //    pathToTheme = "/PresentationFramework.Zune ;component/themes/Zune.normalcolor.xaml";
                    //    break;
                    default:
                    case ThemeTypes.Classic:
                        pathToTheme = "/PresentationFramework.Classic, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35 ;component/themes/Classic.xaml";
                        break;
                }

                if (pathToTheme != null)
                {
                    Uri uriToTheme = new Uri(pathToTheme, UriKind.Relative);

                    object theme = Application.LoadComponent(uriToTheme);

                    if (Application.Current.Resources.MergedDictionaries.Count > 0)
                    {
                        Application.Current.Resources.MergedDictionaries[0] = (ResourceDictionary)theme;
                    }
                }
            }
        }
    }
}
