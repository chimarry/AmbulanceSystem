using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.MedicalTitleCRUD
{
    /// <summary>
    /// Interaction logic for DeleteModalWindow.xaml
    /// </summary>
    public partial class DeleteModalWindow : Window, ILanguageLocalizable, IThemeChangeable
    {
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();
        private readonly MedicalTitleViewModel medicalTitle;

        public OperationStatus OperationStatus { get; private set; }

        public DeleteModalWindow(MedicalTitleViewModel medicalTitle)
        {
            this.medicalTitle = medicalTitle;
            InitializeComponent();
            SwitchLanguage();
            ChangeTheme();
        }

        public void SwitchLanguage()
        {
            YesButton.Content = language.Yes;
            NoButton.Content = language.No;
            QuestionTextBlock.Text = language.ConfirmDelete;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DbStatus status = await medicalTitleService.Delete(Mapping.Mapper.Map<MedicalTitle>(medicalTitle));
            OperationStatus = StatusHandler.Handle(OperationType.Delete, status);
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
