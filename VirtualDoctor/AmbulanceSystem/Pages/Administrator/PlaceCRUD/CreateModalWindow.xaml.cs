using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Controls;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        public OperationStatus OperationStatus { get; private set; }

        public CreateModalWindow()
        {
            InitializeComponent();
            InitalizeComboBoxes();
            SwitchLanguage();
            ChangeTheme();
        }
        public void SwitchLanguage()
        {
            NameLabel.Content = language.Name.Mandatory();
            RadiationTextBlock.Text = language.Radiation.Mandatory();
            CountryNameLabel.Content = language.CountryName.Mandatory();
            PostalCodeLabel.Content = language.PostalCode.Mandatory();

            TerrainQualityTextBlock.Text = language.TerrainQuality.Mandatory();
            FoodQualityTextBlock.Text = language.FoodQuality.Mandatory();
            RecreationalWaterTextBlock.Text = language.RecreationalWaterQuality.Mandatory();
            DrinkingWaterQualityTextBlock.Text = language.DrinkingWaterQuality.Mandatory();
            MedicalVasteTextBlock.Text = language.MedicalVasteInformation.Mandatory();
            InlandWaterQualityTextBlock.Text = language.InlandWaterQualtiy.Mandatory();
            NoiseInformationTextBlock.Text = language.InlandWaterQualtiy.Mandatory();
            NoiseInformationTextBlock.Text = language.NoiseInformation.Mandatory();
            AirQualityTextBlock.Text = language.AirQuality.Mandatory();

            SaveButton.Content = language.Save;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
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

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormIsValid())
            {
                Place place = new Place()
                {
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
                DbStatus status = await placeService.Add(place);
                OperationStatus = StatusHandler.Handle(OperationType.Create, status);
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
    }
}
