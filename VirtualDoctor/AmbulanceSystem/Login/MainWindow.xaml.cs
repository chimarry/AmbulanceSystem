using AmbulanceSystem.Pages.LoginAuthentication;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.Themes;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        public Theme CurrentTheme { get; set; }
        private Page PreviousPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SetUser();
            SwitchLanguage();
            CurrentTheme = Theme.Gryffindor;
            MainFrame.Content = new LoginPage();
        }

        public void SetUser()
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
        }

        public bool ChangeThemeTo(Theme newTheme)
        {
            try
            {
                CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
                MainImageBrush.ImageSource = newTheme.ToImage();
                CurrentTheme = newTheme;
                (MainFrame.Content as IThemeChangeable).ChangeThemeTo(newTheme);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SwitchLanguage()
        {
            AboutItem.Header = language.About;
            SwitchThemeItem.Header = language.Theme;
            GoBackItem.Header = language.GoBack;
            SwitchLanguageItem.Header = language.Translate;
        }

        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clickedTheme = (MenuItem)sender;
            Theme newTheme = (Theme)Enum.Parse(typeof(Theme), clickedTheme.Header.ToString());
            bool isChanged = ChangeThemeTo(newTheme);
        }

        private void SwitchLanguage_Click(object sender, RoutedEventArgs e)
        {
            LanguageLocalizer.SwitchLanguage();
            SwitchLanguage();
            (MainFrame.Content as ILanguageLocalizable).SwitchLanguage();
        }
    }
}
