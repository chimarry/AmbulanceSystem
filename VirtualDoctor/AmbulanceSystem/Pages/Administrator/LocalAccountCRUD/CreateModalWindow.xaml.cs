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
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();

        public OperationStatus OperationStatus { get; private set; }


        public CreateModalWindow()
        {
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
            this.DataContext = new LocalAccountRolesDataSource();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            FullNameLabel.Text = language.FullName;
            EmailLabel.Content = language.Email;
            PasswordLabel.Content = language.Password;
            RepeatPasswordLabel.Content = language.RepeatPassword;
            RolesTextBlock.Text = language.Roles;
            SaveButton.Content = language.Save;
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
                    FullName = FullNameBox.Text,
                    Email = EmailBox.Text,
                    PasswordHash = PasswordBox.Password,
                };
                ObservableCollection<RoleViewModel> roles = RolesComboBox.SelectedItems as ObservableCollection<RoleViewModel>;
                localAccount.SetRoles(roles.Select(x => Mapping.Mapper.Map<Role>(x)).ToList());
                DbStatus status = await localAccountService.Add(localAccount);
                OperationStatus = StatusHandler.Handle(OperationType.Create, status);
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
