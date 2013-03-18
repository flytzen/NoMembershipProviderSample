using System.Web.Helpers;

namespace NoMembershipProviderSample.Models
{
    public static class AccountExtensions
    {
         public static void SetPassword(this Account account, string password)
         {
             account.PasswordHash = Crypto.HashPassword(password);
         }

        public static bool ValidatePassword(this Account account, string password)
        {
            return Crypto.VerifyHashedPassword(account.PasswordHash, password);
        }
    }
}