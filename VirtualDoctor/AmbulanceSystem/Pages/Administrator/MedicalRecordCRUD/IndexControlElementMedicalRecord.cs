using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.MedicalRecordCRUD
{
    public class IndexControlElementMedicalRecord : IndexControlElement
    {
        public override async void Create()
        {
            CreateModalWindow modalWindow = await CreateModalWindow.Create();
            _ = modalWindow.ShowDialog();
            DataGridControl.InformAboutStatus(modalWindow.OperationStatus);
        }

        public override void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            MedicalRecordViewModel model = Mapping.Mapper.Map<MedicalRecordViewModel>(item);

            DeleteModalWindow modalWindow = new DeleteModalWindow(model);
            _ = modalWindow.ShowDialog();
            DataGridControl.InformAboutStatus(modalWindow.OperationStatus);
        }

        public override async void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            MedicalRecordViewModel model = Mapping.Mapper.Map<MedicalRecordViewModel>(item);

            EditModalWindow modalWindow = await EditModalWindow.Create(model);
            _ = modalWindow.ShowDialog();
            DataGridControl.InformAboutStatus(modalWindow.OperationStatus);
        }
    }
}

