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

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    /// <summary>
    /// Interaction logic for DeleteModalWindow.xaml
    /// </summary>
    public partial class DeleteModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        public OperationStatus OperationStatus { get; private set; }

        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly PlaceViewModel placeViewModel;

        public DeleteModalWindow(PlaceViewModel placeViewModel)
        {
            this.placeViewModel = placeViewModel;
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
            DbStatus status = await placeService.Delete(Mapping.Mapper.Map<Place>(placeViewModel));
            OperationStatus = StatusHandler.Handle(OperationType.Delete, status);
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
