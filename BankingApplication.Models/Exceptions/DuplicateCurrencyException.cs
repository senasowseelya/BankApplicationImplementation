using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class DuplicateCurrencyException:Exception
    {
        public DuplicateCurrencyException():base("Currency is already in accepted currencies list")
        {
            
        }
    }
}
