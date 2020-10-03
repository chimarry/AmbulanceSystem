using AmbulanceDatabase;
using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbulanceSystem.Pages.Administrator.DoctorCRUD
{
    public class IndexControlElementDoctor : IndexControlElement
    {
        private readonly IDoctorService doctorService = ServicesAmbulanceFactory.GetInstance().CreateIDoctorService();

        public override async void Create()
        {
            CreateModalWindow modalCreate = await CreateModalWindow.Create();
            _ = modalCreate.ShowDialog();
            DataGridControl.InformAboutStatus(modalCreate.OperationStatus);
        }

        public async override void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            Doctor doctor = Mapping.Mapper.Map<Doctor>(item);

            doctor = await doctorService.GetByPrimaryKey(doctor);

            //DeleteModalWindow deleteModal = new DeleteModalWindow(Mapping.Mapper.Map<DoctorViewModel>(doctor));
            //_ = deleteModal.ShowDialog();
            //  DataGridControl.InformAboutStatus(modalCreate.OperationStatus);
        }

        public async override void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            Doctor doctor = Mapping.Mapper.Map<Doctor>(item);

            doctor = await doctorService.GetByPrimaryKey(doctor);

            EditModalWindow editModal = await EditModalWindow.Create(Mapping.Mapper.Map<DoctorViewModel>(doctor));
            _ = editModal.ShowDialog();
            DataGridControl.InformAboutStatus(editModal.OperationStatus);
        }

        public async override void Details(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            Doctor doctor = Mapping.Mapper.Map<Doctor>(item);

            doctor = await doctorService.GetByPrimaryKey(doctor);

            DetailsModalWindow modal = await DetailsModalWindow.Create(Mapping.Mapper.Map<DoctorViewModel>(doctor));
            _ = modal.ShowDialog();
            DataGridControl.InformAboutStatus(modal.OperationStatus);
        }
    }
}
