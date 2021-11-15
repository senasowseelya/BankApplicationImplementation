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
        public Currency DefaultCurrency { get; set; }
        public List<ServiceCharge> ServiceCharges = new List<ServiceCharge>();
        public List<Employee> Employees = new List<Employee>();
        public List<Account> Accounts = new List<Account>();

        public List<Currency> AcceptedCurrencies = new List<Currency>();
        public Bank(String name)
        {
            Name = name;
            BankId = name.Substring(0, 3) + DateTime.Now.ToString("yyyyMMddhhmmss");
            IFSC = BankId.Substring(9) + name.Substring(3);
            Currency newCurrency = new Currency();
            AcceptedCurrencies.Add(newCurrency);
            DefaultCurrency = newCurrency;
            ServiceCharges = new List<ServiceCharge>
            {
                new ServiceCharge
                {
                    Name="SelfIMPS",
                    Value=2.0
                },
                 new ServiceCharge
                {
                    Name="SelfRTGS",
                    Value=4.0
                },
                  new ServiceCharge
                {
                    Name="OtherRTGS",
                    Value=2.4
                },
                new ServiceCharge
                {
                    Name="OtherIMPS",
                    Value=1.5
                }


            };


        }


    }
}
