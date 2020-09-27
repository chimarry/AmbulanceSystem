using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmbulanceSystem.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for OrganizationalAdminMainPage.xaml
    /// </summary>
    public partial class OrganizationalAdminMainPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        public OrganizationalAdminMainPage()
        {
            InitializeComponent();
        }

        public void ChangeThemeTo(Theme newTheme)
        {
            throw new NotImplementedException();
        }

        public void SwitchLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
