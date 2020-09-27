namespace AmbulanceSystem.Shared
{
    /// <summary>
    /// Defines type that is capable for changing design theme.
    /// </summary>
    public interface IThemeChangeable
    {
        /// <summary>
        /// Switches between current and the new theme.
        /// </summary>
        void ChangeTheme();
    }
}
