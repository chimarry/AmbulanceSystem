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

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    /// <summary>
    /// Interaction logic for DeleteModalWindow.xaml
    /// </summary>
    public partial class DeleteModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IDoctorService doctorService = ServicesAmbulanceFactory.GetInstance().CreateIDoctorService();
        private readonly DoctorViewModel doctorViewModel;

        public OperationStatus OperationStatus { get; private set; }

        public DeleteModalWindow(DoctorViewModel doctor)
        {
            this.doctorViewModel = doctor;
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
            DbStatus status = await doctorService.Delete(Mapping.Mapper.Map<Doctor>(doctorViewModel));
            OperationStatus = StatusHandler.Handle(OperationType.Delete, status);
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
