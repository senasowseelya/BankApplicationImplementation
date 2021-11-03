using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
     public class InvalidTransactionException:Exception
    {
        public InvalidTransactionException():base("Invalid TransactionId")
        {

        }
    }
}
