using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.Themes;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.LoginAuthentication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public bool ChangeThemeTo(Theme newTheme)
        {
            CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
            return true;
        }

        public void SwitchLanguage()
        {
            UsernameBlock.Text = language.Email;
            PasswordBlock.Text = language.Password;
            WelcomeTextBlock.Text = language.Welcome;
            LoginButton.Content = language.Login;
        }

        private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
