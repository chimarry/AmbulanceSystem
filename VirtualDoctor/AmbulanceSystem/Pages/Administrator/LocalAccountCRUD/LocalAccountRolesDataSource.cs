using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AmbulanceSystem.Pages.Administrator.LocalAccountCRUD
{
    public class LocalAccountRolesDataSource : ComboBoxDataSource<RoleViewModel>, INotifyPropertyChanged
    {
        private readonly IRoleService roleService = ServicesAmbulanceFactory.GetInstance().CreateIRoleService();
        private readonly ILocalAccountRoleService localAccountRoleService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountRoleService();

        public LocalAccountRolesDataSource(int? idLocalAccount = null) : base(idLocalAccount)
        {
        }

        protected async override void InitializeItems()
        {
            if (idCurrentModel != null)
                SelectedItems = new ObservableCollection<RoleViewModel>(
                    (await localAccountRoleService.GetAllRolesFor(idCurrentModel.Value))
                     .Select(x => Mapping.Mapper.Map<RoleViewModel>(x))
                    );

            IList<Role> avaliableRoles = await roleService.GetAll();
            var roleslist = avaliableRoles.Select(x => Mapping.Mapper.Map<RoleViewModel>(x))?.Except(SelectedItems)?.Union(SelectedItems);
            Items = new ObservableCollection<RoleViewModel>(roleslist ?? new List<RoleViewModel>());
        }
    }
}
