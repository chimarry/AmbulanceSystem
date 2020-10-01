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

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        public OperationStatus OperationStatus { get; private set; }

        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly PlaceViewModel placeViewModel;

        public EditModalWindow(PlaceViewModel model)
        {
            placeViewModel = model;
            InitializeComponent();
            InitalizeComboBoxes();
            SwitchLanguage();
            ChangeTheme();
            PopulateContent();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            NameLabel.Content = language.Name;
            RadiationTextBlock.Text = language.Radiation;
            CountryNameLabel.Content = language.CountryName;
            PostalCodeLabel.Content = language.PostalCode;

            TerrainQualityTextBlock.Text = language.TerrainQuality;
            FoodQualityTextBlock.Text = language.FoodQuality;
            RecreationalWaterTextBlock.Text = language.RecreationalWaterQuality;
            DrinkingWaterQualityTextBlock.Text = language.DrinkingWaterQuality;
            MedicalVasteTextBlock.Text = language.MedicalVasteInformation;
            InlandWaterQualityTextBlock.Text = language.InlandWaterQualtiy;
            NoiseInformationTextBlock.Text = language.InlandWaterQualtiy;
            NoiseInformationTextBlock.Text = language.NoiseInformation;
            AirQualityTextBlock.Text = language.AirQuality;

            SaveButton.Content = language.Save;
        }

        private void PopulateContent()
        {
            NameBox.Text = placeViewModel.Name;
            CountryNameBox.Text = placeViewModel.CountryName;
            PostalCodeBox.Text = placeViewModel.PostalCode;
            FoodQualityComboBox.SelectedItem = placeViewModel.FoodQuality;
            AirQualityComboBox.SelectedItem = placeViewModel.AirQuality;
            DrinkingWaterComboBox.SelectedItem = placeViewModel.DrinkingWaterQuality;
            RecreationalWaterComboBox.SelectedItem = placeViewModel.RecreationalWaterQuality;
            TerrainQualityComboBox.SelectedItem = placeViewModel.TerrainQuality;
            InlandWaterQualityComboBox.SelectedItem = placeViewModel.InlandWaterQuality;
            MedicalVasteComboBox.SelectedItem = placeViewModel.MedicalVasteInformation;
            NoiseInformationComboBox.SelectedItem = placeViewModel.NoiseInformation;
            RadiationComboBox.SelectedItem = placeViewModel.Radiation;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormIsValid())
            {
                Place place = new Place()
                {
                    IdPlace = placeViewModel.IdPlace,
                    Name = NameBox.Text,
                    CountryName = CountryNameBox.Text,
                    PostalCode = PostalCodeBox.Text,
                    FoodQuality = (int)FoodQualityComboBox.SelectedItem,
                    AirQuality = (int)AirQualityComboBox.SelectedItem,
                    DrinkingWaterQuality = (int)DrinkingWaterComboBox.SelectedItem,
                    RecreationalWaterQuality = (int)RecreationalWaterComboBox.SelectedItem,
                    TerrainQuality = (int)TerrainQualityComboBox.SelectedItem,
                    InlandWaterQuality = (int)InlandWaterQualityComboBox.SelectedItem,
                    MedicalVasteInformation = (int)MedicalVasteComboBox.SelectedItem,
                    NoiseInformation = (int)NoiseInformationComboBox.SelectedItem,
                    Radiation = (int)RadiationComboBox.SelectedItem

                };
                DbStatus status = await placeService.Update(place);
                OperationStatus = StatusHandler.Handle(OperationType.Edit, status);
                Close();
            }
            else
                FieldValidation.WriteMessage(ErrorLabel, language.SelectValues);
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !FieldValidation.IsValidChar(e.Text);
        }

        private bool FormIsValid()
        {
            bool selectedComboBoxes = FieldValidation.AreSelected(FoodQualityComboBox.SelectedIndex, AirQualityComboBox.SelectedIndex
                                             , DrinkingWaterComboBox.SelectedIndex, TerrainQualityComboBox.SelectedIndex
                                             , InlandWaterQualityComboBox.SelectedIndex, MedicalVasteComboBox.SelectedIndex, RadiationComboBox.SelectedIndex, RecreationalWaterComboBox.SelectedIndex);
            bool inputedText = FieldValidation.NotNullOrEmpty(NameBox.Text, CountryNameBox.Text, PostalCodeBox.Text);
            return selectedComboBoxes && inputedText;
        }


        private void InitalizeComboBoxes()
        {
            for (int i = 0; i <= PlaceViewModel.EndScale; ++i)
            {
                RadiationComboBox.Items.Add(i);
                NoiseInformationComboBox.Items.Add(i);
                MedicalVasteComboBox.Items.Add(i);
            }
            for (int i = PlaceViewModel.EndScale; i >= 0; --i)
            {
                AirQualityComboBox.Items.Add(i);
                DrinkingWaterComboBox.Items.Add(i);
                RecreationalWaterComboBox.Items.Add(i);
                FoodQualityComboBox.Items.Add(i);
                TerrainQualityComboBox.Items.Add(i);
                InlandWaterQualityComboBox.Items.Add(i);
            }
        }
    }
}
