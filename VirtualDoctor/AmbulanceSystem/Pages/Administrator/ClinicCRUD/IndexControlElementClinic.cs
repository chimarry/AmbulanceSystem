using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.ClinicCRUD
{
    public class IndexControlElementClinic : IndexControlElement
    {
        public override async void Create()
        {
            CreateModalWindow modalCreate = await CreateModalWindow.Create();
            _ = modalCreate.ShowDialog();
            DataGridControl.InformAboutStatus(modalCreate.OperationStatus);
        }

        public override void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            ClinicViewModel clinic = Mapping.Mapper.Map<ClinicViewModel>(item);

            DeleteModalWindow deleteModal = new DeleteModalWindow(clinic);
            _ = deleteModal.ShowDialog();
            DataGridControl.InformAboutStatus(deleteModal.OperationStatus);
        }

        public override async void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            ClinicViewModel clinic = Mapping.Mapper.Map<ClinicViewModel>(item);

            EditModalWindow modal = await EditModalWindow.Create(clinic);
            _ = modal.ShowDialog();
            DataGridControl.InformAboutStatus(modal.OperationStatus);
        }
    }
}
