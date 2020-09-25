namespace AmbulanceSystem.Pages.LoginAuthentication
{
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity() : base(string.Empty, string.Empty, new string[] { }, string.Empty) { }
    }
}
