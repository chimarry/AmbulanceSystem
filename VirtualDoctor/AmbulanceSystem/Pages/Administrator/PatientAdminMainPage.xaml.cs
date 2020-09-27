using AmbulanceSystem.Shared;
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

        public void ChangeTheme()
        {
            throw new NotImplementedException();
        }

        public void SwitchLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
