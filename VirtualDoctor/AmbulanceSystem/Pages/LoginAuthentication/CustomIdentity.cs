using System.Security.Principal;

namespace AmbulanceSystem.Pages.LoginAuthentication
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string email, string[] roles, string passwordHash)
        {
            Name = name;
            Email = email;
            Roles = roles;
            PasswordHash = passwordHash;
        }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }
        public string PasswordHash { get; private set; }
        public string AuthenticationType { get { return "Custom authentication"; } }
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
    }
}
