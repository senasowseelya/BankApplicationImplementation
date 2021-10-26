using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Transaction
    {
        public String TransId { get; set; }
        public  String Sender { get; set; }
        public String Receiver { get; set; }
        public double Amount { get; set; }
         
        public string CurrencyName { get; set; }
        public EnumTypeofTransactions Type { get; set; }

    }
}
