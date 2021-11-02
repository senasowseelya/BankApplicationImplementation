using System;
using BankingApplication.Database;
using BankingApplication.Models;
namespace BankingApplication.Consl
{
    class Program
    {
        internal static void Main()
        {
            Console.WriteLine("---------Welcome To ABC Banking Service--------");
            Console.WriteLine("1.STAFF\n2.USER\nEnter your choice");
            Console.WriteLine("-----------------------------------------------");
            switch((EnumRole)Convert.ToInt32(Console.ReadLine()))
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
