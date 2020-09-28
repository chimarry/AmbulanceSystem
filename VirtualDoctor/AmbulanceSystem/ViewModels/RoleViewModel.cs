using System.Collections.Generic;

namespace AmbulanceSystem.ViewModels
{
    public class RoleViewModel
    {
        public int IdRole { get; set; }

        public string RoleName { get; set; }

        public override string ToString() => RoleName;

        public override bool Equals(object obj) => obj.GetHashCode() == GetHashCode();

        public override int GetHashCode()
        {
            var hashCode = 2065429282;
            hashCode = hashCode * -1521134295 + IdRole.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RoleName);
            return hashCode;
        }
    }
}
