using AmbulanceSystem.Pages.Administrator;
using AmbulanceSystem.Pages.MedicalStaff;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Utils;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.LoginAuthentication
{
    /// <summary>
    /// Interaction logic for UserRoleModalWindow.xaml
    /// </summary>
    public partial class UserRoleModalWindow : Window
    {
        private readonly Window mainWindow;

        public UserRoleModalWindow(CustomPrincipal customPrincipal, Window mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            Initialize();
            InitializeRoles(customPrincipal.Identity.Roles);
        }

        public void InitializeRoles(string[] possibleRoles)
        {
            foreach (string role in possibleRoles)
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

        public void Initialize()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NextPage((RolesListBox.SelectedItem as ListBoxItem).Content.ToString());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NextPage(string role)
        {
            Close();
            Page nextPage;
            switch (role)
            {
                case "Doctor": nextPage = new DoctorMainPage(); break;
                case "OrganizationalAdmin": nextPage = new OrganizationalAdminMainPage(); break;
                case "AccountAdmin": nextPage = new AccountAdminMainPage(); break;
                case "PatientAdmin": nextPage = new PatientAdminMainPage(); break;
                default: nextPage = new LoginPage(); break;
            }
            mainWindow.NavigateToNextPage(nextPage);
        }
    }
}
