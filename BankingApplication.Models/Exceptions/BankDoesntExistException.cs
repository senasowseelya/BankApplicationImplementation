using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
     public class BankDoesntExistException:Exception
    {
        public BankDoesntExistException() : base("Bank Doesnot Exist.. Please check Bank Name")
        {
        }
    }
}
