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
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    /// <summary>
    /// Interaction logic for CreateModalWindow.xaml
    /// </summary>
    public partial class CreateModalWindow : Window, ILanguageLocalizable, IThemeChangeable
    {
        private readonly IDoctorService doctorService = ServicesAmbulanceFactory.GetInstance().CreateIDoctorService();
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();
        private readonly IClinicService clinicService = ServicesAmbulanceFactory.GetInstance().CreateIClinicService();
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();

        private readonly DoctorViewModel doctor = new DoctorViewModel();
        private DoctorClinicAndMedicalTitleUtil doctorUtil;

        public OperationStatus OperationStatus { get; private set; }

        private CreateModalWindow()
        {
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
        }

        public static async Task<CreateModalWindow> Create()
        {
            CreateModalWindow modal = new CreateModalWindow();
            await modal.InitalizeData();
            modal.doctorUtil = new DoctorClinicAndMedicalTitleUtil(modal.ClinicsListBox, modal.TitlesListBox, modal.MedicalTitleComboBox, modal.ClinicComboBox, modal.doctor)
            {
                CurrentDictionary = modal.CurrentDictionary.MergedDictionaries[0]
            };
            return modal;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
            FirstNameLabel.Content = language.Name;
            LastNameLabel.Content = language.LastName;
            AccountTextBlock.Text = language.Account;
            WorkExperienceLabel.Content = language.WorkExperience;
            AddedClinicsTextBlock.Text = language.WorkClinics;
            AddedTitlesTextBlock.Text = language.CurrentTitles;
            ClinicTextBlock.Text = language.Clinic;
            MedicalTitleTextBlock.Text = language.MedicalTitle;
            GettingTitleLabel.Content = language.GettingTitleDate;
            SinceLabel.Content = language.Since;
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
                    x.Format = "{0}, {1}";
                    ClinicComboBox.Items.Add(x);
                });
            (await medicalTitleService.GetAll()).Select(x => Mapping.Mapper.Map<MedicalTitleViewModel>(x))?.ToList()?.
                ForEach(x => MedicalTitleComboBox.Items.Add(x));
            (await localAccountService.GetAllWithRoleNames()).Select(x => Mapping.Mapper.Map<LocalAccountViewModel>(x))?.ToList()?.
                ForEach(x => AccountComboBox.Items.Add(x));
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidForm())
            {
                doctor.Name = FirstNameBox.Text;
                doctor.LastName = LastNameBox.Text;
                doctor.IdLocalAccount = ((LocalAccountViewModel)AccountComboBox.SelectedItem).IdLocalAccount;
                doctor.WorkExperience = int.Parse(WorkExperienceBox.Text);
                DbStatus status = await doctorService.Add(Mapping.Mapper.Map<Doctor>(doctor));
                OperationStatus = StatusHandler.Handle(OperationType.Create, status);
                Close();
            }
            else FieldValidation.WriteMessage(ErrorLabel, language.SelectValues);
        }

        private bool IsValidForm()
        {
            return true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !FieldValidation.IsValidChar(e.Text);
        }

        private void WorkExperienceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void AddClinicButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.WorkInClinic(SinceDatePicker, UntilDatePicker, DeleteClinicButton_Click);
        }

        private void AddTitleButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.HasMedicalTitle(GettingTitleDatePicker, DeleteMedicalTitleButton_Click);
        }

        private void DeleteMedicalTitleButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.DeleteMedicalTitle(e, medicalTitleService);
        }

        private void DeleteClinicButton_Click(object sender, RoutedEventArgs e)
        {
            doctorUtil.DeleteClinic(e);
        }
    }
}
