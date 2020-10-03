using AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
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

        private async void MedicalRecordButton_Click(object sender, RoutedEventArgs e)
        {
            IndexPage page = await IndexPage.CreateIndexPage(false);
            PageUtil.NavigateToNextPage(Window.GetWindow(this), page);
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
