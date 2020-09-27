using AmbulanceSystem.Shared.Themes;

namespace AmbulanceSystem.Shared.Themes
{
    /// <summary>
    /// Defines type that is capable for changing design theme.
    /// </summary>
    public interface IThemeChangeable
    {
        /// <summary>
        /// Switches between current and the new theme.
        /// </summary>
        /// <param name="newTheme">New theme.</param>
        void ChangeThemeTo(Theme newTheme);
    }
}
