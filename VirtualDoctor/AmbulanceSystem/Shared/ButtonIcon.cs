namespace AmbulanceSystem.Shared
{
    public enum ButtonIcon
    {
        Accounts, Roles, Place, Doctor, MedicalRecord, MedicalTitle, Ambulance
    }

    public static class ButtonIconExtensionMethods
    {
        public static string MapString(this ButtonIcon icon)
            => $"{icon}.png";
    }
}
