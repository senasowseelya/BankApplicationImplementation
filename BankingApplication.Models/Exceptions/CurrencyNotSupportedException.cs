using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Models
{
    public  class CurrencyNotSupportedException:Exception
    {
        public CurrencyNotSupportedException():base("Entered Currency is Not Supported")
        {

        }
    }
}
