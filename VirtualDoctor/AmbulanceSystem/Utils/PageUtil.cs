using AmbulanceSystem.Login;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AmbulanceSystem.Utils
{
    public static class PageUtil
    {
        private const string imagePrefix = "pack://application:,,,/Shared/Images/Buttons/";

        public static void NavigateToNextPage(this Window window, Page page)
        {
            (window as MainWindow).MainFrame.Content = page;
        }

        public static void SetIconAndText(this Button button, string text, string imageName)
        {
            try
            {
                StackPanel stackPanel = VisualTreeHelper.GetChild(button.Template.FindName("border", button) as DependencyObject, 0) as StackPanel;
                Image image = stackPanel.Children[0] as Image;
                image.Source = new BitmapImage(new Uri(imagePrefix + imageName));
                TextBlock textBlock = stackPanel.Children[1] as TextBlock;
                textBlock.Text = text;
            }
            catch (Exception)
            {

            }
        }
    }
}
