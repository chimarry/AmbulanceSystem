using System;
using System.Windows.Media.Imaging;

namespace AmbulanceSystem.Shared
{
    /// <summary>
    /// Enumerates currently supported themes.
    /// </summary>
    public enum Theme
    {
        Gryffindor,
        Hufflepuf,
        Slytherin,
        Revenclaw
    }

    public static class ThemeExtensionMethods
    {
        private const string themePrefix = "pack://application:,,,/Shared/Themes/";
        private const string imagePrefix = "pack://application:,,,/Shared/Images/";

        private const string defaultTheme = themePrefix + "Gryffindor.xaml";
        private const string defaultImage = imagePrefix + "Gryffindor.png";

        public static Theme From(this string name)
            => (Theme)Enum.Parse(typeof(Theme), name);

        public static Uri ToUri(this Theme theme)
        {
            switch (theme)
            {
                case Theme.Gryffindor: return new Uri(themePrefix + "Gryffindor.xaml");
                case Theme.Slytherin: return new Uri(themePrefix + "Slytherin.xaml");
                case Theme.Hufflepuf: return new Uri(themePrefix + "Hufflepuf.xaml");
                case Theme.Revenclaw: return new Uri(themePrefix + "Revenclaw.xaml");
                default:
                    return new Uri(defaultTheme);
            }
        }

        public static BitmapImage ToImage(this Theme theme)
        {
            switch (theme)
            {
                case Theme.Gryffindor: return new BitmapImage(new Uri(imagePrefix + "Gryffindor.png"));
                case Theme.Slytherin: return new BitmapImage(new Uri(imagePrefix + "Slytherin.png"));
                case Theme.Hufflepuf: return new BitmapImage(new Uri(imagePrefix + "Hufflepuff.png"));
                case Theme.Revenclaw: return new BitmapImage(new Uri(imagePrefix + "Revenclaw.png"));
                default: return new BitmapImage(new Uri(defaultImage));
            }
        }
    }
}
