using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Consl
{
    internal class Common
    {
        internal Credentials GetCredentials()
        {
            Credentials userCredentials = new Credentials();
            Console.WriteLine("Enter Username:");
            userCredentials.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            userCredentials.Password = Console.ReadLine();
            return userCredentials;
        }
        internal void DisplayStatus(bool status, String message)
        {
            if (status != false)
                Console.WriteLine(message);
            else
                Console.WriteLine("Action can't be performed..Please contact your Manager");
        }
        internal Account GetActiveUserAccount(Credentials UserCredentials)
        {
            foreach (Bank bank in BankData.banks)
            {
                Account account = bank.Accounts.SingleOrDefault(acc => acc.User.UserName.Equals(UserCredentials.UserName) && acc.User.Password == UserCredentials.Password);
                if (account != null && ValidateAccount(account))
                    return account;

            }
            return null;
        }

        private bool ValidateAccount(Account account)
        {
            if (account.IsActive.Equals(true))
                return true;
            return false;


        }
        internal Bank GetBank(Credentials staffCredentials)
        {

            foreach (Bank bank in BankData.banks)
            {
                if (bank.Employees.Any(emp => emp.UserName.Equals(staffCredentials.UserName) && emp.Password.Equals(staffCredentials.Password)))
                    return bank;

            }
            return null;

        }

    }
}
