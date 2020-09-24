using System.Windows.Controls;

namespace VirtualDoctor.Shared.Controls
{
    /// <summary>
    /// Interaction logic for ThemeSelectorControl.xaml
    /// </summary>
    public partial class ThemeSelectorControl : UserControl
    {
        public ThemeSelectorControl()
        {
            InitializeComponent();
        }

        public static void InitializeAssistent(ThemeSelectorControl control, int count, double height, string name)
        {
            //control.AssistantContainter.Height = control.AssistantContainter.Width = (height / count) * Shared.Config.Properties.Default.ButtonProportion;
            //control.AssistantBorder.Height = control.AssistantContainter.Height * Config.Properties.Default.ButtonProportion;
            //control.AssistantLink.FontFamily = Config.Properties.Default.FontFamily;
            //control.AssistantLink.FontSize = Config.Properties.Default.ButtonFontSize;
            //control.AssistantLink.Inlines.Clear();
            //control.AssistantLink.Inlines.Add(name);
        }
    }
}
