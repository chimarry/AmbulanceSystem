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
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IMedicalRecordService medicalRecordService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalRecordService();
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();
        private readonly MedicalRecordViewModel medicalRecordViewModel;

        public OperationStatus OperationStatus { get; private set; }

        private EditModalWindow(MedicalRecordViewModel medicalRecordViewModel)
        {
            this.medicalRecordViewModel = medicalRecordViewModel;
            InitializeComponent();
            SwitchLanguage();
            ChangeTheme();
        }

        public static async Task<EditModalWindow> Create(MedicalRecordViewModel model)
        {
            EditModalWindow modal = new EditModalWindow(model);
            await modal.InitializeComboBox();
            await modal.InitializeData();
            return modal;
        }

        public void SwitchLanguage()
        {
            FirstNameLabel.Content = language.Name.Mandatory();
            LastNameLabel.Content = language.LastName.Mandatory();
            UNBLabel.Content = language.UBN.Mandatory();
            BirthDateLabel.Content = language.BirthDate.Mandatory();
            BirthPlaceLabel.Content = language.BirthPlace.Mandatory();
            GenderLabel.Content = language.Gender.Mandatory();
            MarriageStatusLabel.Content = language.MarriageStatus.Mandatory();
            MothersNameLabel.Content = language.MothersFullName.Mandatory();
            MothersProfessionLabel.Content = language.MothersProfession.Mandatory();
            FathersNameLabel.Content = language.FathersFullName.Mandatory();
            FathersProfessionLabel.Content = language.FathersProfession.Mandatory();
            InsuranceNumberLabel.Content = language.InsuranceNumber.Mandatory();
            ResidancePlaceTextBlock.Text = language.ResidenceName.Mandatory();
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        private async Task InitializeData()
        {
            try
            {
                FirstNameBox.Text = medicalRecordViewModel.Name;
                LastNameBox.Text = medicalRecordViewModel.LastName;
                BirthDatePicker.SelectedDate = medicalRecordViewModel.BirthDate;
                BirthPlaceBox.Text = medicalRecordViewModel.BirthPlace;
                FathersNameBox.Text = medicalRecordViewModel.FathersFullName;
                MothersNameBox.Text = medicalRecordViewModel.MothersFullName;
                FathersProfessionBox.Text = medicalRecordViewModel.FathersProfession;
                MothersProfessionBox.Text = medicalRecordViewModel.MothersProfession;
                MaleCheckBox.IsChecked = medicalRecordViewModel.Gender == 0 ? true : false;
                FemaleCheckBox.IsChecked = !MaleCheckBox.IsChecked;
                MarriedCheckBox.IsChecked = medicalRecordViewModel.MarriageStatus == 0 ? true : false;
                NotMarriedCheckBox.IsChecked = !MarriedCheckBox.IsChecked;
                UNBBox.Text = medicalRecordViewModel.Jmb;
                InsuranceNumberBox.Text = medicalRecordViewModel.InsuranceNumber;
                ResidancePlaceComboBox.SelectedItem = Mapping.Mapper.Map<PlaceViewModel>((await placeService.GetByPrimaryKey
                    (new Place() { IdPlace = medicalRecordViewModel.IdResidence })));
            }
            catch (Exception)
            {
                OperationStatus = new OperationStatus(language.DatabaseError, AlertType.Error);
                Close();
            }
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
                    IdMedicalRecord = medicalRecordViewModel.IdMedicalRecord,
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
                DbStatus status = await medicalRecordService.Update(medicalRecord);
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
