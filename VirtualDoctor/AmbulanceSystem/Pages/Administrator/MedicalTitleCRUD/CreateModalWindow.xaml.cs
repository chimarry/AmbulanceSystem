using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.MedicalTitleCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, ILanguageLocalizable, IThemeChangeable
    {
        public OperationStatus OperationStatus { get; private set; }
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();

        public CreateModalWindow()
        {
            InitializeComponent();
            SwitchLanguage();
            ChangeTheme();
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
                DbStatus status = await medicalTitleService.Add(new MedicalTitle()
                {
                    Name = NameBox.Text
                });
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
            FieldValidation.NotNullOrEmpty(NameBox.Text);
    }
}
