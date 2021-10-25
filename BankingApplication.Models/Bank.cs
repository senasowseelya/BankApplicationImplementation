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
        public Double Balance { get; set; }
        public String IFSC { get; set; }
        public Double SelfRTGS { get; set; }
        public Double SelfIMPS { get; set; }
        public Double OtherRTGS { get; set;}
        public Double OtherIMPS { get; set;}
        public Double BankBalance { get; set; }


        public List<Employee> Employees = new List<Employee>();

        public List<Account> Accounts = new List<Account>();

        public List<Account> InActiveAccounts = new List<Account>();

        public List<Transaction> BankTransactions = new List<Transaction>();
        public Currency DefaultCurrency { get; set; }

        public List<Currency> AcceptedCurrencies = new List<Currency>();
    }
}
