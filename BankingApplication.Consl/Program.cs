

using BankingApplication.Database;
using BankingApplication.Models;
using System;

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
        internal bool ValidateAccount(Credentials UserCredentials)
        {
            var banks = new JsonReadWrite().ReadData();
            foreach (Bank bank in banks)
            {
                foreach (Account Account in bank.Accounts)
                {
                    if (Account.UserName == UserCredentials.UserName && Account.Password == UserCredentials.Password)
                    {
                        return true;

                    }
                }
            }
            return false;

        }
    }
}
