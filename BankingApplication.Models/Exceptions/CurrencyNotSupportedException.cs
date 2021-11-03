using System;
using System.Runtime.Serialization;

namespace BankingApplication.Models
{
    
    public class CurrencyNotSupportedException : Exception
    {
        public CurrencyNotSupportedException():base("This Currency type is not supported")
        {

        }

        
    }
}