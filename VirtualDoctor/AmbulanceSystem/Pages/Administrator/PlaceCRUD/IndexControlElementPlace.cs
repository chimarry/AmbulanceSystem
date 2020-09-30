using AmbulanceSystem.AutoMapper;
using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.ViewModels;
using System.Data;

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    public class IndexControlElementPlace : IndexControlElement
    {
        public override void Create()
        {
            new CreateModalWindow().ShowDialog();
        }

        public override void Delete(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            PlaceViewModel place = Mapping.Mapper.Map<PlaceViewModel>(item);

            new DeleteModalWindow(place).ShowDialog();
        }

        public override void Edit(object selectedItem)
        {
            DataRowView item = (DataRowView)selectedItem;
            PlaceViewModel place = Mapping.Mapper.Map<PlaceViewModel>(item);
            new EditModalWindow(place).ShowDialog();
        }
    }
}
