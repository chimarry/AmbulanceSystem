using AmbulanceDatabase;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Controls.DataGridControls;
using AmbulanceSystem.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.MedicalTitleCRUD
{
    public class DataGridControlElementMedicalTitle : DataGridControlElement
    {
        private readonly IMedicalTitleService medicalTitleService = ServicesAmbulanceFactory.GetInstance().CreateIMedicalTitleService();

        public DataGridControlElementMedicalTitle() : base()
        {
        }

        public override async Task<int> GetNumberOfItems() => await medicalTitleService.GetTotalNumberOfItems();

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        protected async override Task<IList> GetData(int index, int number)
        {
            List<MedicalTitle> titles = await medicalTitleService.GetRange(index, number) as List<MedicalTitle>;
            return titles.Select(x => AutoMapper.Mapping.Mapper.Map<MedicalTitleViewModel>(x)).ToList();
        }

        protected override Type GetDataType() => typeof(MedicalTitleViewModel);
    }
}
