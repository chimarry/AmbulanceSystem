﻿using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.LoginAuthentication
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IUserAuthenticationService userAuthenticationService = ServicesAmbulanceFactory.GetInstance().CreateIUserAuthenticationService();

        public LoginPage()
        {
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            UsernameBlock.Text = language.Email;
            PasswordBlock.Text = language.Password;
            WelcomeTextBlock.Text = language.Welcome;
            LoginButton.Content = language.Login;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            customPrincipal.Identity = await GetUserPrincipal();
            if (customPrincipal.Identity.GetType() != typeof(AnonymousIdentity))
                new UserRoleModalWindow(customPrincipal, Window.GetWindow(this)).ShowDialog();
        }

        private async Task<CustomIdentity> GetUserPrincipal()
        {
            LocalAccount localAccount = await userAuthenticationService.ValidateUser(UsernameBox.Text, PasswordBox.Password);
            if (localAccount == null)
            {
                Error.Text = language.WrongPasswordOrEmail;
                Error.Visibility = Visibility.Visible;
                return new AnonymousIdentity();
            }

            List<Role> roleNames = localAccount.GetRoles();
            if (roleNames == null)
            {
                Error.Text = language.NoKnownRoles;
                Error.Visibility = Visibility.Visible;
                return new AnonymousIdentity();
            }
            Error.Visibility = Visibility.Collapsed;
            string[] accountRoles = roleNames.Select(x => x.RoleName).ToArray();
            CustomIdentity customIdentity = new CustomIdentity(localAccount.FullName, localAccount.Email, accountRoles, localAccount.PasswordHash);
            return customIdentity;
        }

        private void CollapseErrorIndicator(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Error.Visibility = Visibility.Collapsed;
        }
    }
}
