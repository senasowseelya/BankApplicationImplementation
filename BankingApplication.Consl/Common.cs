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
       JsonReadWrite dataReadWrite;
       internal Common()
       {
            dataReadWrite = new JsonReadWrite();
        }
    
        internal Credentials GetCredentials()
        {
            Credentials userCredentials = new Credentials();
            Console.WriteLine("Enter Username:");
            userCredentials.UserName = Console.ReadLine();
            Console.WriteLine("Enter Password");
            userCredentials.Password = Console.ReadLine();
            Console.WriteLine("Enter Role:\n1.Staff\n2.User\n3.Exit");
            Console.WriteLine("-----------------------------------------------");
            userCredentials.role = (Role)Convert.ToInt32(Console.ReadLine());
            return userCredentials;
        }
        internal void DisplayStatus(bool status, String message)
        {
            if (status != false)
                Console.WriteLine(message);
            else
                Console.WriteLine("Action can't be performed..Please contact your Manager");
        }

        internal bool Validate(Credentials userCredentials)
        {
            if(userCredentials.role.Equals(Role.Staff))
            {
                foreach (Bank bank in BankData.banks)
                {
                    if (bank.Employees.Any(emp => emp.UserName.Equals(userCredentials.UserName) && emp.Password.Equals(userCredentials.Password)))
                        return true;
                }
                

            }
            if(userCredentials.role.Equals(Role.User))
            {
                foreach (Bank bank in BankData.banks)
                {
                    if (bank.Accounts.Any(acc => acc.User.UserName.Equals(userCredentials.UserName) && acc.User.Password == userCredentials.Password && acc.IsActive.Equals(true)))
                        return true;
                }
                
            }
            
            return false;
        }

       

    }
}
