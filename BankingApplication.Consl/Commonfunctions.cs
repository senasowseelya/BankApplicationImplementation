using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Consl
{
    internal class Commonfunctions
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
    }
}
