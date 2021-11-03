using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Account
    {
        public String AccountId { get; set; }
        public String BankID { get; set; }
        public String AccountNumber { get; set; }
        public String AccountHolderName { get; set; }
        public bool IsActive { get; set; }
        
        public Double Balance { get; set; }
        public DateTime DateofIssue { get; set; }

        public User User { get; set; }

        public List<Transaction> Transactions=new List<Transaction>();
    }
}
