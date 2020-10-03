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
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly IMedicalRecordService medicalRecordService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalRecordService();

        public OperationStatus OperationStatus { get; private set; }

        private CreateModalWindow()
        {
            InitializeComponent();
            InitializeDatePicker();
            SwitchLanguage();
            ChangeTheme();
        }

        public static async Task<CreateModalWindow> Create()
        {
            CreateModalWindow modal = new CreateModalWindow();
            await modal.InitializeComboBox();
            return modal;
        }

        public void SwitchLanguage()
        {
            FirstNameLabel.Content = language.Name;
            LastNameLabel.Content = language.LastName;
            UNBLabel.Content = language.UBN;
            BirthDateLabel.Content = language.BirthDate;
            BirthPlaceLabel.Content = language.BirthPlace;
            GenderLabel.Content = language.Gender;
            MarriageStatusLabel.Content = language.MarriageStatus;
            MothersNameLabel.Content = language.MothersFullName;
            MothersProfessionLabel.Content = language.MothersProfession;
            FathersNameLabel.Content = language.FathersFullName;
            FathersProfessionLabel.Content = language.FathersProfession;
            InsuranceNumberLabel.Content = language.InsuranceNumber;
            ResidancePlaceTextBlock.Text = language.ResidenceName;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        private void InitializeDatePicker()
        {
            BirthDatePicker.SelectedDate = new System.DateTime(1997, 01, 01);
        }

        private async Task InitializeComboBox()
        {
            IList<Place> places = await placeService.GetAll();
            foreach (Place place in places)
                ResidancePlaceComboBox.Items.Add(Mapping.Mapper.Map<PlaceViewModel>(place));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FormIsValid())
            {
                MedicalRecord medicalRecord = new MedicalRecord()
                {
                    Name = FirstNameBox.Text,
                    LastName = LastNameBox.Text,
                    BirthDate = BirthDatePicker.SelectedDate.Value,
                    BirthPlace = BirthPlaceBox.Text,
                    FathersFullName = FathersNameBox.Text,
                    MothersFullName = MothersNameBox.Text,
                    FathersProfession = FathersProfessionBox.Text,
                    MothersProfession = MothersProfessionBox.Text,
                    Gender = (sbyte)(MaleCheckBox.IsChecked.Value ? 0 : 1),
                    MarriageStatus = (sbyte)(MarriedCheckBox.IsChecked.Value ? 0 : 1),
                    Jmb = UNBBox.Text,
                    InsuranceNumber = InsuranceNumberBox.Text,
                    IdResidence = ((PlaceViewModel)ResidancePlaceComboBox.SelectedItem).IdPlace
                };
                DbStatus status = await medicalRecordService.Add(medicalRecord);
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
            bool areFilled = FieldValidation.NotNullOrEmpty(FirstNameBox.Text, LastNameBox.Text, BirthPlaceBox.Text, UNBBox.Text,
                                                            MothersNameBox.Text, MothersProfessionBox.Text, FathersNameBox.Text, FathersProfessionBox.Text,
                                                            InsuranceNumberBox.Text);
            bool areChecked = (MaleCheckBox.IsChecked.Value || FemaleCheckBox.IsChecked.Value) && (MarriedCheckBox.IsChecked.Value || NotMarriedCheckBox.IsChecked.Value);
            return areFilled && areChecked && FieldValidation.AreSelected(ResidancePlaceComboBox.SelectedIndex);
        }

        private void Male_Checked(object sender, RoutedEventArgs e)
        {
            FemaleCheckBox.IsEnabled = false;
        }

        private void Female_Checked(object sender, RoutedEventArgs e)
        {
            MaleCheckBox.IsEnabled = false;
        }

        private void MarriedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            NotMarriedCheckBox.IsEnabled = false;
        }

        private void NotMarriedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MarriedCheckBox.IsEnabled = false;
        }

        private void MarriedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            NotMarriedCheckBox.IsEnabled = true;
        }

        private void NotMarriedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MarriedCheckBox.IsEnabled = true;
        }

        private void Male_Unchecked(object sender, RoutedEventArgs e)
        {
            FemaleCheckBox.IsEnabled = true;
        }

        private void Female_Unchecked(object sender, RoutedEventArgs e)
        {
            MaleCheckBox.IsEnabled = true;
        }
    }
}
