using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.Controls.DataGridControls;
using AmbulanceSystem.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmbulanceSystem.Pages.Administrator.RoleCRUD
{
    public class DataGridControlElementRole : DataGridControlElement
    {
        private readonly IRoleService roleService = ServicesAmbulanceFactory.GetInstance().CreateIRoleService();

        public DataGridControlElementRole() : base()
        {
        }

        public override void HideColumns()
        {
            foreach (var column in DataGrid.Columns)
                if (column.SortMemberPath.StartsWith("Id"))
                    column.Visibility = Visibility.Hidden;
        }

        protected override async Task<IList> GetData(int index, int number)
        {
            List<Role> list = await roleService.GetRange(index, number) as List<Role>;
            return list.Select(x => AutoMapper.Mapping.Mapper.Map<RoleViewModel>(x)).ToList();
        }

        protected override Type GetDataType()
            => typeof(RoleViewModel);

        public override async Task<int> GetNumberOfItems()
            => await roleService.GetTotalNumberOfItems();
    }
}
