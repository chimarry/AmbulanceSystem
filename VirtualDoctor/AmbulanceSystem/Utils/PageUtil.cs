using AmbulanceSystem.Login;
using System.Windows;
using System.Windows.Controls;

namespace AmbulanceSystem.Utils
{
    public static class PageUtil
    {
        public static void NavigateToNextPage(this Window window, Page page)
        {
            (window as MainWindow).MainFrame.Content = page;
        }
    }
}
