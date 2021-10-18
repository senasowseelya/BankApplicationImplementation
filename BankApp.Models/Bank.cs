using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Bank
    {
        public String BankId { get; set; }
        public String Name { get; set; }
        public String Branch { get; set; }

        public List<Account> Accounts = new List<Account>();
    }
}
