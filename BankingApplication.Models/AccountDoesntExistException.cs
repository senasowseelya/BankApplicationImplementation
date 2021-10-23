using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class AccountDoesntExistException : Exception
    {
        public AccountDoesntExistException() : base("Account doesn't exist")
        {
        }
    }
}
