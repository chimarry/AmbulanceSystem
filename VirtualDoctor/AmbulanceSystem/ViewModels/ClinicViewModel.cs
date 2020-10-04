using System.Collections.Generic;

namespace AmbulanceSystem.ViewModels
{
    public class ClinicViewModel
    {
        private const string DefaultFormat = "{0} : {1}";

        private string format = DefaultFormat;

        public int IdClinic { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public int IdPlace { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            var hashCode = 875105097;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + IdPlace.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format(format, Name, Place);
        }

        public void SetFormat(string format)
        {
            this.format = format;
        }
    }
}
