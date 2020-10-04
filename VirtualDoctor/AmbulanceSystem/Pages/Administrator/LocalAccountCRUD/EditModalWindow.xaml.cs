using AmbulanceDatabase.Context;
using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.LocalAccountCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();
        private readonly LocalAccountViewModel localAccountViewModel;

        public OperationStatus OperationStatus { get; private set; }

        public EditModalWindow(LocalAccountViewModel localAccountViewModel)
        {
            this.localAccountViewModel = localAccountViewModel;
            InitializeComponent();
            InitializeData();
            ChangeTheme();
            SwitchLanguage();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            FullNameLabel.Content = language.FullName.Mandatory();
            EmailLabel.Content = language.Email.Mandatory();
            PasswordLabel.Content = language.Password.Mandatory();
            RepeatPasswordLabel.Content = language.RepeatPassword.Mandatory();
            RolesTextBlock.Text = language.Roles;
            SaveButton.Content = language.Save;
        }

        private void InitializeData()
        {
            DataContext = new LocalAccountRolesDataSource(localAccountViewModel.IdLocalAccount);
            FullNameBox.Text = localAccountViewModel.FullName;
            PasswordBox.Password = localAccountViewModel.PasswordHash;
            EmailBox.Text = localAccountViewModel.Email;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!AreSamePasswords())
            {
                FieldValidation.WriteMessage(ErrorLabel, language.PasswordsDontMatch);
                return;
            }
            if (IsValidForm())
            {
                LocalAccount localAccount = new LocalAccount()
                {
                    IdLocalAccount = localAccountViewModel.IdLocalAccount,
                    FullName = FullNameBox.Text,
                    Email = EmailBox.Text,
                    PasswordHash = PasswordBox.Password,
                };
                ObservableCollection<RoleViewModel> roles = RolesComboBox.SelectedItems as ObservableCollection<RoleViewModel>;
                localAccount.SetRoles(roles.Select(x => Mapping.Mapper.Map<Role>(x)).ToList());
                DbStatus status = await localAccountService.Update(localAccount);
                OperationStatus = StatusHandler.Handle(OperationType.Edit, status);
                Close();
            }
            else FieldValidation.WriteMessage(ErrorLabel, language.SelectValues);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !FieldValidation.IsValidChar(e.Text);
        }

        private bool IsValidForm() =>
            FieldValidation.NotNullOrEmpty(FullNameBox.Text, EmailBox.Text, PasswordBox.Password) && FieldValidation.IsSelected(RolesComboBox.SelectedIndex);

        private bool AreSamePasswords() => PasswordBox.Password == RepeatPasswordBox.Password;

        private void PasswordBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = string.Empty == e.Text;
        }
    }
}