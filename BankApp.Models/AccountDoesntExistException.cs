using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class AccountDoesntExistException : Exception
    {
        public AccountDoesntExistException() : base("Account doesn't exist")
        {
        }
    }
}
