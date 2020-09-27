using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Themes;
using System;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.MedicalStaff
{
    /// <summary>
    /// Interaction logic for DoctorMainPage.xaml
    /// </summary>
    public partial class DoctorMainPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        public DoctorMainPage()
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
