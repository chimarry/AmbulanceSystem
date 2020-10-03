using AmbulanceSystem.Pages.LoginAuthentication;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AmbulanceSystem.Main
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ILanguageLocalizable
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SetUser();
            SwitchLanguage();
            MainFrame.Content = new LoginPage();
        }

        public void SetUser()
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);
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

            CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
            MainImageBrush.ImageSource = newTheme.ToImage();
            ThemeChanger.SetAsCurrentTheme(newTheme);
            (MainFrame.Content as IThemeChangeable).ChangeTheme();
        }

        private void SwitchLanguage_Click(object sender, RoutedEventArgs e)
        {
            LanguageLocalizer.SwitchLanguage();
            SwitchLanguage();
            (MainFrame.Content as ILanguageLocalizable).SwitchLanguage();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.GetNavigationService(this.MainFrame.Content as DependencyObject).CanGoBack)
                NavigationService.GetNavigationService(MainFrame.Content as DependencyObject).GoBack();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            (MainFrame.Content as IThemeChangeable).ChangeTheme();
            (MainFrame.Content as ILanguageLocalizable).SwitchLanguage();
        }
    }
}
