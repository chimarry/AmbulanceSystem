using AmbulanceDatabase.Entities;
using System.Collections.Generic;

namespace AmbulanceSystem.ViewModels
{
    public class LocalAccountViewModel
    {
        public int IdLocalAccount { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Roles { get; set; }

        private List<Role> roles;
        public List<Role> GetRoles() => roles;

        public void SetRoles(List<Role> roles)
        {
            this.roles = roles;
        }

        public override string ToString() => Email;


        public override bool Equals(object obj) => obj.GetHashCode() == GetHashCode();

        public override int GetHashCode() => -506688385 + EqualityComparer<string>.Default.GetHashCode(Email);
    }
}
