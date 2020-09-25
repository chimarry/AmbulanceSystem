using AmbulanceSystem.Shared.Themes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IThemeChangeable
    {
        public Theme CurrentTheme { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            CurrentTheme = Theme.Gryffindor;
        }

        public bool ChangeThemeTo(Theme newTheme)
        {
            try
            {
                CurrentDictionary.MergedDictionaries[0].Source = newTheme.ToUri();
                MainImageBrush.ImageSource = newTheme.ToImage();
                CurrentTheme = newTheme;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clickedTheme = (MenuItem)sender;
            Theme newTheme = (Theme)Enum.Parse(typeof(Theme), clickedTheme.Header.ToString());
            bool isChanged = ChangeThemeTo(newTheme);
        }
    }
}
