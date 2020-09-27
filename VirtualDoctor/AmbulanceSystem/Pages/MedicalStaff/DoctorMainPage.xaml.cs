using AmbulanceSystem.Shared;
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
