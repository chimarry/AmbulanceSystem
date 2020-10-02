using AmbulanceSystem.Controls.IndexControl;
using AmbulanceSystem.Shared;
using AmbulanceSystem.Utils;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AmbulanceSystem.Pages.Administrator.LocalAccountCRUD
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
            IndexControl control = await IndexControl.CreateIndexControl(new IndexControlElementLocalAccount(), new DataGridControlElementLocalAccount());
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
