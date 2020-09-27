using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for AccountAdminMainPage.xaml
    /// </summary>
    public partial class AccountAdminMainPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        public AccountAdminMainPage()
        {
            InitializeComponent();
            SwitchLanguage();
            ChangeTheme();
        }

        public void ChangeTheme()
        {
            Theme newTheme = ThemeChanger.GetCurrentTheme();
            CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
        }

        public void SwitchLanguage()
        {
            AccountButton.Content = language.Accounts;
            OnLoaded();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void AccountButton_LayoutUpdated(object sender, EventArgs e)
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
                AccountButton.SetIconAndText(language.Accounts, ButtonIcon.Accounts);
        }
    }
}
