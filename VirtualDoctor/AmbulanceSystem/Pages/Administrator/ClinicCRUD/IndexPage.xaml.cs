using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Utils;
using System.Threading.Tasks;

namespace AmbulanceSystem.Pages.Administrator.ClinicCRUD
{
    /// <summary>
    /// Interaction logic for IndexPage.xaml
    /// </summary>
    public partial class IndexPage : IThemeChangeable, ILanguageLocalizable
    {
        private IndexPage()
        {
            InitializeComponent();
        }

        public static async Task<IndexPage> CreateIndexPage()
        {
            IndexPage page = new IndexPage();
            IndexControl control = await IndexControl.CreateIndexControl(new IndexControlElementClinic(), new DataGridControlElementClinic());
            page.IndexPageGrid.Children.Add(control);
            page.ChangeTheme();
            page.SwitchLanguage();
            return page;
        }

        public void ChangeTheme()
        {
            CurrentDictionary.MergedDictionaries[0].Source = ThemeChanger.GetCurrentTheme().ToUri();
            (IndexPageGrid.Children[0] as IndexControl).ChangeTheme();
        }

        public void SwitchLanguage()
        {
            (IndexPageGrid.Children[0] as IndexControl).SwitchLanguage();
        }
    }
}
