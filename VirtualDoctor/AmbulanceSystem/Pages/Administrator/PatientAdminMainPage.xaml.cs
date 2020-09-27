using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Themes;
using System;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for PatientAdminMainPage.xaml
    /// </summary>
    public partial class PatientAdminMainPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        public PatientAdminMainPage()
        {
            InitializeComponent();
        }

        public bool ChangeThemeTo(Theme newTheme)
        {
            throw new NotImplementedException();
        }

        public void SwitchLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
