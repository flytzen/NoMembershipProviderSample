using System;
using System.Collections.Generic;
using System.Linq;

namespace NoMembershipProviderSample.Models
{
    public class AccountRepository
    {
        private static List<Account> accounts = new List<Account>();

        public Account FindByName(string userName)
        {
            return accounts.FirstOrDefault(a => a.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }
    }
}