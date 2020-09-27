using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
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
            SwitchLanguage();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            MedicalRecordButton.Content = language.MedicalRecordsReview;
            OnLoaded();
        }

        private void MedicalRecordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MedicalRecordButton_LayoutUpdated(object sender, EventArgs e)
        {
            OnLoaded();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }

        private void OnLoaded()
        {
            if (IsLoaded)
                MedicalRecordButton.SetIconAndText(language.MedicalRecordsReview, ButtonIcon.MedicalRecord);
        }
    }
}
