using AmbulanceSystem.Shared.Themes;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.LoginAuthentication
{
    /// <summary>
    /// Interaction logic for UserRoleModalWindow.xaml
    /// </summary>
    public partial class UserRoleModalWindow : Window
    {
        public UserRoleModalWindow(CustomPrincipal customPrincipal, Theme currentTheme)
        {
            InitializeComponent();
            Initialize(currentTheme);
            InitializeRoles(customPrincipal.Identity.Roles);
        }

        public void InitializeRoles(string[] possibleRoles)
        {
            foreach (var role in possibleRoles)
            {
                ListBoxItem roleItem = new ListBoxItem()
                {
                    Style = CurrentDictionary.MergedDictionaries[0]["ListBoxItemStyle"] as Style,
                    Height = 33,
                    Margin = new Thickness(0, 0, 0, 20),
                    Content = role
                };
                RolesListBox.Items.Add(roleItem);
            }
        }

        public void Initialize(Theme theme)
        {
            CurrentDictionary.MergedDictionaries[0].Source = theme.ToUri();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}
