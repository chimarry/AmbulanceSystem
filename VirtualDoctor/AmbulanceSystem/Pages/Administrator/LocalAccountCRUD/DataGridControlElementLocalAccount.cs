using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.DataGridControls;
using AmbulanceSystem.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.LocalAccountCRUD
{
    public class DataGridControlElementLocalAccount : DataGridControlElement
    {
        private readonly ILocalAccountService localAccountService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountService();

        public DataGridControlElementLocalAccount() : base()
        {
        }

        public override async Task<int> GetNumberOfItems() => await localAccountService.GetTotalNumberOfItems();

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        protected override async Task<IList> GetData(int index, int number)
        {
            var listAccounts = await localAccountService.GetAllWithRoleNames();
            List<LocalAccountViewModel> models = Mapping.Mapper.Map<List<LocalAccount>, List<LocalAccountViewModel>>(listAccounts.ToList());
            return models;
        }

        protected override Type GetDataType() => typeof(LocalAccountViewModel);
    }
}