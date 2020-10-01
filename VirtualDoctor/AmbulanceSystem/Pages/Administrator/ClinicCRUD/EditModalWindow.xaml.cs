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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AmbulanceSystem.Pages.Administrator.ClinicCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        public OperationStatus OperationStatus { get; private set; }

        private readonly IClinicService clinicService = ServicesAmbulanceFactory.GetInstance().CreateIClinicService();
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly ClinicViewModel clinicViewModel;

        private EditModalWindow(ClinicViewModel model)
        {
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
            clinicViewModel = model;
        }

        public static async Task<EditModalWindow> Create(ClinicViewModel model)
        {
            EditModalWindow modal = new EditModalWindow(model);
            await modal.InitializeData();
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForm())
            {
                DbStatus status = await clinicService.Update(new Clinic()
                {
                    IdClinic = clinicViewModel.IdClinic,
                    Name = NameBox.Text,
                    IdPlace = ((PlaceViewModel)PlaceComboBox.SelectedItem).IdPlace
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
            FieldValidation.NotNullOrEmpty(NameBox.Text) && FieldValidation.IsSelected(PlaceComboBox.SelectedIndex);

        private async Task InitializeData()
        {
            try
            {
                List<PlaceViewModel> places = (await placeService.GetAll()).Select(x => Mapping.Mapper.Map<PlaceViewModel>(x)).ToList();
                foreach (PlaceViewModel p in places)
                    PlaceComboBox.Items.Add(p);

                NameBox.Text = clinicViewModel.Name;
                Place place = await placeService.GetByPrimaryKey(new Place()
                {
                    IdPlace = clinicViewModel.IdPlace
                });
                PlaceComboBox.SelectedItem = Mapping.Mapper.Map<PlaceViewModel>(place);
            }
            catch (Exception)
            {
                OperationStatus = new OperationStatus(language.DatabaseError, AlertType.Error);
                Close();
            }
        }
    }
}
