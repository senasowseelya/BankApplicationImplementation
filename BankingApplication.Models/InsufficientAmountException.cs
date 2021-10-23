using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
     public class InsufficientAmountException:Exception
     {
        public InsufficientAmountException() : base("InSufficient Amount To WithDraw")
        {
        }

    }
}
