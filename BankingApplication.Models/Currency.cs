using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Currency
    {
        
        public  String CurrencyName { get; set; }
        public Double ExchangeRate { get; set; }
        public Currency()
        {
            CurrencyName = "INR";
            ExchangeRate = 1.0;

        }
    }
}
