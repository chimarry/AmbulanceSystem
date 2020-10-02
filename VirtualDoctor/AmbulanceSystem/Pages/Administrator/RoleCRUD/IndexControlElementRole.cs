using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.RoleCRUD
{
    public class IndexControlElementRole : IndexControlElement
    {
        public override void Create()
        {
            CreateModalWindow modalCreate = new CreateModalWindow();
            _ = modalCreate.ShowDialog();
            DataGridControl.InformAboutStatus(modalCreate.OperationStatus);
        }

        public override void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            RoleViewModel role = Mapping.Mapper.Map<RoleViewModel>(item);

            DeleteModalWindow deleteModal = new DeleteModalWindow(role);
            _ = deleteModal.ShowDialog();
            DataGridControl.InformAboutStatus(deleteModal.OperationStatus);
        }

        public override void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            RoleViewModel role = Mapping.Mapper.Map<RoleViewModel>(item);

            EditModalWindow modal = new EditModalWindow(role);
            _ = modal.ShowDialog();
            DataGridControl.InformAboutStatus(modal.OperationStatus);
        }
    }
}
