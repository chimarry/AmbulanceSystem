using System.Globalization;
using System.Threading;


namespace AmbulanceSystem.Utils
{
    /// <summary>
    /// Responsible for setting currect UI culture.
    /// </summary>
    public static class LanguageLocalizer
    {
        private static readonly string SERBIAN_LANGUAGE = "sr-SR";
        private static readonly string ENGLISH_LANGUAGE = "en-EN";

        /// <summary>
        /// Switches current language for a new one. If English is current culture,
        /// then new one will be Serbian and if Serbian is the current culture, then new one
        /// will be English.
        /// </summary>
        public static void SwitchLanguage()
        {
            if (Shared.Config.Properties.Default.Language == SERBIAN_LANGUAGE)
                Shared.Config.Properties.Default.Language = ENGLISH_LANGUAGE;

            else
                Shared.Config.Properties.Default.Language = SERBIAN_LANGUAGE;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Shared.Config.Properties.Default.Language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Shared.Config.Properties.Default.Language);
        }
    }
}
