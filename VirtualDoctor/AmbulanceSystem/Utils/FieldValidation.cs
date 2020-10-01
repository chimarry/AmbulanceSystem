using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AmbulanceSystem.Utils
{
    public static class FieldValidation
    {
        public static bool IsSelected(int index) => index > -1;

        public static bool AreSelected(params int[] indexes) =>
            !indexes.Where(x => x < -1).Any();

        public static bool NotNullOrEmpty(params string[] items) =>
            !items.Where(x => string.IsNullOrEmpty(x)).Any();

        public static bool IsValidChar(string c)
        {
            Regex regex = new Regex("\\w");
            return regex.IsMatch(c);
        }

        public static void WriteMessage(Label label, string message)
        {
            label.Content = message;
            label.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
