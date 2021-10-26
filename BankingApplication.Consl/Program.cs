using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;

namespace BankingApplication.Consl
{
    class Program
    {
        
        internal static void Main()
        {
            
            Console.WriteLine("********Welcome To ABC Banking Service********");
            Console.WriteLine("1.STAFF\n2.USER\nEnter your choice");
            EnumRole Choice = (EnumRole)Enum.Parse(typeof(EnumRole), Console.ReadLine());
            switch (Choice)
            {
                case EnumRole.Staff:
                    new StaffActions();
                    break;
                case EnumRole.User:
                    new UserActions();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }


        internal Credentials GetCredentials()
        {
            Credentials userCredentials=new Credentials();
            Console.WriteLine("Enter Username:");
            userCredentials.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            userCredentials.Password = Console.ReadLine();
            return userCredentials;
        }
        internal Account GetUserAccount(Credentials UserCredentials)
        {
            
            foreach (Bank bank in BankData.banks)
            {
                foreach (Account Account in bank.Accounts)
                {
                    if (Account.UserName == UserCredentials.UserName && Account.Password == UserCredentials.Password)
                    {
                        return Account;

                    }
                }
            }
            return null;

        }
        internal Bank GetStaffBank(Credentials staffCredentials)
        {
            
            foreach (Bank bank in BankData.banks)
            {
                foreach (Employee employee in bank.Employees)
                {
                    if (employee.UserName == staffCredentials.UserName && employee.Password == staffCredentials.Password)
                    {
                        return bank;

                    }
                }
            }
            return null;

        }

    }
}
