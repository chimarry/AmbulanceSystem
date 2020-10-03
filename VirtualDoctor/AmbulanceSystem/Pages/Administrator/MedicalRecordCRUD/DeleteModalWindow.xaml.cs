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

namespace AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD
{
    /// <summary>
    /// Interaction logic for DeleteModalWindow.xaml
    /// </summary>
    public partial class DeleteModalWindow : Window
    {
        private readonly IMedicalRecordService medicalRecordService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalRecordService();
        private readonly MedicalRecordViewModel medicalRecordViewModel;

        public OperationStatus OperationStatus { get; private set; }

        public DeleteModalWindow(MedicalRecordViewModel model)
        {
            this.medicalRecordViewModel = model;
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
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
            DbStatus status = await medicalRecordService.Delete(Mapping.Mapper.Map<MedicalRecord>(medicalRecordViewModel));
            OperationStatus = StatusHandler.Handle(OperationType.Delete, status);
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
