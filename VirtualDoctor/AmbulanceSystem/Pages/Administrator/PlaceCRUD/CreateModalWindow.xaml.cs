using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Controls;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();

        public CreateModalWindow()
        {
            InitializeComponent();
            InitalizeComboBoxes();
            SwitchLanguage();
            ChangeTheme();
            ModalDialog.SetParent(CreateGrid);
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
            ShowModalDialog_Click(status);
            ResetValues();
        }

        private void ResetValues()
        {
            NameBox.Text = string.Empty;
            CountryNameBox.Text = string.Empty;
            PostalCodeBox.Text = string.Empty;
            FoodQualityComboBox.SelectedItem = null;
            AirQualityComboBox.SelectedItem = null;
            DrinkingWaterComboBox.SelectedItem = null;
            RecreationalWaterComboBox.SelectedItem = null;
            TerrainQualityComboBox.SelectedItem = null;
            InlandWaterQualityComboBox.SelectedItem = null;
            MedicalVasteComboBox.SelectedItem = null;
            NoiseInformationComboBox.SelectedItem = null;
            RadiationComboBox.SelectedItem = null;
        }

        private void ShowModalDialog_Click(DbStatus status)
        {
            var res = ModalDialog.ShowHandlerDialog(status.ToString());
            //var resultMessagePrefix = "Result: ";
            //if (res)
            //    ResultText.Text = resultMessagePrefix + "Ok";
            //else
            //    ResultText.Text = resultMessagePrefix + "Cancel";
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

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }
    }
}
