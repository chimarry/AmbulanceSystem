using AmbulanceSystem.Controls.DataGridControls;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Shared.Config;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AmbulanceSystem.Controls.IndexControl
{
    /// <summary>
    /// Interaction logic for IndexControl.xaml
    /// </summary>
    ///
    public partial class IndexControl : UserControl
    {
        private readonly Visibility detailsBtnVisibility;

        private readonly Visibility crudBtnVisibility;

        public IndexControlElement IndexControlElement { get; set; }

        public DataGridControl DataGridControl { get; set; }


        private IndexControl(IndexControlElement indexControlElement,
            Visibility detailsBtnVisibility = Visibility.Hidden, Visibility crudBtnVisibility = Visibility.Visible)
        {
            InitializeComponent();
            IndexControlElement = indexControlElement;
            this.detailsBtnVisibility = detailsBtnVisibility;
            this.crudBtnVisibility = crudBtnVisibility;
        }

        public static async Task<IndexControl> CreateIndexControl(IndexControlElement indexControlElement, DataGridControlElement dataGridControlElement,
            Visibility detailsBtnVisibility = Visibility.Hidden, Visibility crudBtnVisibility = Visibility.Visible)
        {
            IndexControl indexControl = new IndexControl(indexControlElement, detailsBtnVisibility, crudBtnVisibility)
            {
                DataGridControl = await DataGridControl.CreateDataGridControl(dataGridControlElement)
            };
            indexControl.DataContext = indexControl.DataGridControl.DataGrid;
            indexControl.InitializeComponents();
            return indexControl;
        }

        private void InitializeComponents()
        {
            InitializeDataGrid();
            InitializeButtons();
        }

        private void InitializeDataGrid()
        {
            IndexGrid.Children.Add(DataGridControl);
            Grid.SetRow(DataGridControl, 0);
            Grid.SetColumn(DataGridControl, 0);
            Grid.SetColumnSpan(DataGridControl, 3);
            Grid.SetRowSpan(DataGridControl, 1);
        }

        private void InitializeButtons()
        {
            CreateButton.Content = language.Create;
            DeleteButton.Content = language.Delete;
            EditButton.Content = language.Edit;
            DetailsButton.Content = language.Details;
            SetButtonsVisibiltiy();
        }

        private void SetButtonsVisibiltiy()
        {
            DetailsButton.Visibility = detailsBtnVisibility;
            EditButton.Visibility = CreateButton.Visibility = DeleteButton.Visibility = crudBtnVisibility;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Create();
            DataGridControl.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Delete(DataGridControl.DataGrid.SelectedItem);
            DataGridControl.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Edit(DataGridControl.DataGrid.SelectedItem);
            DataGridControl.Refresh();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            IndexControlElement.Details(DataGridControl.DataGrid.SelectedItem);
            DataGridControl.Refresh();
        }
    }
}
