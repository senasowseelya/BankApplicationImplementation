using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public class InsufficientAmountException:Exception
    {
        public InsufficientAmountException():base("Insuffucient balance to perform operation")
        {

        }
    }
}
