using AmbulanceSystem.Shared.Config;
using System.Windows;

namespace AmbulanceSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(language.UnhandledException, "!", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}