using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.Themes;
using AmbulanceSystem.Utils;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for OrganizationalAdminMainPage.xaml
    /// </summary>
    public partial class OrganizationalAdminMainPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        private Theme currentTheme;

        public OrganizationalAdminMainPage(Theme currentTheme)
        {
            InitializeComponent();
            SwitchLanguage();
            ChangeThemeTo(currentTheme);
        }

        public void ChangeThemeTo(Theme newTheme)
        {
            CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
            currentTheme = newTheme;
        }

        public void SwitchLanguage()
        {
            MedicalTitle.Content = language.MedicalTitles;
            Role.Content = language.Roles;
            Doctor.Content = language.Doctors;
            Clinic.Content = language.Clinics;
            Place.Content = language.Places;
            OnLoaded();
        }

        private void Role_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Role_LayoutUpdated(object sender, EventArgs e)
        {
            OnLoaded();
        }

        private void Clinic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Doctor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MedicalTitle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }

        private void OnLoaded()
        {
            if (IsLoaded)
            {
                MedicalTitle.SetIconAndText(language.MedicalTitles, "Title.png");
                Doctor.SetIconAndText(language.Doctors, "Doctor.png");
                Role.SetIconAndText(language.Roles, "Roles.png");
                Place.SetIconAndText(language.Places, "Place.png");
                Clinic.SetIconAndText(language.Clinics, "Ambulance.png");
            }
        }
    }
}
