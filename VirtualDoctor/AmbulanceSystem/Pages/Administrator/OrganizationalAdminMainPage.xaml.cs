using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

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
            SwitchLanguage();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
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

        private async void Clinic_Click(object sender, RoutedEventArgs e)
        {
            ClinicCRUD.IndexPage page = await ClinicCRUD.IndexPage.CreateIndexPage();
            PageUtil.NavigateToNextPage(Window.GetWindow(this), page);
        }

        private async void Place_Click(object sender, RoutedEventArgs e)
        {
            PlaceCRUD.IndexPage page = await PlaceCRUD.IndexPage.CreateIndexPage();
            PageUtil.NavigateToNextPage(Window.GetWindow(this), page);
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
                MedicalTitle.SetIconAndText(language.MedicalTitles, ButtonIcon.MedicalTitle);
                Doctor.SetIconAndText(language.Doctors, ButtonIcon.Doctor);
                Role.SetIconAndText(language.Roles, ButtonIcon.Roles);
                Place.SetIconAndText(language.Places, ButtonIcon.Place);
                Clinic.SetIconAndText(language.Clinics, ButtonIcon.Ambulance);
            }
        }
    }
}
