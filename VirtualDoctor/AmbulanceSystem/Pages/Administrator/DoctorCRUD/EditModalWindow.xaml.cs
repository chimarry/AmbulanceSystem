using AmbulanceDatabase;
using AmbulanceDatabase.Context;
using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    /// <summary>
    /// Interaction logic for EditModalWindow.xaml
    /// </summary>
    public partial class EditModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly IDoctorService doctorService = ServicesAmbulanceFactory.GetInstance().CreateIDoctorService();
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();
        private readonly IClinicService clinicService = ServicesAmbulanceFactory.GetInstance().CreateIClinicService();
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();

        private readonly DoctorViewModel doctor;
        private DoctorClinicAndMedicalTitleUtil doctorUtil;

        public OperationStatus OperationStatus { get; private set; }

        private EditModalWindow(DoctorViewModel doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
        }

        public static async Task<EditModalWindow> Create(DoctorViewModel doctor)
        {
            EditModalWindow modal = new EditModalWindow(doctor);
            await modal.InitalizeData();
            modal.doctorUtil = new DoctorClinicAndMedicalTitleUtil(modal.ClinicsListBox, modal.TitlesListBox, modal.MedicalTitleComboBox, modal.ClinicComboBox, modal.doctor)
            {
                CurrentDictionary = modal.CurrentDictionary.MergedDictionaries[0]
            };
            await modal.SetExistingData();
            return modal;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            FirstNameLabel.Content = language.Name.Mandatory();
            LastNameLabel.Content = language.LastName.Mandatory();
            AccountTextBlock.Text = language.Account.Mandatory();
            WorkExperienceLabel.Content = language.WorkExperience.Mandatory();
            AddedClinicsTextBlock.Text = language.WorkClinics;
            AddedTitlesTextBlock.Text = language.CurrentTitles;
            ClinicTextBlock.Text = language.Clinic.Mandatory();
            MedicalTitleTextBlock.Text = language.MedicalTitle.Mandatory();
            GettingTitleLabel.Content = language.GettingTitleDate.Mandatory();
            SinceLabel.Content = language.Since.Mandatory();
            UntilLabel.Content = language.Until;
            SaveButton.Content = language.Save;
        }

        private async Task InitalizeData()
        {
            (await clinicService.GetAll()).Select(x => Mapping.Mapper.Map<ClinicViewModel>(x))?.ToList()?.
                ForEach(async x =>
                {
                    Place place = await placeService.GetByPrimaryKey(new Place() { IdPlace = x.IdPlace });
                    place.SetFormat("{0}, {1}");
                    x.Place = place.ToString();
                    x.SetFormat("{0}, {1}");
                    ClinicComboBox.Items.Add(x);
                });
            (await medicalTitleService.GetAll()).Select(x => Mapping.Mapper.Map<MedicalTitleViewModel>(x))?.ToList()?.
                ForEach(x => MedicalTitleComboBox.Items.Add(x));
            (await localAccountService.GetAllWithRoleNames()).Select(x => Mapping.Mapper.Map<LocalAccountViewModel>(x))?.ToList()?.
                ForEach(x => AccountComboBox.Items.Add(x));
        }

        private async Task SetExistingData()
        {
            FirstNameBox.Text = doctor.Name;
            LastNameBox.Text = doctor.LastName;
            WorkExperienceBox.Text = doctor.WorkExperience.ToString();
            AccountComboBox.SelectedItem = Mapping.Mapper.Map<LocalAccountViewModel>(await localAccountService.GetByPrimaryKey(new LocalAccount() { IdLocalAccount = doctor.IdLocalAccount }));

            foreach (DoctorClinic doctorClinic in doctor.GetClinics())
                doctorUtil.PutClinicInList(Mapping.Mapper.Map<DoctorClinicViewModel>(doctorClinic), DeleteClinicButton_Click);

            foreach (DoctorMedicalTitle mt in doctor.GetMedicalTitles())
            {
                DoctorMedicalTitleViewModel model = Mapping.Mapper.Map<DoctorMedicalTitleViewModel>(mt);
                model.SetMedicalTitleName();
                doctorUtil.PutMedicalTitleInList(model, DeleteMedicalTitleButton_Click);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForm())
            {
                doctor.Name = FirstNameBox.Text;
                doctor.LastName = LastNameBox.Text;
                doctor.IdLocalAccount = ((LocalAccountViewModel)AccountComboBox.SelectedItem).IdLocalAccount;
                doctor.WorkExperience = int.Parse(WorkExperienceBox.Text);
                DbStatus status = await doctorService.Update(Mapping.Mapper.Map<Doctor>(doctor));
                OperationStatus = StatusHandler.Handle(OperationType.Edit, status);
                Close();
            }
            else FieldValidation.WriteMessage(ErrorLabel, language.SelectValues);
        }

        private bool IsValidForm()
        {
            bool areFilled = FieldValidation.NotNullOrEmpty(FirstNameBox.Text, LastNameBox.Text, WorkExperienceBox.Text);
            bool isSelected = FieldValidation.IsSelected(AccountComboBox.SelectedIndex);
            return areFilled && isSelected;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !FieldValidation.IsValidChar(e.Text);
        }

        private void WorkExperienceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !FieldValidation.IsNumber(e.Text);
        }

        private void AddClinicButton_Click(object sender, RoutedEventArgs e)
        {
            if (SinceDatePicker.SelectedDate.HasValue && FieldValidation.AreSelected(ClinicComboBox.SelectedIndex))
            {
                ErrorLabel.Content = language.WorkPlaceError.Equals(ErrorLabel.Content) ? string.Empty : ErrorLabel.Content;
                doctorUtil.WorkInClinic(SinceDatePicker, UntilDatePicker, DeleteClinicButton_Click);
            }
            else FieldValidation.WriteMessage(ErrorLabel, language.WorkPlaceError);
        }

        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            if (GettingTitleDatePicker.SelectedDate.HasValue && FieldValidation.AreSelected(MedicalTitleComboBox.SelectedIndex))
            {
                ErrorLabel.Content = language.MedicalTitleError.Equals(ErrorLabel.Content) ? string.Empty : ErrorLabel.Content;
                doctorUtil.HasMedicalTitle(GettingTitleDatePicker, DeleteMedicalTitleButton_Click);
            }
            else FieldValidation.WriteMessage(ErrorLabel, language.MedicalTitleError);
        }

        private void DeleteMedicalTitleButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.DeleteMedicalTitle(e, medicalTitleService);
        }

        private void DeleteClinicButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.DeleteClinic(e);
        }

        private void SinceDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UntilDatePicker.DisplayDateStart = SinceDatePicker.SelectedDate?.AddDays(1);
        }
    }
}
