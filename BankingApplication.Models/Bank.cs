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
        public Double BankBalance { get; set; }
        public Currency DefaultCurrency { get;set; }
        public ServiceCharge ServiceCharges { get; set; }
        public List<Employee> Employees = new List<Employee>();
        public List<Account> Accounts = new List<Account>();
        public List<Transaction> BankTransactions = new List<Transaction>();
        public List<Currency> AcceptedCurrencies = new List<Currency>();
        public Bank(String name)
        {
            Name = name;
            BankId = name.Substring(0, 3) + DateTime.Now.ToString("yyyyMMddhhmmss");
            IFSC = BankId.Substring(9) + name.Substring(3);
            Currency newCurrency = new Currency();
            AcceptedCurrencies.Add(newCurrency);
            DefaultCurrency = newCurrency;
            ServiceCharge serviceCharge = new ServiceCharge
            {
                SelfIMPS = 5.0,
                SelfRTGS = 0.0,
                OtherRTGS = 2.4,
                OtherIMPS = 6.5
             };
            ServiceCharges = serviceCharge;
        }

    }
}
