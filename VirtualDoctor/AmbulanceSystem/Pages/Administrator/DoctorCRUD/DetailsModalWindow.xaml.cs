using AmbulanceDatabase;
using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.Shared.OperationStatusHandling;
using AmbulanceSystem.Utils;
using AmbulanceSystem.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    /// <summary>
    /// Interaction logic for DetailsModalWindow.xaml
    /// </summary>
    public partial class DetailsModalWindow : Window, IThemeChangeable, ILanguageLocalizable
    {
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();

        private readonly DoctorViewModel doctor;
        private DoctorClinicAndMedicalTitleUtil doctorUtil;

        public OperationStatus OperationStatus { get; private set; }

        private DetailsModalWindow(DoctorViewModel doctor)
        {
            this.doctor = doctor;
            InitializeComponent();
            ChangeTheme();
            SwitchLanguage();
        }

        public static async Task<DetailsModalWindow> Create(DoctorViewModel doctor)
        {
            DetailsModalWindow modal = new DetailsModalWindow(doctor);
            modal.doctorUtil = new DoctorClinicAndMedicalTitleUtil(modal.ClinicsListBox, modal.TitlesListBox, modal.doctor)
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
            FirstNameLabel.Content = language.Name;
            LastNameLabel.Content = language.LastName;
            AccountTextBlock.Text = language.Account;
            WorkExperienceLabel.Content = language.WorkExperience;
            AddedClinicsTextBlock.Text = language.WorkClinics;
            AddedTitlesTextBlock.Text = language.CurrentTitles;
        }

        private async Task SetExistingData()
        {
            FirstNameBox.Text = doctor.Name;
            LastNameBox.Text = doctor.LastName;
            WorkExperienceBox.Text = doctor.WorkExperience.ToString();
            LocalAccountViewModel account = Mapping.Mapper.Map<LocalAccountViewModel>(
                await localAccountService.GetByPrimaryKey(new LocalAccount()
                {
                    IdLocalAccount = doctor.IdLocalAccount
                }));

            AccountComboBox.Items.Add(account);
            AccountComboBox.SelectedItem = account;

            foreach (DoctorClinic doctorClinic in doctor.GetClinics())
                doctorUtil.PutClinicInList(Mapping.Mapper.Map<DoctorClinicViewModel>(doctorClinic));

            foreach (DoctorMedicalTitle mt in doctor.GetMedicalTitles())
            {
                DoctorMedicalTitleViewModel model = Mapping.Mapper.Map<DoctorMedicalTitleViewModel>(mt);
                model.SetMedicalTitleName();
                doctorUtil.PutMedicalTitleInList(model);
            }
        }

    }
}
