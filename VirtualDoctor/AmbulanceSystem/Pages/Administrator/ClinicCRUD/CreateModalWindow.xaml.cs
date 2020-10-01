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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.ClinicCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly IClinicService clinicService = ServicesAmbulanceFactory.GetInstance().CreateIClinicService();

        public OperationStatus OperationStatus { get; private set; }

        private CreateModalWindow()
        {
            InitializeComponent();
            SwitchLanguage();
            ChangeTheme();
        }

        public static async Task<CreateModalWindow> Create()
        {
            CreateModalWindow modal = new CreateModalWindow();
            await modal.InitializePlaces();
            return modal;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            NameLabel.Content = language.Name;
            PlaceTextBlock.Text = language.Place;
        }

        private async Task InitializePlaces()
        {
            List<PlaceViewModel> places = (await placeService.GetAll()).Select(x => Mapping.Mapper.Map<PlaceViewModel>(x)).ToList();
            foreach (PlaceViewModel place in places)
                PlaceComboBox.Items.Add(place);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForm())
            {
                DbStatus status = await clinicService.Add(new Clinic()
                {
                    Name = NameBox.Text,
                    IdPlace = ((PlaceViewModel)PlaceComboBox.SelectedItem).IdPlace
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
            FieldValidation.NotNullOrEmpty(NameBox.Text) && FieldValidation.IsSelected(PlaceComboBox.SelectedIndex);
    }
}
