using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Themes;
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
        }

        public bool ChangeThemeTo(Theme newTheme)
        {
            throw new System.NotImplementedException();
        }

        public void SwitchLanguage()
        {
            throw new System.NotImplementedException();
        }
    }
}
