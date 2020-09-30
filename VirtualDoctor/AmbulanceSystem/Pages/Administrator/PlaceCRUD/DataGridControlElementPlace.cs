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
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    public class DataGridControlElementPlace : DataGridControlElement
    {
        private readonly IPlaceService placeService = ServicesAmbulanceFactory.GetInstance().CreateIPlaceService();

        public DataGridControlElementPlace() : base()
        {
        }

        public override async Task<int> GetNumberOfItems()
            => await placeService.GetTotalNumberOfItems();

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        protected async override Task<IList> GetData(int index, int number)
        {
            List<Place> list = await placeService.GetRange(index, number) as List<Place>;
            return list.Select(x => AutoMapper.Mapping.Mapper.Map<PlaceViewModel>(x)).ToList();
        }

        protected override Type GetDataType()
            => typeof(PlaceViewModel);
    }
}
