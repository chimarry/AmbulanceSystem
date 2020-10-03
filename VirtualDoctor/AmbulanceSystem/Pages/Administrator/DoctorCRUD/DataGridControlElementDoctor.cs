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

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    class DataGridControlElementDoctor : DataGridControlElement
    {
        private readonly IDoctorService doctorService = ServicesAmbulanceFactory.GetInstance().CreateIDoctorService();

        public DataGridControlElementDoctor() : base()
        {
        }

        public override async Task<int> GetNumberOfItems() => await doctorService.GetTotalNumberOfItems();

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        protected async override Task<IList> GetData(int index, int number)
        {
            var list = await doctorService.GetRangeViews(index, number) as List<DoctorsView>;
            return Mapping.Mapper.Map<List<DoctorsView>, List<DoctorViewModel>>(list);
        }

        protected override Type GetDataType() => typeof(DoctorViewModel);
    }
}
