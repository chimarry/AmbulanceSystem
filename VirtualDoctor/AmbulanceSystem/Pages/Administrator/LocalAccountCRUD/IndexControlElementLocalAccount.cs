using AmbulanceDatabase.Entities;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.LocalAccountCRUD
{
    public class IndexControlElementLocalAccount : IndexControlElement
    {
        private readonly ILocalAccountRoleService localAccountRoleService = ServicesAmbulanceFactory.GetInstance().CreateILocalAccountRoleService();

        public override void Create()
        {
            CreateModalWindow modalCreate = new CreateModalWindow();
            _ = modalCreate.ShowDialog();
            DataGridControl.InformAboutStatus(modalCreate.OperationStatus);
        }

        public override async void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            LocalAccountViewModel model = Mapping.Mapper.Map<LocalAccountViewModel>(item);
            model.SetRoles(await localAccountRoleService.GetAllRolesFor(model.IdLocalAccount) as List<Role>);

            DeleteModalWindow deleteModal = new DeleteModalWindow(model);
            _ = deleteModal.ShowDialog();
            DataGridControl.InformAboutStatus(deleteModal.OperationStatus);
        }

        public override async void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            LocalAccountViewModel model = Mapping.Mapper.Map<LocalAccountViewModel>(item);
            model.SetRoles(await localAccountRoleService.GetAllRolesFor(model.IdLocalAccount) as List<Role>);

            EditModalWindow modal = new EditModalWindow(model);
            _ = modal.ShowDialog();
            DataGridControl.InformAboutStatus(modal.OperationStatus);
        }
    }
}
