using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Utils;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AmbulanceSystem.Pages.Administrator.PlaceCRUD
{
    /// <summary>
    /// Interaction logic for IndexPage.xaml
    /// </summary>
    public partial class IndexPage : Page, IThemeChangeable, ILanguageLocalizable
    {
        private IndexPage()
        {
            InitializeComponent();
        }

        public static async Task<IndexPage> CreateIndexPage()
        {
            IndexPage page = new IndexPage();
            IndexControl control = await IndexControl.CreateIndexControl(new IndexControlElementPlace(), new DataGridControlElementPlace());
            page.IndexPageGrid.Children.Add(control);
            return page;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
        }

        public void SwitchLanguage()
        {
        }
    }
}
