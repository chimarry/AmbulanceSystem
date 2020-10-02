using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.MedicalTitleCRUD
{
    public class IndexControlElementMedicalTitle : IndexControlElement
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
            MedicalTitleViewModel title = Mapping.Mapper.Map<MedicalTitleViewModel>(item);

            DeleteModalWindow deleteModal = new DeleteModalWindow(title);
            _ = deleteModal.ShowDialog();
            DataGridControl.InformAboutStatus(deleteModal.OperationStatus);
        }

        public override void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            MedicalTitleViewModel title = Mapping.Mapper.Map<MedicalTitleViewModel>(item);
            EditModalWindow modal = new EditModalWindow(title);
            _ = modal.ShowDialog();
            DataGridControl.InformAboutStatus(modal.OperationStatus);
        }
    }
}
