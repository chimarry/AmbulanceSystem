using AmbulanceDatabase.Views;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.DataGridControls;
using AmbulanceSystem.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD
{
    public class DataGridControlElementMedicalRecord : DataGridControlElement
    {
        private readonly IMedicalRecordService medicalRecordService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalRecordService();

        public DataGridControlElementMedicalRecord() : base()
        {
        }

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        public override async Task<int> GetNumberOfItems() => await medicalRecordService.GetTotalNumberOfItems();

        protected async override Task<IList> GetData(int index, int number)
        {
            var list = await medicalRecordService.GetRangeViews(index, number) as List<MedicalRecordsView>;
            return Mapping.Mapper.Map<List<MedicalRecordsView>, List<MedicalRecordViewModel>>(list);
        }

        protected override Type GetDataType() => typeof(MedicalRecordViewModel);
    }
}

