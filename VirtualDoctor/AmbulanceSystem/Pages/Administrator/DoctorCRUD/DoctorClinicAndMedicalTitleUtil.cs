using AmbulanceDatabase;
using AmbulanceDatabase.Entities;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Shared.Config;
using AmbulanceSystem.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    public class DoctorClinicAndMedicalTitleUtil
    {
        private const string buttonStyle = "RoundCornerButton";
        private const string textBlockStyle = "MainTextBlockStyle";
        private readonly DoctorViewModel doctor;

        public ListBox ClinicsListBox { get; set; }
        public ListBox MedicalTitlesListBox { get; set; }
        public ComboBox MedicalTitleComboBox { get; set; }
        public ComboBox ClinicComboBox { get; set; }
        public ResourceDictionary CurrentDictionary { get; set; }

        public DoctorClinicAndMedicalTitleUtil(ListBox clinicsListBox, ListBox medicalTitlesListBox, DoctorViewModel doctor)
        {
            ClinicsListBox = clinicsListBox;
            MedicalTitlesListBox = medicalTitlesListBox;
            this.doctor = doctor;
        }

        public DoctorClinicAndMedicalTitleUtil(ListBox clinicsListBox, ListBox medicalTitlesListBox, ComboBox medicalTitleComboBox,
            ComboBox clinicComboBox, DoctorViewModel doctor)
         : this(clinicsListBox, medicalTitlesListBox, doctor)
        {
            ClinicComboBox = clinicComboBox;
            MedicalTitleComboBox = medicalTitleComboBox;
            this.doctor = doctor;
        }


        public void HasMedicalTitle(DatePicker gettingTitleDatePicker, RoutedEventHandler onDelete)
        {
            MedicalTitleViewModel medicalTitleViewModel = (MedicalTitleViewModel)MedicalTitleComboBox.SelectedItem;
            DateTime gettingTitleDate = gettingTitleDatePicker.SelectedDate.Value;
            DoctorMedicalTitleViewModel doctorMedicalTitleViewModel = new DoctorMedicalTitleViewModel()
            {
                IdMedicalTitle = medicalTitleViewModel.IdMedicalTitle,
                GettingTitleDate = gettingTitleDate,
                IdDoctor = doctor.IdDoctor
            };
            doctorMedicalTitleViewModel.SetMedicalTitleName();
            doctor.GetMedicalTitles().Add(Mapping.Mapper.Map<DoctorMedicalTitle>(doctorMedicalTitleViewModel));
            PutMedicalTitleInList(doctorMedicalTitleViewModel, onDelete);
            MedicalTitleComboBox.Items.Remove(medicalTitleViewModel);
            gettingTitleDatePicker.SelectedDate = null;
        }
        public void WorkInClinic(DatePicker sinceDatePicker, DatePicker untilDatePicker, RoutedEventHandler onDelete)
        {

            ClinicViewModel clinicViewModel = ClinicComboBox.SelectedItem as ClinicViewModel;
            DateTime since = sinceDatePicker.SelectedDate.Value;
            DateTime? until = untilDatePicker.SelectedDate != null ? untilDatePicker.SelectedDate : null;
            DoctorClinicViewModel doctorClinicViewModel = new DoctorClinicViewModel()
            {
                IdClinic = clinicViewModel.IdClinic,
                Since = since,
                UntilDate = until,
                IdDoctor = doctor.IdDoctor

            };
            doctor.GetClinics().Add(Mapping.Mapper.Map<DoctorClinic>(doctorClinicViewModel));
            PutClinicInList(doctorClinicViewModel, onDelete);
            untilDatePicker.SelectedDate = null;
            sinceDatePicker.SelectedDate = null;
            ClinicComboBox.SelectedItem = null;
        }

        public void DeleteClinic(RoutedEventArgs e)
        {
            StackPanel item = (e.OriginalSource as Button).Parent as StackPanel;
            string sinceDate = (item.Children[2] as TextBlock).Text.Substring(language.Since.Count() + 2);
            DoctorClinicViewModel doctorClinicViewModel = new DoctorClinicViewModel()
            {
                IdClinic = int.Parse((item.Children[0] as TextBlock).Text),
                Since = DateTime.Parse(sinceDate)

            };
            doctor.GetClinics().Remove(doctor.GetClinics().Where(x => x.IdClinic == doctorClinicViewModel.IdClinic && x.Since == doctorClinicViewModel.Since).Single());
            ClinicsListBox.Items.Remove(item);
            ClinicsListBox.Items.Refresh();
        }

        public async void DeleteMedicalTitle(RoutedEventArgs e, IMedicalTitleService medicalTitleService)
        {
            StackPanel item = (e.OriginalSource as Button).Parent as StackPanel;

            DoctorMedicalTitle medicalTitle = new DoctorMedicalTitle()
            {
                IdMedicalTitle = int.Parse((item.Children[0] as TextBlock).Text),

            };
            doctor.GetMedicalTitles().Remove(doctor.GetMedicalTitles().Where(x => x.IdMedicalTitle == medicalTitle.IdMedicalTitle).Single());
            MedicalTitle titleToAdd = await medicalTitleService.GetByPrimaryKey(new MedicalTitle() { IdMedicalTitle = medicalTitle.IdMedicalTitle });
            MedicalTitleComboBox.Items.Add(Mapping.Mapper.Map<MedicalTitleViewModel>(titleToAdd));

            MedicalTitlesListBox.Items.Remove(item);
            MedicalTitlesListBox.Items.Refresh();
        }

        public void PutClinicInList(DoctorClinicViewModel doctorClinicViewModel, RoutedEventHandler onDelete = null)
        {
            StackPanel stackPanel = new StackPanel
            {
                CanVerticallyScroll = true,
                CanHorizontallyScroll = true
            };
            stackPanel.Children.Add(new TextBlock()
            {
                Visibility = Visibility.Hidden,
                Text = doctorClinicViewModel.IdClinic.ToString()
            });
            stackPanel.Children.Add(new TextBlock()
            {
                Text = language.Name + " : " + doctorClinicViewModel.ClinicsName,
                Style = CurrentDictionary[textBlockStyle] as Style
            });
            stackPanel.Children.Add(new TextBlock()
            {
                Text = language.Since + " : " + doctorClinicViewModel.Since,
                Style = CurrentDictionary[textBlockStyle] as Style
            });
            stackPanel.Children.Add(new TextBlock()
            {
                Text = language.Until + " : " + doctorClinicViewModel.UntilDate,
                Style = CurrentDictionary[textBlockStyle] as Style
            });
            if (onDelete != null)
            {
                Button deleteButton = new Button()
                {
                    Content = language.Delete,
                    Style = CurrentDictionary[buttonStyle] as Style,
                    MaxWidth = 100,
                    Width = 80
                };
                deleteButton.Click += onDelete;
                stackPanel.Children.Add(deleteButton);
            }
            ClinicsListBox.Items.Add(stackPanel);
            ClinicsListBox.Items.Refresh();
        }

        public void PutMedicalTitleInList(DoctorMedicalTitleViewModel doctorMedicalTitleViewModel, RoutedEventHandler onDelete = null)
        {
            StackPanel stackPanel = new StackPanel
            {
                CanVerticallyScroll = true,
                CanHorizontallyScroll = true
            };
            stackPanel.Children.Add(new TextBlock()
            {
                Visibility = Visibility.Hidden,
                Text = doctorMedicalTitleViewModel.IdMedicalTitle.ToString()
            });
            stackPanel.Children.Add(new TextBlock()
            {
                Text = language.Name + " : " + doctorMedicalTitleViewModel.MedicalTitleName,
                Style = CurrentDictionary[textBlockStyle] as Style
            });
            stackPanel.Children.Add(new TextBlock()
            {
                Text = language.GettingTitleDate + " : " + doctorMedicalTitleViewModel.GettingTitleDate,
                Style = CurrentDictionary[textBlockStyle] as Style
            });
            if (onDelete != null)
            {
                Button deleteButton = new Button()
                {
                    Content = language.Delete,
                    Style = CurrentDictionary[buttonStyle] as Style,
                    MaxWidth = 100,
                    Width = 80
                };

                deleteButton.Click += onDelete;
                stackPanel.Children.Add(deleteButton);
            }
            MedicalTitlesListBox.Items.Add(stackPanel);
            MedicalTitlesListBox.Items.Refresh();
        }
    }
}