using AmbulanceSystem.Shared;

namespace AmbulanceSystem.Utils
{
    public static class ThemeChanger
    {
        public static void SetAsCurrentTheme(Theme theme)
        {
            Shared.Config.Properties.Default.Theme = (int)theme;
        }

        public static Theme GetCurrentTheme()
          => (Theme)Shared.Config.Properties.Default.Theme;
    }
}
