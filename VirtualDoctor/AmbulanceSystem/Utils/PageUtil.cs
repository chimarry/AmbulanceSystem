using AmbulanceSystem.Login;
using AmbulanceSystem.Shared;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AmbulanceSystem.Utils
{
    public static class PageUtil
    {
        private static readonly string imagePrefix = Shared.Config.Properties.Default.ButtonIconUriPrefix;
        private static readonly Dictionary<ButtonIcon, BitmapImage> cachedImages;

        static PageUtil()
        {
            cachedImages = new Dictionary<ButtonIcon, BitmapImage>();
            foreach (int iconIndex in Enum.GetValues(typeof(ButtonIcon)))
                cachedImages.Add((ButtonIcon)iconIndex, GetByName((ButtonIcon)iconIndex));
        }

        public static void NavigateToNextPage(this Window window, Page page)
        {
            (window as MainWindow).MainFrame.Content = page;
        }

        public static void SetIconAndText(this Button button, string text, ButtonIcon icon)
        {
            StackPanel stackPanel = VisualTreeHelper.GetChild(button.Template.FindName("border", button) as DependencyObject, 0) as StackPanel;
            Image image = stackPanel.Children[0] as Image;
            image.Source = cachedImages[icon];
            TextBlock textBlock = stackPanel.Children[1] as TextBlock;
            textBlock.Text = text;
        }

        private static BitmapImage GetByName(ButtonIcon icon)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imagePrefix + icon.MapString());
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}