using System;
using BankingApplication.Database;
using BankingApplication.Models;


namespace BankingApplication.Consl
{
    class Program
    {
        internal static void Main()
        {
            Console.WriteLine("*********Welcome To ABC Banking Service********");
            Console.WriteLine("1.STAFF\n2.USER\nEnter your choice");
            Console.WriteLine("-----------------------------------------------");
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
        
       



    }
}
