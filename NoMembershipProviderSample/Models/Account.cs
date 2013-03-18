namespace NoMembershipProviderSample.Models
{
    public class Account
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        private string[] roles;

        public string[] Roles
        {
            get { return roles ?? (this.roles = new string[0]); }
            set { this.roles = value; }
        }
    }
}