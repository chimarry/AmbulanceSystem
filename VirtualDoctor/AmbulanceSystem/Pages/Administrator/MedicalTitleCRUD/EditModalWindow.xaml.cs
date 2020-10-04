using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.MedicalTitleCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();
        private readonly MedicalTitleViewModel medicalTitle;

        public OperationStatus OperationStatus { get; private set; }

        public EditModalWindow(MedicalTitleViewModel medicalTitleViewModel)
        {
            medicalTitle = medicalTitleViewModel;
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
            NameLabel.Content = language.MedicalTitle.Mandatory();
            SaveButton.Content = language.Save;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForm())
            {
                DbStatus status = await medicalTitleService.Update(new MedicalTitle()
                {
                    Name = NameBox.Text,
                    IdMedicalTitle = medicalTitle.IdMedicalTitle

                });
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
            FieldValidation.NotNullOrEmpty(NameBox.Text);
    }
}
