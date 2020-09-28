using AmbulanceServices.Factories;
using AmbulanceServices.Interfaces;
using System.Collections.Generic;

namespace AmbulanceSystem.ViewModels
{
    public class ClinicViewModel
    {
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
            return Name + " : " + Place;
        }
    }
}
