using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Bank
    {
        public String BankId { get; set; }
        public String Name { get; set; }
        public String Branch { get; set; }
        public String IFSC { get; set; }
        public Double RTGS { get; set; }
        public Double IMPS { get; set; }

        public List<Account> Accounts = new List<Account>();

        public List<Transaction> BankTransactions = new List<Transaction>();
    }
}
